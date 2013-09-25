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
namespace Boxes.Integration.InternalIoc
{
    using System;

    /// <summary>
    /// a registration of a contract with a given service.
    /// </summary>
    public class Registration
    {
        private readonly int _hash;
        private Type _service;

        public Registration(Type contract, Type service)
        {
            IsAllowedToBeOverridden = false;
            IsDefault = false;

            Contract = contract;
            Service = service;

            _hash = contract.FullName.GetHashCode();

        }

        public Type Contract { get; private set; }
        public Type Service
        {
            get { return _service; }
            set
            {
                if (!IsAllowedToBeOverridden && IsDefault)
                {
                    throw new Exception("you cannot override this service");
                }
                _service = value;
            }
        }


        /// <summary>
        /// is this set via boxes, or an extension
        /// </summary>
        internal bool IsDefault { get; private set; }

        /// <summary>
        /// indicate if the component is allowed to be overridden
        /// </summary>
        internal bool IsAllowedToBeOverridden { get; private set; }


        public override int GetHashCode()
        {
            return _hash;
        }
    }
}
