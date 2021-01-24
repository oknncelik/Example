#region

using System;

#endregion

namespace Example.Common.Helpers
{
    public static class TypeCastHelpers
    {
        public static string ToStr(this object value)
        {
            try
            {
                return value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ToInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        public static int? ToNullableInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        public static decimal ToDecimal(this object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal? ToNullableDecimal(this object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return null;
            }
        }

        public static long ToLong(this object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }

        public static long? ToNullableLong(this object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDateTime(this object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ToNullableDateTime(this object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
    }
}