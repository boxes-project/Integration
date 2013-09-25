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
namespace Boxes.Integration.Setup.Registrations
{
    using System;

    /// <summary>
    /// a register context holds all the information required for someone to have complete control
    /// over a single types registration.
    /// </summary>
    /// <typeparam name="TConfiguration">the underlying IoC registration type</typeparam>
    public class RegisterContext<TConfiguration>
    {
        /// <summary>
        /// the configuration of the underlying IoC registration
        /// </summary>
        public TConfiguration Configuration { get; set; }

        /// <summary>
        /// the type the configuration is for
        /// </summary>
        public Type Type { get; set; }
    }
}