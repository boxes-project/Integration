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
namespace Boxes.Integration.Trust.Contexts.BoxesExtensions
{
    using System;

    /// <summary>
    /// check to see if a package is allowed to configure a component (Boxes Extension)
    /// </summary>
    public sealed class SetupFromPackageTrustContext : TrustContext
    {
        
        public SetupFromPackageTrustContext(Type contract, Type setup, Package package)
        {
            Contract = contract;
            Setup = setup;
            Package = package;
        }

        /// <summary>
        /// the contract being setup
        /// </summary>
        public Type Contract { get; set; }

        /// <summary>
        /// the class which will setup the contract
        /// </summary>
        public Type Setup { get; set; }
        
        /// <summary>
        /// the package where the setup resides
        /// </summary>
        public Package Package { get; set; }

    }
}