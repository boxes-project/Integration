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
namespace Boxes.Integration.Exceptions
{
    using System;
    using Factories;

    /// <summary>
    /// there is no <see cref="ICreateLoader"/> registered for this type
    /// </summary>
    public class CreateLoaderException : Exception
    {
        /// <summary>
        /// the loader which requires a creator
        /// </summary>
        public Type LoaderType { get; private set; }

        public CreateLoaderException(Type loaderType)
        {
            LoaderType = loaderType;
        }

        public override string Message
        {
            get { return "{0} - has no ICreateLoader registered ".FormatWith(LoaderType); }
        }
    }
}