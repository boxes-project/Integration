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

    //TODO: keep an eye on the use of this class, look at some profiling

    /// <summary>
    /// the context for the registration
    /// </summary>
    /// <typeparam name="TBuilder">the current builder</typeparam>
    public class RegistrationContext<TBuilder>
    {
        public RegistrationContext(Type type, TBuilder builder)
        {
            Type = type;
            Builder = builder;
        }

        /// <summary>
        /// the type to register
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// the builder to register it with
        /// </summary>
        public TBuilder Builder { get; set; }
    }
}