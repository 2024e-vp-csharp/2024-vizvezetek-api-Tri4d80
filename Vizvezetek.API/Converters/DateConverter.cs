// Converters/DateConverter.cs
using System;

namespace Vizvezetek.API.Converters
{
    public static class DateConverter
    {
        public static DateTime DateOnlyToDateTime(DateOnly dateOnly)
        {
            return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
        }
    }
}
