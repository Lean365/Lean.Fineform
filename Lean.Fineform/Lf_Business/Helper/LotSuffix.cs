using System;

namespace LeanFine.Lf_Business.Helper
{
    public static class LotSuffix
    {
        public static string GetSpecialFormattedDate()
        {
            DateTime now = DateTime.Now;
            int month = now.Month;
            int yearLastTwoDigits = now.Year % 100;

            string monthCode;
            switch (month)
            {
                case 10:
                    monthCode = "X";
                    break;
                case 11:
                    monthCode = "Y";
                    break;
                case 12:
                    monthCode = "Z";
                    break;
                default:
                    monthCode = month.ToString();
                    break;
            }

            return $"{yearLastTwoDigits}{monthCode}";
        }
    }
}
