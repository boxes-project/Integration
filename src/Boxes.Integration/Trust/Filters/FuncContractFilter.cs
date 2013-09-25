// Copyright 2012 - 2013 dbones.co.uk (David Rundle)
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
    using Contexts.BoxesExtensions;

    /// <summary>
    /// apply the trust of a type given its package context
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class FuncContractFilter<TContract> : ContractFilter<TContract>
    {
        private readonly Func<TypeFromPackageTrustContext, bool> _trust;

        public FuncContractFilter(Func<TypeFromPackageTrustContext, bool> trust)
        {
            _trust = trust;
        }

        protected override bool IsTrustedContext(TypeFromPackageTrustContext trustContext)
        {
            return _trust(trustContext);
        }
    }
}