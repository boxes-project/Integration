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
    [Obsolete("Use Register, this will be removed, very soon")]
    public class Registration : IRegister
    {
        public Registration()
        {
            RegistrationMeta = new RegistrationMeta();
        }

        public RegistrationMeta RegistrationMeta { get; private set; }

        public Registration RegisterWith(Contracts with)
        {
            switch (with)
            {
                case Contracts.AllInterfaces:
                    RegistrationMeta.With = type => type.AllInterfaces();
                    break;
                case Contracts.FirstInterface:
                    RegistrationMeta.With = type => new[] { (type.FirstInterface() ?? type) };
                    break;
                case Contracts.SelfOnly:
                    RegistrationMeta.With = type => new[] { type };
                    break;
                case Contracts.SelfAndAllInterfaces:
                    RegistrationMeta.With = type => type.SelfAndAllInterfaces();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("with");
            }
            return this;
        }

        public Registration RegisterWith(Func<Type, IEnumerable<Type>> registerWith)
        {
            RegistrationMeta.With = registerWith;
            return this;
        }

        public Registration RegisterWith(IEnumerable<Type> registerWith)
        {
            RegistrationMeta.With = type => registerWith;
            return this;
        }

        public Registration Where(Predicate<Type> where)
        {
            RegistrationMeta.Where += where;
            return this;
        }

        public Registration Ctor(Func<object> factoryMethod)
        {
            RegistrationMeta.FactoryMethod = factoryMethod;
            return this;
        }

        public Registration LifeStyle<TLifeStyle>()
        {
            LifeStyle(typeof (TLifeStyle));
            return this;
        }

        public Registration LifeStyle(object lifeStyle)
        {
            RegistrationMeta.LifeStyle = lifeStyle;
            return this;
        }

        public Registration Configure<TConfiguration>(Action<TConfiguration> cfg)
        {
            RegistrationMeta.Configurations.Add(o => cfg((TConfiguration)o));
            return this;
        }
    }
}