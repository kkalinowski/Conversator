﻿using System;
using System.Globalization;
using lib12.WPF.Converters;

namespace Conversator
{
    public class ContentConverter : StaticConverter<ContentConverter>
    {
        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
                return "Thinking...";
            else
                return "Say";
        }
    }
}