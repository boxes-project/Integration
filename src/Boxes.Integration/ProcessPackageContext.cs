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
    /// the context of a package while it is being processed
    /// </summary>
    public class ProcessPackageContext
    {
        private readonly int _hash;

        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="package">package</param>
        /// <param name="dependencyTypes">types of interest</param>
        public ProcessPackageContext(Package package, IEnumerable<Type> dependencyTypes)
        {
            Package = package;
            DependencyTypes = dependencyTypes;
            _hash = package.GetHashCode();
        }

        /// <summary>
        /// the package
        /// </summary>
        public Package Package { get; private set; }

        /// <summary>
        /// all the types of interest
        /// </summary>
        public IEnumerable<Type> DependencyTypes { get; private set; }

        public override int GetHashCode()
        {
            return _hash;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as ProcessPackageContext;
            if (temp == null) return false;
            return GetHashCode() == temp.GetHashCode();
        }
    }
}