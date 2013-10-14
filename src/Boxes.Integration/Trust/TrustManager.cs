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
namespace Boxes.Integration.Trust
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Contexts;
    using Exceptions;
    using Filters;

    /// <summary>
    /// Default trust manager
    /// </summary>
    /// <remarks>
    /// this class is sealed, to try and provide some security (ie proxy-ing it)
    /// 
    /// Also this works on a optimistic way, it looks for a black listing, not a white one.
    /// 
    /// you may also note the <see cref="IBoxesExtensionWithSetup"/> interface, this is to allow people configure
    /// extra filters in modules, to ensure no once replace this with another hacked instance, just add a filter to prevent it.
    /// </remarks>
    public sealed class TrustManager : ITrustManager, IBoxesExtensionWithSetup
    {
        readonly IDictionary<Type, List<ITrustFilter>> _trustFilters = new Dictionary<Type, List<ITrustFilter>>();

        public void IsTrusted(TrustContext context)
        {
            List<ITrustFilter> filters;
            if (!_trustFilters.TryGetValue(context.GetType(), out filters))
            {
                //no filters
                return;
            }
            
            bool failedTrust = filters
                .Where(trustFilter => trustFilter.CanHandle(context))
                .Any(trustFilter => !trustFilter.IsTrusted(context));
            
            if(failedTrust)
            {
                throw new FailedTrustException(context);
            }
        }

        public void AddTrust(ITrustFilter trust)
        {
            List<ITrustFilter> filters;
            if (!_trustFilters.TryGetValue(trust.HandlesTrustContextType, out filters))
            {
                filters = new List<ITrustFilter>();
                _trustFilters.Add(trust.HandlesTrustContextType, filters);
            }

            filters.Add(trust);
        }
    }
}