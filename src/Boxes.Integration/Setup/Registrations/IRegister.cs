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
namespace Boxes.Integration.Setup.Registrations
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class provides a mechanism to setup the registration of types with the underlying IoC.
    /// </summary>
    public interface IRegister 
    {
        /// <summary>
        /// meta information about the registration
        /// </summary>
        RegistrationMeta RegistrationMeta { get; }
    }

    /// <summary>
    /// This class provides a mechanism to setup the registration of types with the underlying IoC.
    /// </summary>
    /// <typeparam name="TScope">The signature for the scope/lifestyle</typeparam>
    /// <typeparam name="TConfiguration">the IoC direct configuration for the current registration</typeparam>
    public interface IRegister<TScope, TConfiguration> : IRegister
    {
        /// <summary>
        /// apply a filter (pattern) to find which types this registration will apply too
        /// </summary>
        /// <param name="where">the pattern to find the types this registration applies too</param>
        IRegister<TScope, TConfiguration> Where(Predicate<Type> where);
        
        /// <summary>
        /// which contracts to associate the current type with
        /// </summary>
        /// <param name="contracts">select the appropriate contracts to register this class with</param>
        IRegister<TScope, TConfiguration> AssociateWith(Contracts contracts);

        /// <summary>
        /// which contracts to associate the current type with
        /// </summary>
        /// <param name="contracts">provide a list of contracts to associate the current type with</param>
        IRegister<TScope, TConfiguration> AssociateWith(Func<Type, IEnumerable<Type>> contracts);

        /// <summary>
        /// which contracts to associate the current type with
        /// </summary>
        /// <param name="contracts">provide a list of contracts to associate the current type with</param>
        IRegister<TScope, TConfiguration> AssociateWith(IEnumerable<Type> contracts);
        
        /// <summary>
        /// supply a ctor to use, apply this in the Configure Method, as this will allow direct access
        /// to the actual IoC's registration
        /// </summary>
        /// <param name="factoryMethod">the ctor to use</param>
        [Obsolete("not all IoC's support this", true)]
        IRegister<TScope, TConfiguration> Ctor(Func<object> factoryMethod);
        
        /// <summary>
        /// the life style of the type (scope and lifestyle are considered to be the same thing at this point)
        /// </summary>
        /// <param name="lifeStyle">the life style to use</param>
        IRegister<TScope, TConfiguration> LifeStyle(TScope lifeStyle);

        /// <summary>
        /// the scope of the type (scope and lifestyle are considered to be the same thing at this point)
        /// </summary>
        /// <param name="scope">the scope to use</param>
        IRegister<TScope, TConfiguration> Scope(TScope scope);
        
        /// <summary>
        /// direct access to the IoC's registration, Recommended for the more advanced setup's
        /// </summary>
        /// <param name="cfg">ioc's registration</param>
        IRegister<TScope, TConfiguration> Configure(Action<RegisterContext<TConfiguration>> cfg);
    }
}