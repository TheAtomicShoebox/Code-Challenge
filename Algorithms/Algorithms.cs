using System;
using System.Linq;
using System.Text;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            int ret = n;
            for(int i = n - 1; i > 0; i--)
            {
                ret *= i;
            }

            return ret;
        }

        public static string FormatSeparators(params string[] items)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < items.Length - 1; i++)
            {
                sb.Append($"{items[i]}");

                // not really my favorite way of doing this.
                // can't really think of a more generic methodology, though

                if(i == items.Length - 2)
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(", ");
                }
            }

            sb.Append($"and {items[items.Length - 1]}");

            return sb.ToString();
        }
    }
}