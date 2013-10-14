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
    /// Manifest information for a package, this also includes any extensions the package includes
    /// </summary>
    public class ExtensionManifest : Manifest
    {
        private readonly ICollection<Module> _extensions = new List<Module>(); 

        public ExtensionManifest(string name, Version version, string description, 
                                 IEnumerable<Module> exports, IEnumerable<Module> imports) 
            : base(name, version, description, exports, imports) {}

        /// <summary>
        /// list of all the modules which extend Boxes functionality
        /// </summary>
        public IEnumerable<Module> Extensions { get { return _extensions; } }

        internal void SetExtension(Module extension)
        {
            _extensions.Add(extension);
        }
    }
}