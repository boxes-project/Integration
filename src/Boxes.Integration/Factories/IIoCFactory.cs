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
    /// create instances of containers as required
    /// </summary>
    public interface IIocFactory<TBuilder, TContainer>
    {

        /// <summary>
        /// create an instance of the IoC Builder
        /// </summary>
        /// <returns>an IoC builder, in most cases this may just be the IoC container</returns>
        TBuilder CreateBuilder();

        /// <summary>
        /// create an instance of the IoC Builder for a child container
        /// </summary>
        /// <param name="parentContainer">the parent IoC container</param>
        /// <returns>a IoC builder, in most cases this may just be the child IoC container</returns>
        TBuilder CreateBuilder(TContainer parentContainer);

        /// <summary>
        /// Creates an instance of the main container
        /// </summary>
        TContainer CreateContainer(TBuilder builder);

        /// <summary>
        /// Creates a child container, off the main container
        /// </summary>
        TContainer CreateChildContainer(TContainer container, TBuilder builder);
    }
}