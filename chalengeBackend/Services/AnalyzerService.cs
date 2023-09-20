namespace ChalengeBackend.Services
{

    using System;

    public class AnalyzerService
    {
        public static bool TryReverse(uint number, out uint reversedNumber)
        {
            reversedNumber = 0;

            // Check if the input number is a valid unsigned integer
            if (number <= uint.MaxValue)
            {
                // Convert the number to a string and reverse it
                char[] charArray = number.ToString().ToCharArray();
                Array.Reverse(charArray);
                string reversedNumberString = new string(charArray);

                // Convert the reversed string back to an unsigned integer
                if (uint.TryParse(reversedNumberString, out reversedNumber))
                {
                    return true;
                }
            }

            return false;
        }
    }
}