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

    /// <summary>
    /// A base trust filter, this adds a basic handle to match the <see cref="TrustContext"/>
    /// </summary>
    /// <typeparam name="TContext">the <see cref="TrustContext"/> this filter will be filter against</typeparam>
    public abstract class TrustFilterBase<TContext> : ITrustFilter 
        where TContext : TrustContext
    {
        public Type HandlesTrustContextType { get { return typeof (TContext); } }

        public virtual bool CanHandle(TrustContext trustContext)
        {
            if(!(trustContext is TContext))
            {
                return false;
            }

            return CanHandleContext((TContext)trustContext);
        }

        public virtual bool IsTrusted(TrustContext trustContext)
        {
            return IsTrustedContext((TContext) trustContext);
        }

        protected abstract bool CanHandleContext(TContext context);
        protected abstract bool IsTrustedContext(TContext trustContext);
    }
}