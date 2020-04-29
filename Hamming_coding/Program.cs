using System;
using System.Linq;
using System.Text;

namespace Hamming_coding
{
    class Program
    {
        static void Main()
        {
            string str = "";
            string sk = "";
            string example = "";

            Console.WriteLine("Введите строку символов (латиница, макс. 6):");

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (str.Length > 0)
                        {
                            str = str.Remove(str.Length - 1, 1);

                            Console.Write(key.KeyChar + " " + key.KeyChar);
                        }

                        break;
                    default:
                        if ((key.KeyChar <= 122) && (key.KeyChar >= 48) && str.Length < 6)
                        {
                            Console.Write(key.KeyChar);

                            str += key.KeyChar;
                        }

                        break;
                }
            }
            while (key.KeyChar != 13);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(str))
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            str = sb.ToString();

            Console.WriteLine("\n\nИсходная комбинация:\n" + str + "\n");

            example = str;
            example = example.Insert(0, "X").Insert(1, "X");

            sk = str;
            str = str.Insert(0, "0").Insert(1, "0");
            int numBit = 2;

            for (int i = 4, j = 3; i < str.Length; i = (int)Math.Pow(2, j), j++)
            {
                numBit++;

                str = str.Insert(i - 1, "0");
                example = example.Insert(i - 1, "X");
            }

            Console.WriteLine(example);

            string s1 = null, s2 = null, s3 = null, s4 = null, s5 = null, s6 = null;

            int x = str.Length;

            for (int i = 0; i < str.Length / 2; i++)
            {
                s1 += "0";
            }

            for (int i = 0; i < str.Length; i += 2)
            {
                s1 = s1.Insert(i, "1");
            }

            if (numBit >= 1)
            {
                for (int i = 0; i < str.Length / 2; i++)
                {
                    s2 += "0";
                }

                for (int i = 1; i < str.Length; i += 4)
                {
                    s2 = s2.Insert(i, "11");
                }

                if (s2.Length > s1.Length)
                {
                    s2 = s2.Remove(s2.Length - 1);
                }

                else if (s2.Length < s1.Length)
                {
                    s2 += "0";
                }
            }

            if (numBit >= 3)
            {
                s3 = str;
                s3 = s3.Replace('1', '0');

                for (int i = 3; i < str.Length; i += 8)
                {
                    s3 = s3.Insert(i, "1111");
                }

                if (s3.Length > s2.Length)
                {
                    int raznica = s3.Length - s2.Length;
                    s3 = s3.Remove(s3.Length - raznica);
                }
            }

            if (numBit >= 4)
            {
                s4 = str;
                s4 = s4.Replace('1', '0');

                for (int i = 7; i < str.Length; i += 16)
                {
                    s4 = s4.Insert(i, "11111111");
                }

                if (s4.Length > s3.Length)
                {
                    int raznica = s4.Length - s3.Length;
                    s4 = s4.Remove(s4.Length - raznica);
                }
            }

            if (numBit >= 5)
            {
                s5 = str;
                s5 = s5.Replace('1', '0');

                for (int i = 15; i < str.Length; i += 32)
                {
                    s5 = s5.Insert(i, "1111111111111111");
                }

                if (s5.Length > s4.Length)
                {
                    int raznica = s5.Length - s4.Length;
                    s5 = s5.Remove(s5.Length - raznica);
                }
            }

            if (numBit >= 6)
            {
                s6 = str;
                s6 = s6.Replace('1', '0');

                for (int i = 23; i < str.Length; i += 64)
                {
                    s6 = s6.Insert(i, "11111111111111111111111111111111");
                }

                if (s6.Length > s5.Length)
                {
                    int raznica = s6.Length - s5.Length;
                    s6 = s6.Remove(s6.Length - raznica);
                }
            }

            int[] sBoss = str.Select(ch => int.Parse(ch.ToString())).ToArray();
            int[] sBoss1 = new int[0];
            int[] sBoss2 = new int[0];
            int[] sBoss3 = new int[0];
            int[] sBoss4 = new int[0];
            int[] sBoss5 = new int[0];
            int[] sBoss6 = new int[0];

            if (numBit > 0)
            {
                sBoss1 = s1.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            if (numBit >= 1)
            {
                sBoss2 = s2.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            if (numBit >= 3)
            {
                sBoss3 = s3.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            if (numBit >= 4)
            {
                sBoss4 = s4.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            if (numBit >= 5)
            {
                sBoss5 = s5.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            if (numBit >= 6)
            {
                sBoss6 = s6.Select(ch => int.Parse(ch.ToString())).ToArray();
            }

            string[] tem = new string[numBit];

            int temp = 0;

            for (int i = 0; i < str.Length; i++)
            {
                temp += sBoss[i] * sBoss1[i];
            }

            tem[0] = temp > 1 ? Convert.ToString(temp % 2) : Convert.ToString(temp);

            Console.WriteLine("\nr1 = {0} ", tem[0]);

            int temp2 = 0;

            if (numBit >= 1)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp2 += sBoss[i] * sBoss2[i];
                }

                tem[1] = temp2 > 1 ? Convert.ToString(temp2 % 2) : Convert.ToString(temp2);

                Console.WriteLine("r2 = {0} ", tem[1]);
            }

            int temp3 = 0;

            if (numBit >= 3)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp3 += sBoss[i] * sBoss3[i];
                }

                tem[2] = temp3 > 1 ? Convert.ToString(temp3 % 2) : Convert.ToString(temp3);

                Console.WriteLine("r3 = {0} ", tem[2]);
            }

            int temp4 = 0;

            if (numBit >= 4)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp4 += sBoss[i] * sBoss4[i];
                }

                tem[3] = temp4 > 1 ? Convert.ToString(temp4 % 2) : Convert.ToString(temp4);

                Console.WriteLine("r4 = {0} ", tem[3]);
            }

            int temp5 = 0;

            if (numBit >= 5)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp5 += sBoss[i] * sBoss5[i];
                }

                tem[4] = temp5 > 1 ? Convert.ToString(temp5 % 2) : Convert.ToString(temp5);

                Console.WriteLine("r5 = {0} ", tem[4]);
            }

            int temp6 = 0;

            if (numBit >= 6)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp6 += sBoss[i] * sBoss6[i];
                }

                tem[5] = temp6 > 1 ? Convert.ToString(temp6 % 2) : Convert.ToString(temp6);

                Console.WriteLine("r6 = {0} ", tem[5]);
            }

            for (int i = 1, j = 1, k = 0; i < sk.Length; i = (int)Math.Pow(2, j), j++, k++)
            {
                sk = sk.Insert(i - 1, tem[k]);
            }

            Console.WriteLine("\n" + sk);

            str = "";
            string was = "";

            Console.WriteLine("\nВведите принятую комбинацию: ");

            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (str.Length > 0)
                        {
                            str = str.Remove(str.Length - 1, 1);

                            Console.Write(key.KeyChar + " " + key.KeyChar);
                        }

                        break;
                    default:
                        if ((key.KeyChar <= 49) && (key.KeyChar >= 48))
                        {
                            Console.Write(key.KeyChar);

                            str += key.KeyChar;
                        }

                        break;
                }
            }
            while (key.KeyChar != 13);

            was = str;
            int[] sBossDecode = str.Select(ch => int.Parse(ch.ToString())).ToArray();
            temp = 0;

            for (int i = 0; i < str.Length; i++)
            {
                temp += sBossDecode[i] * sBoss1[i];
            }

            tem[0] = temp > 1 ? Convert.ToString(temp % 2) : Convert.ToString(temp);

            Console.WriteLine("\n\nr1 = {0} ", tem[0]);

            if (numBit >= 1)
            {
                temp2 = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    temp2 += sBossDecode[i] * sBoss2[i];
                }

                tem[1] = temp2 > 1 ? Convert.ToString(temp2 % 2) : Convert.ToString(temp2);

                Console.WriteLine("r2 = {0} ", tem[1]);
            }

            if (numBit >= 3)
            {
                temp3 = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    temp3 += sBossDecode[i] * sBoss3[i];
                }

                tem[2] = temp3 > 1 ? Convert.ToString(temp3 % 2) : Convert.ToString(temp3);

                Console.WriteLine("r3 = {0} ", tem[2]);
            }

            if (numBit >= 4)
            {
                temp4 = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    temp4 += sBossDecode[i] * sBoss4[i];
                }

                tem[3] = temp4 > 1 ? Convert.ToString(temp4 % 2) : Convert.ToString(temp4);

                Console.WriteLine("r4 = {0} ", tem[3]);
            }

            if (numBit >= 5)
            {
                temp5 = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    temp5 += sBossDecode[i] * sBoss5[i];
                }

                tem[4] = temp5 > 1 ? Convert.ToString(temp5 % 2) : Convert.ToString(temp5);

                Console.WriteLine("r5 = {0} ", tem[4]);
            }

            if (numBit >= 6)
            {
                temp6 = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    temp6 += sBossDecode[i] * sBoss6[i];
                }

                tem[5] = temp6 > 1 ? Convert.ToString(temp6 % 2) : Convert.ToString(temp6);

                Console.WriteLine("r6 = {0} ", tem[5]);
            }

            int plus = 0;

            if (tem[0] != "0")
            {
                plus += 1;
            }

            if (numBit >= 1)
            {
                if (tem[1] != "0")
                {
                    plus += 2;
                }
            }

            if (numBit >= 3)
            {
                if (tem[2] != "0")
                {
                    plus += 4;
                }
            }

            if (numBit >= 4)
            {
                if (tem[3] != "0")
                {
                    plus += 8;
                }
            }

            if (numBit >= 5)
            {
                if (tem[4] != "0")
                {
                    plus += 16;
                }
            }

            if (numBit >= 6)
            {
                if (tem[5] != "0")
                {
                    plus += 32;
                }
            }

            if (plus != 0)
            {
                Console.WriteLine("\nОшибка на {0} позиции.\n", plus);

                str = str[plus - 1] == '1' ? str.Remove(plus - 1, 1).Insert(plus - 1, "0") : str.Remove(plus - 1, 1).Insert(plus - 1, "1");
            }

            else
            {
                Console.WriteLine("\nОшибок нет.\n");
            }

            string initialCombination = "";

            int power = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if ((i + 1) == Math.Pow(2, power))
                {
                    power++;
                }

                else
                {
                    initialCombination += str[i];
                }
            }

            Console.WriteLine("Полученная комбинация:\n" + initialCombination + "\n");

            string split_codes = "";

            for (int i = 0; i < initialCombination.Length; i++)
            {
                split_codes += initialCombination[i];

                if ((i + 1) % 8 == 0)
                {
                    split_codes += " ";
                }
            }

            string[] codes = split_codes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            byte[] bytes = new byte[codes.Length];

            for (int i = 0; i < codes.Length; i++)
            {
                bytes[i] = Convert.ToByte(codes[i], 2);
            }

            string message = Encoding.ASCII.GetString(bytes);

            Console.WriteLine(message);
        }
    }
}
