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
    /// <remarks>
    /// note this is abstract, it is inherited by the container implementation, 
    /// this was to make the API easier to work with 
    /// IE new Register() VS new Register[Type, WindsorContainer]() 
    /// the latter is not friendly to do
    /// </remarks>
    public abstract class RegisterBase<TScope, TConfiguration> : IRegister<TScope, TConfiguration>
    {
        protected RegistrationMeta _meta = new RegistrationMeta();

        public RegistrationMeta RegistrationMeta { get { return _meta; } }

        public virtual IRegister<TScope, TConfiguration> Where(Predicate<Type> where)
        {
            _meta.Where += where;
            return this;
        }

        public IRegister<TScope, TConfiguration> AssociateWith(Contracts contracts)
        {
            switch (contracts)
            {
                case Contracts.AllInterfaces:
                    _meta.With = type => type.AllInterfaces();
                    break;
                case Contracts.FirstInterface:
                    _meta.With = type => new[] { (type.FirstInterface() ?? type) };
                    break;
                case Contracts.SelfOnly:
                    _meta.With = type => new[] { type };
                    break;
                case Contracts.SelfAndAllInterfaces:
                    _meta.With = type => type.SelfAndAllInterfaces();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("contracts");
            }
            return this;
        }

        public IRegister<TScope, TConfiguration> AssociateWith(Func<Type, IEnumerable<Type>> contracts)
        {
            _meta.With = contracts;
            return this;
        }

        public IRegister<TScope, TConfiguration> AssociateWith(IEnumerable<Type> contracts)
        {
            _meta.With = type => contracts;
            return this;
        }


        public IRegister<TScope, TConfiguration> Ctor(Func<object> factoryMethod)
        {
            _meta.FactoryMethod = factoryMethod;
            return this;
        }

        public IRegister<TScope, TConfiguration> LifeStyle(TScope lifeStyle)
        {
            _meta.LifeStyle = lifeStyle;
            return this;
        }

        public IRegister<TScope, TConfiguration> Scope(TScope scope)
        {
            _meta.LifeStyle = scope;
            return this;
        }

        public IRegister<TScope, TConfiguration> Configure(Action<RegisterContext<TConfiguration>> cfg)
        {
            _meta.Configurations.Add(o => cfg((RegisterContext<TConfiguration>)o));
            return this;
        }
    }
}