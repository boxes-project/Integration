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

    /// <summary>
    /// contains information on when and where to apply an Aspect
    /// </summary>
    public class InterceptorMeta
    {
        /// <summary>
        /// when to apply this interceptor
        /// </summary>
        public Predicate<InterceptionContext> Where { get; set; }

        /// <summary>
        /// the interceptor to apply
        /// </summary>
        public Type Interceptor { get; set; }
    }
}
