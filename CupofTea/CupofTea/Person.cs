using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CupofTea
{
    public class Person
    {
        public int ObjectsInHand;

        public void ResetPerson()
        {
            this.ObjectsInHand = 0;
        }
        public bool CheckHandsFull()
        {
            bool IsFull;
            if (this.ObjectsInHand == 2)
            {
                IsFull = true;
            }
            else
            {
                IsFull = false;
            }
            return IsFull;
        }
       public bool CheckHandsEmpty()
        {
            bool IsEmpty;
            if (this.ObjectsInHand == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
            return IsEmpty;
        }
    }
}
