using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CthulhuTechDice
{
    class Status
    {
        //Constant string for Status message
        private const string Init = "Status: ";
        public bool DiceRolled { get; set; }

        /// <summary>
        /// Functions to update the status line
        /// </summary>
        /// <param name="Types">The types of entry: InvalidEntry = Where the entry is not a number, Fine = Where the system is fine, DiceRolled = Where the dice have rolled successfully </param>
        /// <returns>Status message for the program</returns>
        public string UpdateStatus(StatusTypes Types)
        {
            
            string StatusMessage = "";

            switch (Types)
            {
                case StatusTypes.InvalidEntry:
                    StatusMessage = Init + "Error: Non-Digit number entered.";
                    break;
                case StatusTypes.Fine:
                    StatusMessage = Init + "Ready to Go!";
                    break;
                case StatusTypes.DiceRolled:
                    StatusMessage = Init + "Dice Rolled. ";
                    break;
                case StatusTypes.Untrained:
                    StatusMessage = "  Note: Untrained = 1/2 single Dice. ";
                    break;

                case StatusTypes.CritFail:
                    StatusMessage = Init + "Dice Rolled. ";
                    
                    break;


            }
            return StatusMessage;
        }
        /// <summary>
        /// Overload: Function to update the status line where the number is outside of the boundary
        /// </summary>
        /// <param name="Boundary1">Start boundary = integer</param>
        /// <param name="Boundary2">End Boundary - integer</param>
        /// <returns>Status message for the program</returns>
        public string UpdateStatus(int Boundary1, int Boundary2)
        {

            string StatusMessage = Init + "Error: Value outside boundary, input value between " + Boundary1.ToString() + " and " + Boundary2.ToString() + ".";
            return StatusMessage;
        }
        /// <summary>
        /// Overload: Function to update the status line where the number is below the lower boundary
        /// </summary>
        /// <returns>Status message for the program</returns>
        public string UpdateStatus()
        {
            string StatusMessage = Init + "Error: Value outside boundary, input value greater than 7";
            return StatusMessage;
        }
        /// <summary>
        /// Overload: Function to update the status line where an error has occured
        /// </summary>
        /// <param name="Error">The error code</param>
        /// <returns>Status message for the program</returns>
        public string UpdateStatus(Exception Exception)
        {
            
            string StatusMessage = Init + "Error: " + Exception + "  Please report this to TimBull128@live.co.uk";
            return StatusMessage;
        }



        public string UpdateSuccess(SuccessTypes SuccessType, int NumDegrees)
        {
            string SuccessMessage = "";
            switch (SuccessType)
            {
                case SuccessTypes.NotApplicable:
                    SuccessMessage += "N/A";
                    break;
                case SuccessTypes.Failed:
                    SuccessMessage += "Failed by " + NumDegrees.ToString() + " Degrees.";
                    break;
                case SuccessTypes.Succeeded:
                    SuccessMessage += "Passed by " + NumDegrees.ToString() + " Degrees.";
                    break;
                case SuccessTypes.CritPass:
                    SuccessMessage += "Critical Pass!";
                    break;
                case SuccessTypes.CritFail:
                    SuccessMessage += "Critical Fail Found!";
                    break;
            }
            return SuccessMessage;
        }
    }
    /// <summary>
    /// Enumerations for UpdateStatus function
    /// </summary>
    enum StatusTypes
    {
        InvalidEntry,
        Fine,
        DiceRolled,
        Untrained,
        ErrorMessage,
        CritFail,
        
    }
    enum SuccessTypes
    {
        NotApplicable,
        Failed,
        Succeeded,
        CritPass,
        CritFail,
        NotDetermined

    }
}
