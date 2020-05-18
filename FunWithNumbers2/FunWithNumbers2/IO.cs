using System;
using System.Collections.Generic;
using System.Text;

namespace FunWithNumbers2

{
    class IO
    {

        public static int GetInput(string Message)
        {
            int value = 0;
            bool error;
            string userInput;
            string thisMessage = "That was not a number, Try again!";

            do
            {
                //Reset error value
                error = false;
                //Get user input for the value
                Console.WriteLine(Message);
                userInput = Console.ReadLine();

                try
                {

                    //return userInput as a integer Value
                    value = int.Parse(userInput);
                }
                catch
                {
                    //if error had occurred, clear screen and try again
                    Console.Clear();
                    Console.WriteLine(thisMessage[0]);
                    //set error state to true
                    error = true;
                }
                break;


                //loop while error == true
            } while (error == true);

            return value;

        }
    }
}
