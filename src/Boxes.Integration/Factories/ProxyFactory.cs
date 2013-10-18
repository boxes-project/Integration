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
namespace Boxes.Integration.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using Castle.DynamicProxy;

    public class ProxyFactory : IProxyFactory<IInterceptor>
    {
        private readonly ProxyGenerator _proxyGenerator;

        public ProxyFactory()
        {
            _proxyGenerator = new ProxyGenerator();
        }

        public T CreateProxy<T>(T instance, IEnumerable<IInterceptor> interceptors) where T : class
        { 
            return _proxyGenerator.CreateClassProxyWithTarget(instance, interceptors.ToArray());
        }

        public object CreateProxy(object instance, IEnumerable<IInterceptor> interceptors)
        {
            return _proxyGenerator.CreateClassProxyWithTarget(instance, interceptors.ToArray());
        }
    }
}