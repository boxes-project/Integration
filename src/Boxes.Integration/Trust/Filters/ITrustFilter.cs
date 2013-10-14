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
namespace Boxes.Integration.Trust.Filters
{
    using System;
    using Contexts;

    /// <summary>
    /// A trust filter will see if the context is legit and there is no bad intention happening
    /// </summary>
    public interface ITrustFilter
    {
        /// <summary>
        /// returns the main trust context this filter supports (this is to try and make the process a little faster)
        /// </summary>
        Type HandlesTrustContextType { get; } 

        /// <summary>
        /// to see if this filter can be applied to the current trust context
        /// </summary>
        /// <param name="trustContext">the trust context</param>
        /// <returns></returns>
        bool CanHandle(TrustContext trustContext);

        /// <summary>
        /// returns if the context can be trusted
        /// </summary>
        /// <param name="trustContext">the trust context</param>
        /// <returns>true if the context can be trusted</returns>
        bool IsTrusted(TrustContext trustContext);
    }
}