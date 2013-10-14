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
    using System.Collections.Generic;
    using Boxes.Tasks;
    using Extensions;
    using Filters;
    using Registrations;

    /// <summary>
    /// the setup for this area of the application
    /// </summary>
    public interface IContainerSetup : IBoxesExtensionWithSetup
    {
        /// <summary>
        /// the default filter to find exported classes with
        /// </summary>
        ITypeRegistrationFilter DefaultTypeRegistrationFilter { get; }

        /// <summary>
        /// Register a type (can be as simple as does it implement a Dependency or to apply a filter)
        /// </summary>
        /// <param name="registration">The registration with details on how setup the IoC with the types which match the where clause</param>
        void AddRegistration(IRegister registration);

        /// <summary>
        /// override the default package filter (this will be used across all packages)
        /// </summary>
        /// <param name="typeTypesFilter">the filter</param>
        void SetDefaultFilter(ITypeRegistrationFilter typeTypesFilter);

        /// <summary>
        /// set a filter for a particular package, some packages may require their own filters
        /// </summary>
        /// <param name="typesFilterTypes">the filter</param>
        /// <param name="packgeName">packages which </param>
        void AddPackgeLevelFilter(ITypeRegistrationFilter typesFilterTypes, params string[] packgeName);

        /// <summary>
        /// get the filter for a given package
        /// </summary>
        /// <param name="packageName">name of the package</param>
        /// <returns>null if there is not filter</returns>
        ITypeRegistrationFilter GetTypeRegistrationFilter(string packageName);
    }


    /// <summary>
    /// setup how LifeStyles will be managed
    /// </summary>
    public interface IContainerSetup<TBuilder> : IContainerSetup
    {
        /// <summary>
        /// all the registration to run the types through
        /// </summary>
        IEnumerable<IBoxesTask<RegistrationContext<TBuilder>>> Registrations { get; }
    }

    
}