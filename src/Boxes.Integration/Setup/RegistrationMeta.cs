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
namespace Boxes.Integration.Setup
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// stores information about the registration, this will be used to create a boxes task
    /// </summary>
    public class RegistrationMeta
    {
        public RegistrationMeta()
        {
            Configurations = new List<Action<object>>();
        }

        /// <summary>
        /// filter to apply
        /// </summary>
        public Predicate<Type> Where { get; set; }
        
        /// <summary>
        /// what to register it as
        /// </summary>
        public Func<Type, IEnumerable<Type>> With { get; set; }
        
        /// <summary>
        /// use custom ctor
        /// </summary>
        [Obsolete("this is being removed, the use of Configurations will allow for direct access to the registration")]
        public object FactoryMethod { get; set; }
        
        /// <summary>
        /// the scope of the types which this registration is for
        /// </summary>
        public object LifeStyle { get; set; }

        /// <summary>
        /// a list of configurations to apply
        /// </summary>
        public List<Action<object>> Configurations { get; private set; }
    }
}