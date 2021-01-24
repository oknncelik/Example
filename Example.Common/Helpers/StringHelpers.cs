namespace Example.Common.Helpers
{
    public static class StringHelpers
    {
        public static bool IsNotEmpty(this string value)
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

        public static string TrToEng(this string text)
        {
            try
            {
                text = text.Replace('ç', 'c');
                text = text.Replace('Ç', 'C');
                text = text.Replace('ğ', 'g');
                text = text.Replace('Ğ', 'G');
                text = text.Replace('ı', 'i');
                text = text.Replace('İ', 'I');
                text = text.Replace('ş', 's');
                text = text.Replace('Ş', 'S');
                text = text.Replace('ö', 'o');
                text = text.Replace('Ö', 'O');
                text = text.Replace('ü', 'u');
                text = text.Replace('Ü', 'U');
                return text;
            }
            catch
            {
                return text;
            }
        }
    }
}