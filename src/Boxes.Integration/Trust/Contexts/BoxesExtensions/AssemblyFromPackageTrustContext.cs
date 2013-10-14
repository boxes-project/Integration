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
namespace Boxes.Integration.Trust.Contexts.BoxesExtensions
{
    using System.Reflection;

    /// <summary>
    /// check to see if an assembly/module from a package is ok (Boxes Extension)
    /// </summary>
    public sealed class AssemblyFromPackageTrustContext : TrustContext
    {
        public AssemblyFromPackageTrustContext(Assembly assembly, Package package)
        {
            Assembly = assembly;
            Package = package;
        }

        /// <summary>
        /// the assembly/module in question
        /// </summary>
        public Assembly Assembly { get; set; }
        
        /// <summary>
        /// package the assembly resides in
        /// </summary>
        public Package Package { get; set; }
    }
}