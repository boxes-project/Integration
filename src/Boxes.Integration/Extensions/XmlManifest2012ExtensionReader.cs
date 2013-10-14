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
namespace Boxes.Integration.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Boxes.Tasks;

    /// <summary>
    /// Support for the 2012 extension manifest
    /// </summary>
    /// <code>
    /// <pre>
    /// <?xml version="1.0" encoding="utf-8" ?>
    /// <manifest xmlns="http://schemas.dbones.co.uk/developer/boxes/2012/extension">
    ///     <name>Acme.Test</name>
    ///     <version>1.0</version>
    ///     <description>this is an awesome module which will add cool features</description>
    ///     <exports>
    ///         <assembly name="Acme.Test.Bus" version="3.1.0" />
    ///         <assembly name="Acme.Test.Core" />
    ///     </exports>
    ///     <imports>
    ///         <dependency name="BlueSky.Ioc" version="3.1.0" />
    ///         <dependency name="Acme.Data" />
    ///     </imports>
    ///     <extends>
    ///         <assembly name="Acme.Test.Extension" />
    ///     </extends>
    /// </manifest>
    /// </pre>
    /// </code>
    public class XmlManifest2012ExtensionReader : XmlManifestReader
    {
        public override XNamespace BoxesNs
        {
            get { return "http://schemas.dbones.co.uk/developer/boxes/2012/extension"; }
        }

        public override Manifest ReadManifest(XElement manifestXml)
        {
            var manifest = (ExtensionManifest)base.ReadManifest(manifestXml);
            var extends = GetModules(manifestXml, "extends", "assembly");
            foreach(var extension in extends)
            {
                manifest.SetExtension(extension);
            }
            return manifest;
        }

        protected override Manifest CreateManifestInstance(string name, Version version, string description, IEnumerable<Module> exports, IEnumerable<Module> imports)
        {
            return new ExtensionManifest(name, version, description, exports, imports);
        }
    }
}