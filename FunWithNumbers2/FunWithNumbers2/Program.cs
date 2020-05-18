using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


namespace FunWithNumbers2
{
    class Program
    {

        static void Main(string[] args)
        {

            //declare variables
            int comparison, counter, sum, computation, loops;
            DateTime dateTime = DateTime.Now;
            StringBuilder stringBuilder = new StringBuilder();

            bool done;
            //Populate message array
            string[] message = new string[6];
            message[0] = "What is the starting number?";
            message[1] = "How many Loops?";
            message[2] = "What is the number that you want to use for the sums?";


            stringBuilder.Append("The date is: ");
            stringBuilder.Append(dateTime.ToShortDateString());
            message[3] = stringBuilder.ToString(); //message becomes what was added
            stringBuilder.Clear();

            stringBuilder.Append("The time is: ");
            stringBuilder.Append(dateTime.ToShortTimeString());
            message[4] = stringBuilder.ToString(); //message becomes what was added
            stringBuilder.Clear();

            message[5] = "Go again?";
            /////////////////////////////////////
            //Initial message
            Console.WriteLine("Hello!");
            Console.WriteLine(message[3]);
            Console.WriteLine(message[4]);
            stringBuilder.Clear();
            do
            {
                done = false;
                //IO.IO.GetInput is declared later - get the inputs for the values
                computation = IO.GetInput(message[0]);

                do
                {
                    loops = IO.GetInput(message[1]);
                    if (loops < 0)
                    {
                        stringBuilder.Append(loops.ToString());
                        stringBuilder.Append(" is an invalid number, input a number equal to or above 0");
                        Console.WriteLine(stringBuilder.ToString());
                        stringBuilder.Clear();
                        Console.ReadLine();
                        Console.Clear();
                    }
                } while (loops < 0);
                sum = IO.GetInput(message[2]);

                //set variables
                comparison = computation;           //Sets a static value for "computation" to evaluate against
                counter = computation + loops;      //Sets the "counter" to the value starting from point "computation" looping "loops" times

                //LoopCount.YeildLoop Type values: Mult, Add, Sub, Divide
                //Also overloaded LoopCount.YeildLoop calls default message
                while (computation < counter)
                {
                    if (computation % 2 == 0 | computation % 3 == 0)
                    {
                        Console.WriteLine("This only show when the number is divisble by 2 or 3");
                        LoopCount.YeildLoop(computation, sum, TypeofFunction.Sub);

                    }

                    if (computation % 4 == 0 | computation % 5 == 0)
                    {
                        Console.WriteLine("This only shows when the number is divisble by 4 or 5");
                        LoopCount.YeildLoop(computation, sum, TypeofFunction.Add);
                    }
                    if (computation % 6 == 0)
                    {
                        Console.WriteLine("This only shows when the number is divisble by 6");
                        LoopCount.YeildLoop(computation, sum, TypeofFunction.Divide);
                    }
                    if (computation % 7 == 0)
                    {
                        Console.WriteLine("This only shows on a 7");
                        LoopCount.YeildLoop(computation, sum, TypeofFunction.Mult);
                    }
                    if (computation == comparison + 100)
                    {
                        LoopCount.YeildLoop(computation);

                        break;
                    }

                    computation++;


                }
                stringBuilder.Clear();
                Console.WriteLine("You'll note that I have skipped parts for you...");
                string userInput;
                bool tryAgain = false;
                Console.WriteLine("Another try?");
                do
                {
                    userInput = Console.ReadLine();
                    switch (userInput.ToUpper())
                    {
                        case "YES":
                            done = true;
                            tryAgain = false;
                            break;
                        case "NO":
                            done = false;
                            tryAgain = false;
                            break;
                        default:
                            Console.WriteLine("Answer not Recongised - Yes or No ony");
                            tryAgain = true;
                            break;
                    }
                } while (tryAgain);

            } while (!done);
            Console.ReadLine();
            Environment.Exit(0);


        }




        //Function gets the user input - and loops if input is incorrect format
        //Returns the input in value as integer


    }
    enum TypeofFunction
    {
        Add,
        Sub,
        Mult,
        Divide,

    }
    enum Answers
    {
        Yes,
        No
    }
}

