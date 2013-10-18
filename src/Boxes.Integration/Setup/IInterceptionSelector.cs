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
namespace Boxes.Integration.Setup
{
    using System;
    using System.Collections.Generic;
    using Interception;

    /// <summary>
    /// the selector will indicate what interceptors to apply on a given registration
    /// </summary>
    public interface IInterceptionSelector
    {
        /// <summary>
        /// find the interceptor types to apply on a registration
        /// </summary>
        /// <param name="ctx">contains all the inforamtion required to see which interceptor types will be returned</param>
        /// <returns>a distinct list of interceptor types</returns>
        IEnumerable<Type> InterceptorsToApply(InterceptionContext ctx);
    }
}