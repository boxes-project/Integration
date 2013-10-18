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
namespace Boxes.Integration.Setup.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.DynamicProxy;

    public class RegisterInterception : IRegisterInterception
    {
        private Predicate<InterceptionContext> _where;
        private readonly List<Type> _interceptorTypes = new List<Type>();

        public IEnumerable<InterceptorMeta> InterceptorMetas
        {
            get
            {
                return _interceptorTypes.Select(x => new InterceptorMeta() {Interceptor = x, Where = _where});
            }
        }

        public IRegisterInterception Where(Predicate<InterceptionContext> @where)
        {
            _where += @where;
            return this;
        }

        public IRegisterInterception Apply(params Type[] interceptor)
        {
            _interceptorTypes.AddRange(interceptor);
            return this;
        }

        public IRegisterInterception Apply<T>() where T : IInterceptor
        {
            _interceptorTypes.Add(typeof(T));
            return this;
        }
    }
}