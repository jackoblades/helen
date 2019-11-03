using System;

namespace Helen.Term
{
    public static class Terminal
    {
        static int PaddingLength = 75;

        public static void WriteLine(string s = "", char pad = ' ')
        {
            Console.WriteLine(s.PadRight(PaddingLength, pad));
        }
    }
}