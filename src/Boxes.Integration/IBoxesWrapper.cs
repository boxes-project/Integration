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
    using Discovering;
    using Loading;
    using Setup;
    using Trust;

    /// <summary>
    /// takes Boxes and extends it to provide lifestyle management with Dependency Injection along with the (isolated) module loading
    /// </summary>
    /// <remarks>
    /// the generic type is require to help the extension methods, do not get rid of it
    /// </remarks>
    public interface IBoxesWrapper<TBuilder,TContainer> : IDisposable
    {
        /// <summary>
        /// package registry, this will provide the information of what modules have been loaded
        /// </summary>
        PackageRegistry PackageRegistry { get; }
        
        /// <summary>
        /// Setup Boxes with the <see cref="ILoader"/> and default <see cref="IPackageScanner"/> 
        /// </summary>
        /// <typeparam name="TLoader">the type of the loader to use with boxes</typeparam>
        /// <param name="defaultPackageScanner">the default scanner to use to find modules with</param>
        void Setup<TLoader>(IPackageScanner defaultPackageScanner) where TLoader : ILoader;

        /// <summary>
        /// Discover packages, using a supplied <see cref="IPackageScanner"/>
        /// </summary>
        /// <param name="packageScanner">package scanner to discovery packages with</param>
        void DiscoverPackages(IPackageScanner packageScanner);

        /// <summary>
        /// Discover packages, using the default <see cref="IPackageScanner"/>
        /// </summary>
        void DiscoverPackages();

        /// <summary>
        /// Load packages ready for the application to use
        /// </summary>
        void LoadPackages();

        /// <summary>
        /// get an internal service
        /// </summary>
        /// <typeparam name="T">The service to return</typeparam>
        /// <returns>null if it it does not exist</returns>
        T GetService<T>();
    }

    /// <summary>
    /// extensions for the boxes wrapper
    /// </summary>
    public static class BoxesWrapperExtensions
    {
        /// <summary>
        /// the trust manager being used by boxes.
        /// </summary>
        public static ITrustManager TrustManager<TBuilder, TContainer>(this IBoxesWrapper<TBuilder, TContainer> boxes)
        {
            return boxes.GetService<ITrustManager>();
        }



    }
}