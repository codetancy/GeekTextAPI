using System.Text.RegularExpressions;

namespace Web.Constants
{
    public static class RegularExpressions
    {
        public static readonly Regex PhoneRegex =
            new Regex("^(\\+\\d{1,2}\\s)?\\d{3}-\\d{3}-\\d{4}", RegexOptions.Compiled);
    }
}
