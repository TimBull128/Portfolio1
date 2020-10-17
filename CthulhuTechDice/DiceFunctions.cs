using System;
using System.Collections;
using System.Security.Cryptography;
namespace CthulhuTechDice
{
	public class DiceFunctions
	{
		int DiceRolled;
		/// <summary>
        /// Rolls the dice
        /// </summary>
        /// <returns>The dice value from a random number generator</returns>
		public void Roll()
		{
			Random Random = new Random();
			this.DiceRolled = Random.Next();

		}
		
	}

}
