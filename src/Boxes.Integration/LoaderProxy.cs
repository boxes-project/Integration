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
    using System.Collections.Generic;
    using Loading;

    /// <summary>
    /// Loader proxy allows a third party class to see which new packages have
    /// been loaded
    /// </summary>
    internal class LoaderProxy : ILoader
    {
        private readonly ILoader _proxied;
        private readonly List<Package> _loadedPackages = new List<Package>();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="proxied">loader to proxy</param>
        public LoaderProxy(ILoader proxied)
        {
            _proxied = proxied;
        }

        /// <summary>
        /// any new loaded packages
        /// </summary>
        public IEnumerable<Package> Packages { get { return _loadedPackages; } }

        /// <summary>
        /// clears the load packages, in case you need to do multiple loads
        /// </summary>
        public void ClearLoadedPackages()
        {
            _loadedPackages.Clear();
        }


        public void LoadPackage(Package package)
        {
            _proxied.LoadPackage(package);
            _loadedPackages.Add(package);
        }
    }
}