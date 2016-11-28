using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OptionsAnalyzerApp.Models
{
    #region EnumExtensions
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
    #endregion

    #region Attributes
    #region DisplayAttribute
    public class EnumDisplayAttribute : Attribute
    {
        internal EnumDisplayAttribute(string display)
        {
            this.Display = display;
        }
        public String Display { get; private set; }
    }
    #endregion 

    #region PluralDisplayAttribute
    public class EnumPluralDisplayAttribute : Attribute
    {
        internal EnumPluralDisplayAttribute(string display)
        {
            this.Display = display;
        }
        public String Display { get; private set; }
    }
    #endregion 
    #endregion

    #region OptionTypes
    public enum OptionTypes
    {
        [EnumDisplay("None")]
        None = 0,

        [EnumDisplay("Call")]
        [EnumPluralDisplay("Calls")]
        Call,

        [EnumDisplay("Put")]
        [EnumPluralDisplay("Puts")]
        Put
    }
    #endregion
}
