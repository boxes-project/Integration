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
namespace Boxes.Integration.Factories
{
    /// <summary>
    /// A Setup to use with the IoC (this should be injected into the <see cref="IIocFactory{TBuilder,TContainer}"/>)
    /// </summary>
    /// <typeparam name="TBuilder">the IoC setup</typeparam>
    public interface IIocSetup<in TBuilder>
    {
        /// <summary>
        /// configure the main container
        /// </summary>
        /// <param name="builder">the main container builder, (this should be before any registrations)</param>
        void Configure(TBuilder builder);

        /// <summary>
        /// configure a child container
        /// </summary>
        /// <param name="builder">the child builder, (this should be before any registrations)</param>
        void ConfigureChild(TBuilder builder);
    }

    /// <summary>
    /// does nothing, vanilla setup of the ioc
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public class IocSetup<TBuilder> : IIocSetup<TBuilder>
    {
        public void Configure(TBuilder builder)
        {
            
        }

        public void ConfigureChild(TBuilder builder)
        {
            
        }
    }
}