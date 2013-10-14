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
namespace Boxes.Integration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Dependency resolver interface to be used by <see cref="Boxes.Integration"/>
    /// </summary>
    public interface IDependencyResolver 
    {
        /// <summary>
        /// resolve an instance
        /// </summary>
        /// <typeparam name="T">the service to be resolved</typeparam>
        /// <returns>an instance of the service</returns>
        T Resolve<T>();

        /// <summary>
        /// resolve an instance
        /// </summary>
        /// <param name="type">the service to be resolved</param>
        /// <returns>an instance of the service</returns>
        object Resolve(Type type);

        /// <summary>
        /// resolve all instance for a service
        /// </summary>
        /// <typeparam name="T">the service to be resolved</typeparam>
        /// <returns>all instances for the service</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// resolve all instance for a service
        /// </summary>
        /// <param name="type">the service to be resolved</param>
        /// <returns>all instances for the service</returns>
        IEnumerable<object> ResolveAll(Type type);
        
        /// <summary>
        /// handles the releasing of an instance
        /// </summary>
        /// <param name="obj">the object to release</param>
        void Release(object obj);
    }
}