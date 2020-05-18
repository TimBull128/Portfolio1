using System;
using System.Collections.Generic;
using System.Text;

namespace FunWithNumbers2
{
    class LoopCount
    {
        public static void YeildLoop(int loop)
        {

            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append("The yeild loop is: ");
            stringBuilder.Append(loop.ToString());

            Console.WriteLine(stringBuilder);
            stringBuilder.Clear();


            Console.WriteLine("Whoo I'm tired... I'll just end this here");
            return;
        }
        public static void YeildLoop(int loop, int sum, TypeofFunction Type)
        {

            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append("The yeild loop is: ");
            stringBuilder.Append(loop.ToString());
            Console.WriteLine(stringBuilder);
            stringBuilder.Clear();


            int ans;
            switch (Type)
            {
                case TypeofFunction.Add:
                    ans = loop + sum;

                    stringBuilder.Append("The additon " + loop.ToString() + " + " + sum.ToString() + " is: ");
                    stringBuilder.Append(ans.ToString());

                    Console.WriteLine(stringBuilder);
                    stringBuilder.Clear();
                    Console.WriteLine();
                    break;

                case TypeofFunction.Sub:
                    ans = loop - sum;
                    stringBuilder.Append("The Subtraction " + loop.ToString() + " - " + sum.ToString() + " is: ");
                    stringBuilder.Append(ans.ToString());

                    Console.WriteLine(stringBuilder);
                    stringBuilder.Clear();
                    Console.WriteLine();
                    break;

                case TypeofFunction.Mult:
                    ans = loop * sum;

                    stringBuilder.Append("The Multiplication " + loop.ToString() + " x " + sum.ToString() + " is: ");
                    stringBuilder.Append(ans.ToString());

                    Console.WriteLine(stringBuilder);
                    stringBuilder.Clear();
                    Console.WriteLine();
                    break;

                case TypeofFunction.Divide:
                    try
                    {
                        ans = loop / sum;
                        stringBuilder.Append("The Division" + loop + "/" + sum + " is: ");
                        stringBuilder.Append(ans.ToString());

                        Console.WriteLine(stringBuilder);
                        stringBuilder.Clear();
                        Console.WriteLine();
                    }
                    catch
                    {
                        Console.WriteLine("The division is infinite");
                        Console.WriteLine();
                    }
                    break;
            }

        }
    }
}
