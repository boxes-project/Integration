// Copyright 2012 - 2013 dbones.co.uk
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Boxes.Integration.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Boxes.Tasks;
    using Extensions;
    using InternalIoc;
    using Trust;
    using Trust.Contexts;
    using Trust.Contexts.BoxesExtensions;

    internal class ExtendBoxesTask : IBoxesTask<Package>
    {
        private readonly IInternalContainer _container;
        private readonly ITrustManager _trustManager;
        private readonly IDictionary<Type, Action<Type, object>> _callSetupCaches = new Dictionary<Type, Action<Type, object>>(); 

        public ExtendBoxesTask(IInternalContainer container)
        {
            _container = container;
            _trustManager = container.Resolve<ITrustManager>();
        }

        public bool CanHandle(Package item)
        {
            return item.Loaded && item.Manifest is ExtensionManifest;
        }

        public void Execute(Package item)
        {
            //1. find assemblies with extensions
            //2. register types with internal ioc
            //3. find any extensions which can be configured
            //4. configure extension

            var manifest = (ExtensionManifest)item.Manifest;
            var assemblies = manifest
                .Extensions
                .Select(item.GetInternalAssembly)
                .Select(x=>
                        {
                            if(x.Assembly == null)
                            {
                                x.LoadFromFile();
                            }
                            _trustManager.IsTrusted(new AssemblyFromPackageTrustContext(x.Assembly, item));
                            return x.Assembly;
                        });

            var types = assemblies.SelectMany(x => x.GetExportedTypes()).ToArray();

            foreach (var service in types.Where(x => !x.IsInterface && !x.IsAbstract  && typeof(IBoxesExtension).IsAssignableFrom(x)))
            {
                var registeredService = service;
                var contract = registeredService.FirstInterface();
                
                //try and get the generic types
                if (contract.IsGenericType)
                {
                    contract = contract.GetGenericTypeDefinition();
                }

                if (registeredService.IsGenericType)
                {
                    registeredService = service.GetGenericTypeDefinition();
                }

                
                _trustManager.IsTrusted(new TypeFromPackageTrustContext(contract, service, item));
                _container.Add(contract, registeredService);
            }


            var setupExtensionType = typeof(ISetupBoxesExtension<>);

            //painful code follows
            foreach (var setup in types.Where(x => !x.IsAbstract && x.IsClass))
            {
                //filter out types we are not interested with
                var setupInterface = setup
                    .AllInterfaces()
                    .FirstOrDefault(x =>
                                    x.IsGenericType
                                    && x.GetGenericTypeDefinition().IsAssignableFrom(setupExtensionType));

                if (setupInterface == null)
                {
                    continue;
                }

                //try to get the interface so we can resolve the correct type. (there will be some limitations)
                Type directContract = setupInterface.GetGenericArguments()[0];
                Type contractInterface = directContract.IsInterface
                                             ? directContract
                                             : directContract.FirstInterface();
                
                //run trust against the interface, as it will be this type which will be used to resolve the
                //type with the internal ioc
                _trustManager.IsTrusted(new SetupFromPackageTrustContext(contractInterface, setup, item));

                //get the service, and set it up
                Action<Type, object> callSetup;
                if (!_callSetupCaches.TryGetValue(directContract, out callSetup))
                {
                    //lame try to minimise the amount of reflection being recalled.
                    var configureMethodInfo = setupExtensionType.MakeGenericType(new[] { directContract }).GetMethod("Configure");
                    var handleMethodInfo = setupExtensionType.MakeGenericType(new[] { directContract }).GetMethod("CanHandle");
                    
                    callSetup =
                        (setupType, serviceInstance) =>
                        {
                            var instance = Activator.CreateInstance(setupType);
                            var canHandle = (bool)handleMethodInfo.Invoke(instance, new[] { serviceInstance });
                            if (canHandle)
                            {
                                configureMethodInfo.Invoke(instance, new[] { serviceInstance });    
                            }
                        };
                }

                var service = _container.Resolve(contractInterface);
                callSetup(setup, service);
            }
        }
    }
}