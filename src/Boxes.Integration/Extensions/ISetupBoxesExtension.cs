// Copyright 2012 - 2013 dbones.co.uk (David Rundle)
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
    /// <summary>
    /// this is the class which will setup a <see cref="IBoxesExtensionWithSetup"/>
    /// </summary>
    public interface ISetupBoxesExtension<in TConfigure> where TConfigure : IBoxesExtensionWithSetup
    {
        /// <summary>
        /// this allows the setup to refine which extension it will extend, in the event where
        /// the generic parameter does not provide a concrete class, thus provides limited filtering.
        /// </summary>
        /// <param name="extension">the extension to be configured</param>
        /// <returns>true if this setup will configure the extension</returns>
        bool CanHandle(TConfigure extension);

        /// <summary>
        /// setup the boxes extension
        /// </summary>
        /// <param name="extension">the extension to setup</param>
        void Configure(TConfigure extension);
    }
}