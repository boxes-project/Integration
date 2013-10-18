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
    using Castle.DynamicProxy;

    /// <summary>
    /// register an interceptor/aspect for AOP
    /// </summary>
    public interface IRegisterInterception
    {
        IEnumerable<InterceptorMeta> InterceptorMetas { get; }

        /// <summary>
        /// apply a filter (pattern) to find which types this registration will apply too
        /// </summary>
        /// <param name="where">the pattern to find the types this registration applies too</param>
        IRegisterInterception Where(Predicate<InterceptionContext> @where);

        /// <summary>
        /// the interceptors to apply
        /// </summary>
        /// <param name="interceptor">interceptors to apply</param>
        IRegisterInterception Apply(params Type[] interceptor);

        /// <summary>
        /// the interceptor apply
        /// </summary>
        /// <typeparam name="T">the interceptor to apply</typeparam>
        IRegisterInterception Apply<T>() where T : IInterceptor;
    }
}