namespace Example.Common.Helpers
{
    public static class StringHelpers
    {
        public static bool IsNotEmpity(this string value)
        {
            try
            {
                return value != null && value.Trim().Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
