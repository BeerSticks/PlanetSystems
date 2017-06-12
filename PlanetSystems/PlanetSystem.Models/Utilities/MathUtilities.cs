using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Models.Utilities
{
    public static class MathUtilities
    {
        public static double ParseFromExpNotation(double number, int e)
        {
            var result = number * Math.Pow(10, e);
            return result;
        }

        public static void ParseToExpNotation(double source, out double target, out int e)
        {
            string text = $"{source}";
            text = text.ToLower();
            var indexOfE = text.IndexOf('e');
            if (indexOfE > 0)
            {
                string[] components = text.Split(new char[] { 'e' });
                target = double.Parse(components[0]);
                e = int.Parse(components[1]);
            }
            else
            {
                int indexOfDecimalMark = text.IndexOf('.');
                if (indexOfDecimalMark < 0)
                {
                    indexOfDecimalMark = text.Length;
                }
                int indexOfFirstNonZero = -1;
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsNumber(text[i]) && text[i] != '0')
                    {
                        indexOfFirstNonZero = i;
                        break;
                    }
                }

                if (indexOfFirstNonZero >= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (char.IsNumber(text[i]) && text[i] != '0')
                        {
                            sb.Append(text[i]);
                        }
                    }
                    sb.Insert(1, '.');
                    target = double.Parse(sb.ToString());
                    e = (indexOfDecimalMark - indexOfFirstNonZero) - 1;
                }
                else
                {
                    target = 0;
                    e = 0;
                }
            }
        }
    }
}
