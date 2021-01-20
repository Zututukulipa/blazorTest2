using System;
using System.Linq;
using System.Text;

namespace ElsaWebApp.Utils
{
    public class StringUtils
    {
        public static string RemoveDiacritics(string inputText)
        {
            var stringFormD = inputText.Normalize(NormalizationForm.FormD);
            var retVal = new StringBuilder();
            for (int letter = 0; letter < stringFormD.Length; ++letter)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[letter]) !=
                    System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[letter]);
            }

            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ScrambleLetters(string inputString)
        {
            return new string(inputString.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}