using System;
using System.Collections.Generic;

namespace Haceau.Bilibili
{
    public static class AvToBv
    {
        private static string table = "fZodR9XQDSUm21yCkr6zBqiveYah8bt4xsWpHnJE7jL5VG3guMTKNPAwcF";
        private static Dictionary<char, ulong> tr = new Dictionary<char, ulong>();
        private static List<int> s = new List<int>() { 11, 10, 3, 8, 4, 6 };
        private static ulong xor = 177451812;
        private static ulong add = 8728348608;

        public static string ToBv(ulong input)
        {
            for (ulong i = 0; i < 58; ++i)
                tr[table[(int)i]] = i;
            input = (input ^ xor) + add;
            List<char> r = new List<char>(){ 'B', 'V', '1', 'n', 'n', '4', 'n', '1', 'n', '7', 'n', 'n' };
            for (int i = 0; i < 6; ++i)
                r[s[i]] = table[(int)(input / (ulong)Math.Pow(58, i) % 58)];
            return string.Join("", r);
        }

        public static ulong ToAv(string input)
        {
            for (ulong i = 0; i < 58; ++i)
                tr[table[(int)i]] = i;
            ulong r = 0;
            for (int i = 0; i < 6; ++i)
                r += tr[input[s[i]]] * (ulong)Math.Pow(58, i);
            return (r - add) ^ xor;
        }
    };

    class Start
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var argument in args)
                {
                    try
                    {
                        if (argument.Substring(0, 2).ToLower() == "av")
                            Console.WriteLine($"{argument}:{AvToBv.ToBv(ulong.Parse(argument.Substring(2)))}");
                        else
                            Console.WriteLine($"{argument}:av{AvToBv.ToAv(argument)}");
                    }
                    catch
                    {
                        Console.WriteLine($"Error: {argument}");
                    }
                }
                return;
            }
            Console.WriteLine("Haceau AvToBv Calculator v1.0.0");
            while (true)
            {
                Console.WriteLine("请输入av或bv号");
                Console.Write(">>> ");
                string input = Console.ReadLine();
                string result;
                try
                {
                    if (input.Substring(0, 2).ToLower() == "av")
                        result = AvToBv.ToBv(ulong.Parse(input.Substring(2)));
                    else
                        result = "av" + AvToBv.ToAv(input).ToString();
                }
                catch
                {
                    return;
                }
                Console.WriteLine(result);
            }
        }
    }
}