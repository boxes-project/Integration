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
namespace Boxes.Integration.InternalIoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    /// <summary>
    /// this container is a simple internalContainer which handles all contracts as singletons, (very limited functionality)
    /// </summary>
    internal class InternalInternalContainer : IInternalContainer
    {
        private readonly IDictionary<Type, Registration> _registrations = new Dictionary<Type, Registration>();
        private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly object _lock = new object();
        
        public void Add(Type contract, Type service)
        {
            lock (_lock)
            {
                Registration registration;
                if (_registrations.TryGetValue(contract, out registration))
                {
                    //at this point the source is trusted, so it can override a service if required
                    registration.Service = service;
                }
                else
                {
                    registration = new Registration(contract, service);
                    _registrations.Add(contract, registration);
                }
            }
        }

        public void setInstance(Type service, object instance)
        {
            if (service != instance.GetType())
            {
                throw new ServiceTypeDoesNotMatchInstanceException(service, instance);
            }

            if (_instances.ContainsKey(service))
            {
                _instances[service] = instance;
            }
            else
            {
                _instances.Add(service, instance);
            }
        }


        public object Resolve(Type contract)
        {
            lock (_lock)
            {
                Func<Registration, Type> getServiceType;
                if (contract.IsGenericType)
                {
                    //no support for list or lazy, this is a simple internalContainer
                    Type[] genericArguments = contract.GetGenericArguments();
                    getServiceType = r => r.Service.IsGenericType ? r.Service.MakeGenericType(genericArguments) : r.Service;
                    contract = contract.GetGenericTypeDefinition();
                }
                else
                {
                    getServiceType = r => r.Service;
                }

                Registration registration;
                if (!_registrations.TryGetValue(contract, out registration))
                {
                    return null;
                }

                var serviceType = getServiceType(registration);

                object instance;
                if (_instances.TryGetValue(serviceType, out instance))
                {
                    return instance;
                }

                instance = CreateInstance(serviceType);
                _instances.Add(serviceType, instance);

                return instance;
            }
        }


        private object CreateInstance(Type service)
        {
            var ctor = service.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Count())
                .FirstOrDefault(x => x.GetParameters().All(p => Resolve(p.ParameterType) != null));

            if (ctor == null)
            {
                throw new Exception("cannot resolve service");
            }

            var ctorArgs = ctor.GetParameters().Select(x => Resolve(x.ParameterType)).ToArray();
            var instance = ctor.Invoke(ctorArgs);
            return instance;
        }


        public void Dispose()
        {
            lock (_lock)
            {
                foreach (var instance in _instances.Values)
                {
                    var disposeOfMe = instance as IDisposable;
                    if (disposeOfMe != null)
                    {
                        disposeOfMe.Dispose();
                    }
                }
                _instances.Clear();
            }
        }
    }
}