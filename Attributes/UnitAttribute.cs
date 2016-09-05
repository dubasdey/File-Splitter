using System;
using System.Reflection;
namespace FileSplitter.Attributes
{
    /// <summary>
    /// Class to help out with units
    /// </summary>
    /// <created>Nick</created>
    public class UnitAttribute : System.Attribute
    {
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
        public UnitAttribute(string identifier, byte factor)
        {
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
        public static T Parse<T>(string identifier)
        {
            Type type = typeof(T);
            UnitAttribute toTest;
            // Enumerate all public static fields
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                // Check each attribute
                foreach (var attribute in field.GetCustomAttributes(false))
                {
                    // Get the type of the attribute
                    Type attributeType = attribute.GetType();
                    // And verify that 
                    if (attributeType == typeof(UnitAttribute))
                    {
                        toTest = attribute as UnitAttribute;
                        if (toTest.Identifier == identifier)
                        {
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
        public static UnitAttribute GetFromField<T>(T value)
        {
            Type type = typeof(T);
            string enumPropertyNameToTest = value.ToString();
            // Enumerate all public static fields
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.Name == enumPropertyNameToTest)
                {
                    // Check each attribute
                    foreach (var attribute in field.GetCustomAttributes(false))
                    {
                        // Get the type of the attribute
                        Type attributeType = attribute.GetType();
                        // And verify that 
                        if (attributeType == typeof(UnitAttribute))
                        {
                            return attribute as UnitAttribute;
                        }
                    }
                }
            }
            return null;
        }
    }
}