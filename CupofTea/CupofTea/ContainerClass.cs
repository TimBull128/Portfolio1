using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.CodeDom.Compiler;


namespace CupofTea
{
    /// <summary>
    /// Various objects for use
    /// </summary>

    public abstract class ContainerClass
    {

        
        public Containers Name { get; set; }
        public bool? InHand { get; set; }
        public IngredientNames Contains1;
        public IngredientNames? Contains2;
        public IngredientNames? Contains3;
        
        public bool IsFull { get; set; }

        public string Pickup()
        {
            StringBuilder BuildString = new StringBuilder();

            string Value, OutputVar;
            Value = CheckName();
            if (this.InHand == false)
            {
                BuildString.Append(Value + " has been picked up" + "\n");
                this.InHand = true;

            }
            else
            {
                BuildString.Append(Value + " is already in your hand" + "\n");

            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
        }

        public string Putdown()
        {
            StringBuilder BuildString = new StringBuilder();


            string Value, OutputVar;
            Value = CheckName();

            if (this.InHand == true)
            {
                BuildString.Append(Value + " has been put down" + "\n");
                this.InHand = false;


            }
            else
            {
                BuildString.Append(Value + " is already on table" + "\n");
                this.InHand = false;
            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
        }
        public string Fill(IngredientNames ingredient, Containers From)
        {
            string OutputVar;
            StringBuilder BuildString = new StringBuilder();
            IngredientNames Nothing = IngredientNames.Nothing;
            BuildString.Append(ingredient.ToString() + " has been added to " + this.Name.ToString() + " from " + From.ToString() );
            if (this.Contains1 == Nothing)
            {
                this.Contains1 = ingredient;
                if (this.Contains2 == null)
                {
                    this.IsFull = true;
                }
            }
            else if (this.Contains2 == Nothing && this.Contains2 != null)
            {
                this.Contains2 = ingredient;
                if (this.Contains3 != Nothing)
                {
                    this.IsFull = true;
                }
            }
            else if (this.Contains3 == Nothing && this.Contains3 != null)
            {
                Contains3 = ingredient;
                this.IsFull = true;
            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
        }
        public string Fill(IngredientNames ingredient)
        {
            StringBuilder BuildString = new StringBuilder();
            IngredientNames Nothing = IngredientNames.Nothing;
            
            string Value, OutputVar;
            Value = CheckName();
            if (this.InHand == true)
            {
                 if (ingredient != IngredientNames.Nothing)
                 { 
                    if (this.IsFull == false)
                    {
                        BuildString.Append(Value + " has been filled with " + ingredient.ToString() + "\n");
                        if (Contains1 == Nothing)
                        {
                            Contains1 = ingredient;
                            if(Contains2 == null)
                            {
                                this.IsFull = true;
                            }
                        }
                        else if(Contains2 == Nothing && Contains2 != null) 
                        {
                            Contains2 = ingredient;
                            if(Contains3 != Nothing)
                            {
                                this.IsFull = true;
                            }
                        }
                        else if(Contains3 == Nothing && Contains3 != null)
                        {
                            Contains3 = ingredient;
                            this.IsFull = true;
                        }
                    }
                    else
                    {
                        BuildString.Append(Value + " is already full, empty first!" + "\n");
                    }
                }
                else
                {
                    BuildString.Append("You tried to fill the " + Value + " with fresh air alongside it's current contents. \n");
                    BuildString.Append("You gave up after a while...");
                }
            }
            else
            {
                BuildString.Append("Might want to pick up this item before you try fill this item.");
            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
        }
        public string Empty()
        {
            StringBuilder BuildString = new StringBuilder();
            IngredientNames Nothing = IngredientNames.Nothing;
            string Value, OutputVar;
            Value = CheckName();
            if (this.IsFull == true | ( //Fix for cup not emptying right
                                            this.Name == Containers.Cup &&

                                            ( this.Contains1 != IngredientNames.Nothing |
                                            this.Contains2 != IngredientNames.Nothing |
                                            this.Contains3 != IngredientNames.Nothing )
                                        )
                 )
            {
                BuildString.Append(Value + " has been emptied!" + "\n");
                this.Contains1 = Nothing;
                if(this.Contains2 != null)
                {
                    this.Contains2 = Nothing;
                }
                if(this.Contains3 != null)
                {
                    this.Contains3 = Nothing;
                }
                this.IsFull = false;
            }
            else
            {
                BuildString.Append(Value + " is already empty!" + "\n");
            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;

        }
        /// <summary>
        /// Removes the identified ingredient - only works on teabags
        /// </summary>
        /// <param name="ingredient">Ingredient to be removed</param>
        /// <returns>Returns the amount of items that was removed</returns>
        public string RemoveItem(IngredientNames ingredient)
        {

            StringBuilder BuildString = new StringBuilder();
            int count = 0;
            string OutputVar;
            if (ingredient == IngredientNames.Teabag)
            {
                if (this.Name != Containers.Kettle)
                {
                    if (this.Contains1 == IngredientNames.Teabag)
                    {
                        this.Contains1 = IngredientNames.Nothing;
                        count++;
                    }
                    if (this.Contains2 == IngredientNames.Teabag)

                    {
                        this.Contains2 = IngredientNames.Nothing;
                        count++;
                    }
                    if (this.Contains3 == IngredientNames.Teabag)
                    {
                        this.Contains3 = IngredientNames.Nothing;
                        count++;
                    }
                    BuildString.Append(count.ToString() + " teabags have been removed");
                }
                else
                {
                    BuildString.Append("A lot of teabags have been removed");
                }
            }
            
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
        }
        private string CheckName()
        {
            
            string OutputVar;
            //Check the item that is required
            switch (this.Name)
            {
                case Containers.Cup:
                    OutputVar = "Cup";
                    break;
                case Containers.Kettle:
                    OutputVar = "Kettle";
                    break;
                case Containers.MilkBottle:
                    OutputVar = "Milk Bottle";
                    break;
                case Containers.Spoon:
                    OutputVar = "Spoon";
                    break;
                default:
                    OutputVar = "Nothing";
                    break;
            }
            return OutputVar;
        }

    }
    public class Kettle : ContainerClass
    {
        
        public bool IsOn { get; set; }
        public float Temp { get; set; }
        public bool Boiled { get; set; }

        public void Boil() 
        {
            this.IsOn = true;
            Thread BoilThread = new Thread(Boiling);
            BoilThread.Start();

        }
        private void Boiling()
        {
            DateTime Time1 = DateTime.Now;
            DateTime Time2;
            float Deltatime;
            while (this.IsOn == true)
            {
                Time2 = DateTime.Now;
                Deltatime = (Time2.Ticks - Time1.Ticks) / 10000000f;
                Temp += Deltatime * 10;
                if (Temp > 100)
                {
                    this.IsOn = false;
                    this.Boiled = true;
                }
            }


        }

    }
    public class Spoon : ContainerClass
    {
        public int StirAmount { get; set; }
        public bool PreviouslyContainedTeabag { get; set; }
        public string Stir()
        {
            StringBuilder BuildString = new StringBuilder();
            string OutputVar;
            
            if (StirAmount < 5 )
            {
                BuildString.Append("You have stirred " + StirAmount.ToString() + " times");
                StirAmount++;
            }
            else if (StirAmount == 5)
            {
                BuildString.Append("That's enough stirring - you have a nice cup of tea");
                StirAmount++;
            }
            else if (StirAmount > 5)
            {
                BuildString.Append("You have stirred " + StirAmount.ToString() + " times - seriously, that's enough");
                StirAmount++;
            }
            if (StirAmount > 20)
            {
                BuildString.Append("You... May have a problem");
                
            }
            OutputVar = BuildString.ToString();
            BuildString.Clear();
            return OutputVar;
            
        }
        
        public string TakeOut(IngredientNames ingredient)
        {
            StringBuilder BuildString = new StringBuilder();
            string OutPutVar;
            if ( !(ingredient == IngredientNames.Teabag && ingredient == IngredientNames.Nothing ))
            {
                BuildString.Append("Either through sheer grim determination or sheer boredom you manage to empty the " + this.Name.ToString() + ".");
                if (this.Name == Containers.Cup)
                {
                    if (ingredient == IngredientNames.Milk) 
                    {
                        BuildString.Append("Also you take out the Water...");
                        if (this.Contains1 == IngredientNames.Water)
                        {
                            Contains1 = IngredientNames.Nothing;
                        }
                        if (this.Contains2 == IngredientNames.Water)
                        {
                            Contains2 = IngredientNames.Nothing;
                        }
                        if (this.Contains3 == IngredientNames.Water)
                        {
                            Contains3 = IngredientNames.Nothing;
                        }
                    }
                    else if(ingredient == IngredientNames.Water)
                    {
                        BuildString.Append("Also you take out the Milk...");
                        if (this.Contains1 == IngredientNames.Milk)
                        {
                            Contains1 = IngredientNames.Nothing;
                        }
                        if (this.Contains2 == IngredientNames.Milk)
                        {
                            Contains2 = IngredientNames.Nothing;
                        }
                        if (this.Contains3 == IngredientNames.Milk)
                        {
                            Contains3 = IngredientNames.Nothing;
                        }
                    }
                    
                }
                BuildString.Append("Congratulations!");
                if (this.Contains1 == ingredient) {
                    Contains1 = IngredientNames.Nothing;
                }
                if (this.Contains2 == ingredient)
                {
                    Contains2 = IngredientNames.Nothing;
                }
                if (this.Contains3 == ingredient)
                {
                    Contains3 = IngredientNames.Nothing;
                }
            }
            else
            {
                BuildString.Append("Still trying to spoon out fresh air?");
            }
            OutPutVar = BuildString.ToString();
            BuildString.Clear();
            return OutPutVar;
        }
        /// <summary>
        /// Attempt to take out of the container using Spoon. Note: secondary use of "remove" function required 
        /// </summary>
        /// <param name="Container">The Container to take out</param>
        /// <param name="Ingredient">The ingredient to take out</param>
        /// <returns>String value of the resulting story</returns>
        public string TakeOut(Containers Container, IngredientNames Ingredient)
        {
            StringBuilder BuildString = new StringBuilder();
            if(Container == Containers.Cup)
            {
                
                if (Ingredient == IngredientNames.Teabag)
                {
                    BuildString.Append("You take out the Teabag");
                    this.Contains1 = Ingredient;
                }
                else if (Ingredient != IngredientNames.Nothing)
                {
                    BuildString.Append("You take out enough to fill the teaspoon - this of course does not affect the contents of the cup");
                    this.Contains1 = Ingredient;
                    
                }
                else
                {
                    BuildString.Append("Nothing was taken out");
                }
            }
            else
            {
                if (Ingredient == IngredientNames.Teabag)
                {
                    BuildString.Append("You take out the Teabags one by one - this takes some time");
                    this.Contains1 = Ingredient;
                    
                }
                else if(Ingredient != IngredientNames.Nothing)
                {
                    
                    BuildString.Append("You take out enough to fill the teaspoon - this of course does not affect the contents of the " + Container);
                    this.Contains1 = Ingredient;
                }
                else
                {
                    BuildString.Append("Nothing was taken out");
                }
            }
            return BuildString.ToString();
        }



    }
    public class Cup : ContainerClass
    {
        public int NumIngredients { get; set; }
        public bool Tea { get; set; }

    }
    public class MilkBottle : ContainerClass
    {

    }

    /// <summary>
    /// Enumerations for ingredients
    /// </summary>
    public enum IngredientNames
    {
        Teabag,
        Milk,
        Water,
        Nothing
    }
    /// <summary>
    /// Enumerations for Containers
    /// </summary>
    public enum Containers
    {
        Kettle,
        MilkBottle,
        Spoon,
        Cup,
        None
    }

    

}

