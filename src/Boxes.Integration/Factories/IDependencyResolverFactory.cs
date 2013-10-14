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
namespace Boxes.Integration.Factories
{
    /// <summary>
    /// create an instance of a dependency resolver
    /// </summary>
    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// create an instance of a dependency resolver
        /// </summary>
        /// <param name="container">the container which the dependency resolver will wrap around</param>
        /// <returns>a dependency resolver</returns>
        IDependencyResolver CreateResolver(object container);
    }
}