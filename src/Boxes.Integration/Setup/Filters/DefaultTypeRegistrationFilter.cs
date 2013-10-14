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
namespace Boxes.Integration.Setup.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// the default will return all exported types
    /// </summary>
    class DefaultTypeRegistrationFilter : ITypeRegistrationFilter 
    {
        public IEnumerable<Type> FilterTypes(Package package)
        {
            return package
                .LoadedAssemblies
                .SelectMany(x => x.GetExportedTypes());
        }
    }
}