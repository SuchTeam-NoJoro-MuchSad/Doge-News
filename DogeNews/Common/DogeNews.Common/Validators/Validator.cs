using System;

namespace DogeNews.Common.Validators
{
    public class Validator
    {
        public static void ValidateThatObjectIsNotNull(object obj, string objectName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(objectName);
            }
        }

        public static void ValidateThatStringIsNotNullOrEmpty(string str, string objectName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(objectName);
            }
        }

        public static void ValidateThatNumberIsNotPositive(double number, string valueName)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }
        }
    }
}
