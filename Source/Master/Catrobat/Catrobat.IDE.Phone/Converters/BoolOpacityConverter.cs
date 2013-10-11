﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Catrobat.IDE.Phone.Converters
{
    public class BoolOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return Visibility.Collapsed;

            var visible = (bool) value;

            if (parameter != null && (bool) parameter)
            {
                visible = !(bool) value;
            }

            return visible ? 1.0 : 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}