using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using System.Windows.Data;
using TaskMenager.Data.Entities;

namespace TaskMenager.Utils
{
    internal class StatusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
