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
namespace Boxes.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// some type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// returns of the current type is equal to the generic
        /// </summary>
        /// <typeparam name="TService">compare the type to this</typeparam>
        /// <param name="type">the type to compare</param>
        public static bool Is<TService>(this Type type)
        {
            return typeof(TService) == type;
        }


        private static readonly IDictionary<Type, IEnumerable<Type>> _allInterfacesForType = new Dictionary<Type, IEnumerable<Type>>();

        /// <summary>
        /// Get all the interfaces this class implements
        /// </summary>
        /// <param name="type">the type of interest</param>
        public static IEnumerable<Type> AllInterfaces(this Type type)
        {
            if (type == null)
            {
                return new Type[0];
            }

            IEnumerable<Type> cached;
            if (_allInterfacesForType.TryGetValue(type, out cached))
            {
                return cached;
            }

            HashSet<Type> interfaces = new HashSet<Type>();
            if (type.IsInterface)
            {
                interfaces.Add(type);
            }

            var types = type.GetInterfaces();
            for (int i = 0; i < types.Length; i++)
            {
                var iface = types[i];
                interfaces.AddRange(iface.AllInterfaces());
            }
            _allInterfacesForType.Add(type, interfaces);
            return interfaces;
        }

        /// <summary>
        /// The first interface a type implements
        /// </summary>
        /// <param name="type">the type of interest</param>
        public static Type FirstInterface(this Type type)
        {
            return type.GetInterfaces().FirstOrDefault();
        }

        /// <summary>
        /// Gets all the interfaces this type implements, it also adds the type of interest to the result set
        /// </summary>
        /// <param name="type">the type of interest</param>
        public static IEnumerable<Type> SelfAndAllInterfaces(this Type type)
        {
            HashSet<Type> result = new HashSet<Type>(type.GetInterfaces());
            if (!type.IsInterface && type.IsClass)
            {
                result.Add(type);
            }
            return result;
        }
    }

    /// <summary>
    /// couple of string extensions
    /// </summary>
    public static class StringExtension
    {

        /// <summary>
        /// format the string, "{0} world".FormatWith("Hello");
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }

}