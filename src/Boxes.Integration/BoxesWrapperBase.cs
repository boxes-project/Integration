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
namespace Boxes.Integration
{
    using Boxes.Tasks;
    using Discovering;
    using Factories;
    using InternalIoc;
    using Loading;
    using Process;
    using Setup;
    using Tasks;
    using Trust;

    /// <summary>
    /// this wrapper of boxes.core, this will integrate the features of boxes with an IoC container
    /// </summary>
    /// <typeparam name="TBuilder">the type used to build registrations</typeparam>
    /// <typeparam name="TContainer">the type of the container, this will be used to resolve the instances</typeparam>
    /// <remarks>
    /// do not remove the TContainer this is used with the extension methods and with the other classes in the system</remarks>
    public abstract class BoxesWrapperBase<TBuilder, TContainer> : IBoxesWrapper<TBuilder, TContainer>
    {
        //TODO: look at disabling registrations (controllable in a module) 
        //http://ayende.com/blog/2792/introducing-monorail-hotswap 
        //issue is around the containers, a subset allow to deletions of registrations
        //an other way is to restart the app and only register the required packages with the IoC
        //the second way can be supported by coding a module
        //favored way, use of child container, will need to re-initialise the container (delete, create a new one)

        //TODO: migrations (possible in a module)
        //updates of a module will require a restart, currently the Loaders do not support 
        //app domains. they do offer a level of isolation

        //TODO: Multi-tenancy (possible in a module)
        //use child containers to handle tenants. implement a IProcess (consider PLinq) to register
        //the correct packages with the correct child container.

        //TODO: consider how to admin an application (need feedback)

        //TODO: events (possible in a module)

        private IPackageScanner _defaultPackageScanner;
        private ILoader _loader;
        private readonly TaskRunner<Package> _extensionRunner;
        private readonly LoaderFactory _loaderFactory;
        private readonly IInternalContainer _internalContainer;

        protected BoxesWrapperBase()
        {
            PackageRegistry = new PackageRegistry();
            _internalContainer = new InternalInternalContainer();
            _loaderFactory = new LoaderFactory();

            //setup the internal IoC
            _internalContainer.Add<PackageRegistry, PackageRegistry>();
            _internalContainer.setInstance(typeof(PackageRegistry), PackageRegistry);
            
            _internalContainer.Add<IProcessOrder, TopologicalProcessOrder>();
            _internalContainer.Add(typeof(IIocSetup<>), typeof(IocSetup<>));
            _internalContainer.Add<ITrustManager, TrustManager>();

            _internalContainer.Add(typeof (IProxyFactory<>), typeof (ProxyFactory));
            _internalContainer.Add<LoaderFactory, LoaderFactory>();
            _internalContainer.setInstance(typeof(LoaderFactory), _loaderFactory);

            
            //this is a default impl, it could be overriden in a module by another interface/impl
            _internalContainer.Add(typeof(IDefaultContainerSetup<>), typeof(DefaultContainerSetup<>));


            Initialize(_internalContainer);

            _extensionRunner = new TaskRunner<Package>(new ExtendBoxesTask(_internalContainer));
            
        }

        public PackageRegistry PackageRegistry { get; private set; }

        public void Setup<TLoader>(IPackageScanner defaultPackageScanner) where TLoader : ILoader
        {
            _defaultPackageScanner = defaultPackageScanner;
            _loader = _loaderFactory.CreateLoader<TLoader>(PackageRegistry);
        }

        public void DiscoverPackages(IPackageScanner packageScanner)
        {
            PackageRegistry.DiscoverPackages(packageScanner);
        }

        public void DiscoverPackages()
        {
            PackageRegistry.DiscoverPackages(_defaultPackageScanner);
        }

        public void LoadPackages()
        {
            var loader = new LoaderProxy(_loader);
            PackageRegistry.LoadPackages(loader);

            var processOrder = _internalContainer.Resolve<IProcessOrder>();
            var arrangedPackages = processOrder.Arrange(loader.Packages);

            //process extensions first (completely, before running the rest of the package or process)
            _extensionRunner.Execute(arrangedPackages).Force();
        }

        public T GetService<T>()
        {
            return _internalContainer.Resolve<T>();
        }


        public virtual void Dispose()
        {
            _internalContainer.Dispose();
        }

        /// <summary>
        /// setup boxes integration with the IoC implementation (register types with the internal IoC)
        /// </summary>
        protected abstract void Initialize(IInternalContainer internalContainer);

    }
}