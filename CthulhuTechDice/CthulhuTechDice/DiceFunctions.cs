using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Documents;

namespace CthulhuTechDice
{
    /// <summary>
    /// used to store the amount of each dice there are in the set
    /// </summary>
    class DiceValues
    {

        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int Seven { get; set; }
        public int Eight { get; set; }
        public int Nine { get; set; }
        public int Ten { get; set; }
    }
    public class ScoreFunctions
    {
        public int Score { get; set; }
        public int Degree;
        public bool CompareScore(int Comparison)
        {
            bool Result;
            if(this.Score > Comparison)
            {
                Result = true;
                if(this.Score <= Comparison + 2) {
                    this.Degree = 1;
                }
                else if(this.Score > Comparison + 2 && this.Score <= Comparison + 4)
                {
                    this.Degree = 2;
                }
                else if(this.Score > Comparison + 4 && this.Score <= Comparison + 6)
                {
                    this.Degree = 3;
                }
                else if(this.Score > Comparison + 6 && this.Score <= Comparison + 9)
                {
                    this.Degree = 4;
                }
                else
                {
                    this.Degree = 5;
                }

            }
            else
            {
                Result = false;

                if (this.Score >= Comparison - 2)
                {
                    this.Degree = 1;
                }
                else if (this.Score < Comparison + 2 && this.Score >= Comparison - 4)
                {
                    this.Degree = 2;
                }
                else if (this.Score < Comparison + 4 && this.Score >= Comparison - 6)
                {
                    this.Degree = 3;
                }
                else if (this.Score < Comparison + 6 && this.Score >= Comparison + 9)
                {
                    this.Degree = 4;
                }
                else
                {
                    this.Degree = 5;
                }

            }
            return Result;
        }
    }
    public class DiceFunctions
    {
        
        public bool CritFail;
        //public int NumberOfDiceOutput;
        public int NumberDiceRolled;
        //public IList<int> DiceStopped = new List<int>();
        public IList<int> Scores = new List<int>();
        DiceValues _DiceValues;
        

        /// <summary>
        /// Rolls the dice
        /// </summary>
        /// <returns>The dice value from a random number generator</returns>
        public int Roll(int MinimumValue, int MaximumValue)
        {
            
            //Define the function for a RNG Crypto service provider which provides functions for better number generation using bytes
            //Random was producing a lot of duplicate values over multiple rolls
            RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

            byte[] RandomNumber = new byte[1];


            //Setting the seed for the random number
            _generator.GetBytes(RandomNumber);


            double AsciiValueOfRandomCharacter = Convert.ToDouble(RandomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double Multiplier = Math.Max(0, (AsciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = 20 - 1 + 1;

            double RandomValueInRange = Math.Floor(Multiplier * range);

            int Seed = (int)(MinimumValue + RandomValueInRange);


            // Setting the actual number for the dice roll
            // Get a new byte
            RandomNumber = new byte[1];

            for (int i = 1; i < Seed; i++) { 
                _generator.GetBytes(RandomNumber);
            }
            
            AsciiValueOfRandomCharacter = Convert.ToDouble(RandomNumber[0]);

            //Same process as above
            Multiplier = Math.Max(0, (AsciiValueOfRandomCharacter / 255d) - 0.00000000001d);
            range = MaximumValue - MinimumValue + 1;

            RandomValueInRange = Math.Floor(Multiplier * range);
            
            int DiceRolled = (int)(MinimumValue + RandomValueInRange);
            this.NumberDiceRolled++;
            return DiceRolled;
        }


        public void ClearScores()
        {
            this.Scores.Clear();
            this.CritFail = false;
            this._DiceValues = new DiceValues { One = 0, Two = 0, Three = 0, Four = 0, Five = 0, Six = 0, Eight = 0, Nine = 0, Seven = 0, Ten = 0 };

        }
        public int Score(IList<int> DiceValues)
        {
            
            IList<int> OrderedList = this.SortValues(DiceValues);
            
            
            CheckForCritFail();
            

            Scores.Clear();


            //Check CritFail
            if (this.CritFail)
            {
                Scores.Add(0);
            }
            else
            {

                this.MaxNumber(OrderedList);
                if (this.NumberDiceRolled >= 3)
                {
                    this.StraightScore(OrderedList);
                }
                this.FlushScore();

            }
            return this.Scores.Max();
        }
        /// <summary>
        /// Checks for crit fail = 1/2 dice in 1's or check the single dice if there was only one dice rolled
        /// </summary>
        private void CheckForCritFail()
        {
            this.CritFail = false;

            if (this.NumberDiceRolled != 1)
            {
                if (_DiceValues.One >= NumberDiceRolled / 2)
                {
                    this.CritFail = true;
                }
            }
            else
            {
                if(_DiceValues.One == 1)
                {
                    this.CritFail = true;
                }
            }

        }

        
        private void StraightScore(IList<int> DiceValues)
        {
            int Count = 0;
            int PreviousDie = -10;
            int CurrentScore = DiceValues[0];
            foreach(int Die in DiceValues)
            {
                if (Count != 0)
                {
                    if (Die == PreviousDie + 1)
                    {
                        CurrentScore += Die;
                        
                    }
                    else
                    {
                        Scores.Add(CurrentScore);
                        CurrentScore = Die;

                    }
                }
                PreviousDie = Die;
                Count++;
            }
            if(DiceValues[DiceValues.Count - 1] == PreviousDie - 1)
            {
                CurrentScore += DiceValues[DiceValues.Count - 1];
            }
            else
            {
                Scores.Add(CurrentScore);
            }
        }
        private void MaxNumber(IList<int> DiceValues)
        {
            Scores.Add (DiceValues.Max());
        }
        /// <summary>
        /// uses the values of dice to get a total which is stored in scores
        /// </summary>
        private void FlushScore()
        {
            Scores.Add(this._DiceValues.One);
            Scores.Add(this._DiceValues.Two * 2);
            Scores.Add(this._DiceValues.Three * 3);
            Scores.Add(this._DiceValues.Four * 4);
            Scores.Add(this._DiceValues.Five * 5);
            Scores.Add(this._DiceValues.Six * 6);
            Scores.Add(this._DiceValues.Seven * 7);
            Scores.Add(this._DiceValues.Eight * 8);
            Scores.Add(this._DiceValues.Nine * 9);
            Scores.Add(this._DiceValues.Ten * 10);
        }
        
        /// <summary>
        /// Sorts the array of dice into two different formats: the first is an aggrigation of dice values from 1-10, the second is an ordered set of values - which is returned 
        /// </summary>
        /// <param name="DiceValues">The input dice values</param>
        /// <returns>A ordered list of dice values</returns>
        private IList<int> SortValues(IList<int> DiceValues)
        {
            
            List<int> OrderedDice = (List<int>)DiceValues;
            OrderedDice.Sort();
            IList<int> Output = OrderedDice;

            this.ClearScores();
            foreach (int Die in DiceValues)
            {
                switch (Die)
                {
                    case 1:
                        _DiceValues.One++;
                        break;
                    case 2:
                        _DiceValues.Two++;
                        break;
                    case 3:
                        _DiceValues.Three++;
                        break;
                    case 4:
                        _DiceValues.Four++;
                        break;
                    case 5:
                        _DiceValues.Five++;
                        break;
                    case 6:
                        _DiceValues.Six++;
                        break;
                    case 7:
                        _DiceValues.Seven++;
                        break;
                    case 8:
                        _DiceValues.Eight++;
                        break;
                    case 9:
                        _DiceValues.Nine++;
                        break;
                    case 10:
                        _DiceValues.Ten++;
                        break;
                    default:
                        break;
                }
                
            }


            return Output;


        }
    }
}