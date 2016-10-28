/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection;

namespace FileSplitter.Attributes {
    /// <summary>
    /// Class to help out with units
    /// </summary>
    /// <created>Nick</created>
    public class UnitAttribute : Attribute {

        /// <summary>
        /// The step size to factor computer sizes
        /// B => KiB => MiB => GiB => TiB => PiB
        /// </summary>
        /// <created>Nick</created>
        internal const int ComputerSizeFactorStep = 1024;

        /// <summary>
        /// The way to identify the unit from the command-line
        /// </summary>
        /// <created>Nick</created>
        public string Identifier { get; private set; }

        /// <summary>
        /// The computer-size factor to use when translating units
        /// </summary>
        /// <created>Nick</created>
        public byte Factor { get; private set; }

        /// <summary>
        /// Cached and calculated property that holds the value that the unit represents
        /// </summary>
        /// <created>Nick</created>
        public double CalculatedFactor { get; private set; }

        /// <summary>
        /// Allows you to specify a textual key and factor to indentify a SplitUnit
        /// </summary>
        /// <param name="identifier">The textual identifier to match against later</param>
        /// <param name="factor">The step to use to calculate the meaning</param>
        /// <created>Nick</created>
        public UnitAttribute(string identifier, byte factor) {
            Identifier = identifier;
            Factor = factor;
            CalculatedFactor = Math.Pow(ComputerSizeFactorStep, Factor);
        }

        /// <summary>
        /// Finds an enum value with the specified identifier
        /// </summary>
        /// <typeparam name="T">The enum to find a value that holds the attribute</typeparam>
        /// <param name="identifier">The identifier field of the attribute to retireve</param>
        /// <returns>an enum value with the specified identifier</returns>
        /// <created>Nick</created>
        public static T Parse<T>(string identifier) {
            Type type = typeof(T);
            UnitAttribute toTest;
            // Enumerate all public static fields
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static)) {
                // Check each attribute
                foreach (var attribute in field.GetCustomAttributes(false)) {
                    // Get the type of the attribute
                    Type attributeType = attribute.GetType();
                    // And verify that 
                    if (attributeType == typeof(UnitAttribute)) {
                        toTest = attribute as UnitAttribute;
                        if (toTest.Identifier == identifier) {
                            return (T)field.GetValue(null);
                        }
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// Retrieves the attribute (if available) that was set on an enum field
        /// </summary>
        /// <typeparam name="T">The enum to check</typeparam>
        /// <param name="value">The enum value to get the attribute off of</param>
        /// <returns>the attribute (if available) that was set on an enum field</returns>
        /// <created>Nick</created>
        public static UnitAttribute GetFromField<T>(T value) {
            Type type = typeof(T);
            string enumPropertyNameToTest = value.ToString();
            // Enumerate all public static fields
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static)) {
                if (field.Name == enumPropertyNameToTest) {
                    // Check each attribute
                    foreach (var attribute in field.GetCustomAttributes(false)) {
                        // Get the type of the attribute
                        Type attributeType = attribute.GetType();
                        // And verify that 
                        if (attributeType == typeof(UnitAttribute)) {
                            return attribute as UnitAttribute;
                        }
                    }
                }
            }
            return null;
        }
    }
}