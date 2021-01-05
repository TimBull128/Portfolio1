using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Base

{
    class Base10Calculations
    {
        public long AddNumber(long FirstNum)
        {
            return FirstNum;
        }
        public long AddNumber(long FirstNum, long SecNum)
        {
            return (FirstNum + SecNum);

        }
        public long SubNumber(long FirstNum, long SecNum)
        {
            return (FirstNum - SecNum);
        }
        public long MultNumber(long FirstNum, long SecNum)
        {
            return (FirstNum * SecNum);
        }
        public long? DivNumber(long FirstNum, long SecNum)
        {
            long? Result;
            try
            {
                Result = FirstNum / SecNum;


            }
            catch
            {
                Result = null;
            }
            return Result;
        }
    }
    class Conversions
    {
        public long BaseNumber;

        public long ToBase10(int InputNumber)
        {
            string[] InputDigits = InputNumber.ToString().Split();
            int i = InputDigits.Length - 1;
            long Result = 0;
            foreach(string Digit in InputDigits)
            {
                long.TryParse(Digit, out long Value);
                Result += Value + (BaseNumber * i);
                i--;
            }
            return Result;
        }
        public long ToBase(long InputNumber)
        {
            IList<long> Digits = new List<long>();
            long TempResult = InputNumber;
            int Counter = 0;
            if(InputNumber < this.BaseNumber)
            {
                Digits.Add(InputNumber);
            }
            else
            {
                while (TempResult > BaseNumber) {
                    Digits.Add(TempResult % this.BaseNumber);
                    TempResult = (TempResult - Digits[Counter]) / BaseNumber;
                    Counter++;
                }
                Digits.Add(TempResult);
            }
            
            string Figure = "";
            for(int i = Digits.Count - 1; i >= 0; i--)
            {
                Figure += Digits[i];
            }
            long.TryParse(Figure, out long Result);
            return Result;

                
        }
    }
}
