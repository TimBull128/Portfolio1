using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CupofTea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //declare objects for use later use
        //Note - these are set in function "ReOrganiseObjects()"
        private Kettle VarKettle;
        private Spoon VarSpoon;
        private MilkBottle VarBottle ;
        private Cup VarCup;
        private Person VarPerson = new Person();
        private Story VarStory = new Story();

        //Constants for enumerations used in the code
        private const IngredientNames Water = IngredientNames.Water;
        private const IngredientNames Milk = IngredientNames.Milk;
        private const IngredientNames Teabag = IngredientNames.Teabag;
        private const IngredientNames Nothing = IngredientNames.Nothing;
        private const Containers Kettle = Containers.Kettle;
        private const Containers Bottle = Containers.MilkBottle;
        private const Containers Cup = Containers.Cup;
        private const Containers Spoon = Containers.Spoon;

        private string BoiledStatus;


        /// <summary>
        /// Main Code
        /// </summary>
        public MainWindow()
        {
            //Initialize components
            InitializeComponent();

            ReOrganiseObjects(false);

            //Initialize the instances of events.
            //Buttons
            BtnReset.Click += BtnReset_Click;
            BtnUp.Click += BtnUp_Click;
            BtnDown.Click += BtnDown_Click;
            BtnEmpty.Click += BtnEmpty_Click;
            BtnFill.Click += BtnFill_Click;
            BtnPourIn.Click += BtnPourIn_Click;
            BtnTurnOn.Click += BtnTurnOn_Click;
            BtnTakeOut.Click += BtnTakeOut_Click;
            BtnStir.Click += BtnStir_Click;
            //Change of Radio buttons for containers
            RadBottle.Click += RadBottle_Click;
            RadCup.Click += RadCup_Click;
            RadKettle.Click += RadKettle_Click;
            RadSpoon.Click += RadSpoon_Click;
            //Change of Check buttons for to containers
            CheckToBottle.Click += CheckToBottle_Click;
            CheckToCup.Click += CheckToCup_Click;
            CheckToKettle.Click += CheckToKettle_Click;
            CheckToSpoon.Click += CheckToSpoon_Click;
            
        }

        private void BtnStir_Click(object sender, RoutedEventArgs e)
        {
 
            Containers ToValue = CheckToContainer();
            if (ToValue == Containers.Cup)
            {
                if(VarCup.Tea == true)
                {
                    

                    if (VarSpoon.StirAmount == 5)
                    {
                        VarStory.StirMessage = VarSpoon.Stir();
                        VarStory.AddSuccessMessage = true;
                    }
                    else
                    {
                        VarStory.StirMessage = VarSpoon.Stir();
                    }
                }

                
            }
            else
            {
                VarStory.StirMessage = "You stirred the " + ToValue.ToString() + ".  Nothing happend.";
            }
            VarStory.AddStirMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
        }

        private void BtnTakeOut_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        // each one removed clicks from other checkboxes - also happens when deselected
        private void CheckToSpoon_Click(object sender, RoutedEventArgs e)
        {
            CheckToBottle.IsChecked = false;
            CheckToCup.IsChecked = false;
            CheckToKettle.IsChecked = false;
        }

        private void CheckToKettle_Click(object sender, RoutedEventArgs e)
        {
            CheckToBottle.IsChecked = false;
            CheckToCup.IsChecked = false;
            CheckToSpoon.IsChecked = false;
        }

        private void CheckToCup_Click(object sender, RoutedEventArgs e)
        {
            CheckToBottle.IsChecked = false;
            CheckToSpoon.IsChecked = false;
            CheckToKettle.IsChecked = false;
        }

        private void CheckToBottle_Click(object sender, RoutedEventArgs e)
        {
            CheckToSpoon.IsChecked = false;
            CheckToCup.IsChecked = false;
            CheckToKettle.IsChecked = false;
        }

        private void BtnTurnOn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder BuildString = new StringBuilder();
            Thread BoilCheckThread = new Thread(CheckBoiling);
            string VarOutput;

            if(RadKettle.IsChecked == true)
            {
                if (VarKettle.IsFull == true)
                {
                    if(VarKettle.InHand == false)
                    {
                        if (VarKettle.Boiled == false)
                        {
                            VarKettle.Boil();
                            BoilCheckThread.Start();
                        }
                        else if(VarKettle.Temp < 100)
                        {
                            BuildString.Append("Kettle is still boiling!");
                        }
                        else
                        {
                            BuildString.Append("The water is already boiled!");
                        }

                    }
                    else
                    {
                        BuildString.Append("You really need to put the kettle on the table before you start boiling it");
                    }
                }
                else
                {
                    BuildString.Append("Kettle is not full - this would cause damage to the kettle");
                }
            }
            else
            {
                BuildString.Append("There is nowhere to turn this item on!");
            }
            VarOutput = BuildString.ToString();
            BuildString.Clear();
            
            TxtOutput.Text = VarOutput;
        }
        private void CheckBoiling()
        {
            StringBuilder BuildString = new StringBuilder();
            
            while (VarKettle.Boiled == false)
            {
                BoiledStatus = "Not Boiled";
                BuildString.Append("Kettle is: " + BoiledStatus + "\n");
                VarStory.Boiled = BuildString.ToString();
                VarStory.AddBoiledMessage = true;
                TxtOutput.Text = VarStory.UpdateStory();
                BuildString.Clear();
            }
            BoiledStatus = "Boiled";
            BuildString.Append("Kettle is: " + BoiledStatus + "\n");
            VarStory.Boiled = BuildString.ToString();
            VarStory.AddBoiledMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            BuildString.Clear();

        }
        private void BtnPourIn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder BuildString = new StringBuilder();
            Containers FromValue = CheckToContainer();
            Containers ToValue = CheckContainerNames();
            IngredientNames IngredientValue = IngredientNames.Nothing;
           
            bool? IsInHandFrom = false, IsInHandTo = false;
            //Checks the status of the content of the original
            //Also checks if the object is in hand currently
            switch (FromValue)
            {
                case Containers.Kettle:
                    IngredientValue = VarKettle.Contains1;
                    IsInHandFrom = VarKettle.InHand;
                    break;
                case Containers.MilkBottle:
                    IngredientValue = VarBottle.Contains1;
                    IsInHandFrom = VarBottle.InHand;
                    break;
                case Containers.Spoon:
                    IngredientValue = VarSpoon.Contains1;
                    IsInHandFrom = VarSpoon.InHand;
                    break;
                case Containers.Cup:

                    break;
                default:
                    BuildString.Append("Nothing selected to pour from!");
                    break;

            }
            //Checks wether or not the object pouring to is in hand
            switch (ToValue)
            {
                case Containers.Kettle:
                    IsInHandTo = VarKettle.InHand;
                    break;
                case Containers.Cup:
                    IsInHandTo = VarKettle.InHand;
                    break;
                case Containers.MilkBottle:
                    IsInHandTo = VarBottle.InHand;
                    break;
                case Containers.Spoon:
                    IsInHandTo = VarSpoon.InHand;
                    break;
                default:
                    BuildString.Append("Nothing selected to pour into!");
                    break;
            }
            if (IsInHandFrom == true && IsInHandTo == true)
            {
                if (IngredientValue != Nothing)
                {
                    if (FromValue == ToValue)
                    {
                        BuildString.Append("...How do you propose to do that?");
                    }
                    else
                    {
                        switch (ToValue)
                        {
                            case Containers.Kettle:
                                if (FromValue != ToValue)
                                {
                                    BuildString.Append(VarKettle.Fill(IngredientValue, FromValue) + "\n");

                                }
                                break;
                            case Containers.MilkBottle:
                                if (FromValue != ToValue)
                                {
                                    BuildString.Append(VarBottle.Fill(IngredientValue, FromValue) + "\n");

                                }
                                break;
                            case Containers.Spoon:
                                if (FromValue != ToValue)
                                {
                                    BuildString.Append(VarSpoon.Fill(IngredientValue, FromValue)+ "\n");

                                }
                                break;
                            case Containers.Cup:
                                if (FromValue != ToValue)
                                {
                                    //BuildString.Append(VarCup.Fill(IngredientValue, FromValue));
                                    

                                }
                                break;
                            case Containers.None:
                                break;
                        }

                        switch (FromValue)
                        {
                            case Containers.Kettle:
                                BuildString.Append(VarKettle.Empty() + "\n");

                                break;
                            case Containers.MilkBottle:
                                BuildString.Append(VarBottle.Empty() + "\n");
                                break;
                            case Containers.Spoon:
                                BuildString.Append(VarBottle.Empty() + "\n");
                                break;
                            case Containers.Cup:

                                break;
                            case Containers.None:
                                break;

                        }
                    }
                }
                else
                {
                    BuildString.Append("The original container does not have any ingredients in it!");
                }

            }
            else
            {
                BuildString.Append("One or more containers is not in hand - check that you have both containers in hand!");
            }
            VarStory.AddIngredient = BuildString.ToString();
            VarStory.AddAddIngredientMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            CheckContainerContents(ToValue);
            BuildString.Clear();
        }
        private void RadSpoon_Click(object sender, RoutedEventArgs e)
        {

            CheckContainerContents(Spoon);
            BtnStir.IsEnabled = true;
            BtnTakeOut.IsEnabled = true;
        }

        private void RadKettle_Click(object sender, RoutedEventArgs e)
        {

            CheckContainerContents(Kettle);
            BtnStir.IsEnabled = false;
            BtnTakeOut.IsEnabled = false;
        }

        private void RadCup_Click(object sender, RoutedEventArgs e)
        {
            CheckContainerContents(Cup);
            BtnStir.IsEnabled = false;
            BtnTakeOut.IsEnabled = false;

        }


        private void RadBottle_Click(object sender, RoutedEventArgs e)
        {
            CheckContainerContents(Bottle);
            BtnStir.IsEnabled = false;
            BtnTakeOut.IsEnabled = false;
        }



        //Button responses
        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder BuildString = new StringBuilder();
            IngredientNames Ingredient = CheckIngredent();           
            if (RadKettle.IsChecked == true)
            {

                BuildString.Append(VarKettle.Fill(Ingredient));
                CheckContainerContents(Kettle);

            }
            if (RadBottle.IsChecked == true)
            {
                BuildString.Append(VarBottle.Fill(Ingredient));
                CheckContainerContents(Bottle);
            }
            if (RadSpoon.IsChecked == true)
            {
                BuildString.Append(VarSpoon.Fill(Ingredient));
                CheckContainerContents(Spoon);
            }
            if (RadCup.IsChecked == true)
            {
                BuildString.Append(VarCup.Fill(Ingredient));
                CheckContainerContents(Cup);
            }

            VarStory.AddIngredient = BuildString.ToString();
            VarStory.AddAddIngredientMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            BuildString.Clear();
        }

        private void BtnEmpty_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder BuildString = new StringBuilder();
            if (RadKettle.IsChecked == true)
            {
                BuildString.Append(VarKettle.Empty());
                CheckContainerContents(Kettle);

            }
            if (RadBottle.IsChecked == true)
            {
                BuildString.Append(VarBottle.Empty());
                CheckContainerContents(Bottle);
            }
            if (RadSpoon.IsChecked == true)
            {
                BuildString.Append(VarSpoon.Empty());
                CheckContainerContents(Spoon);
            }
            if(RadCup.IsChecked == true)
            {
                BuildString.Append(VarCup.Empty());
                CheckContainerContents(Cup);
            }
            VarStory.EmptyMessage = BuildString.ToString();
            VarStory.AddEmptyMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            BuildString.Clear();
        }

        
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            CheckHands();
            StringBuilder BuildString = new StringBuilder();
            if(VarPerson.CheckHandsEmpty() == false) {

                switch (CheckContainerNames())
                {
                    case Containers.Kettle:
                        BuildString.Append(VarKettle.Putdown());

                        break;
                    case Containers.MilkBottle:
                        BuildString.Append(VarBottle.Putdown());

                        break;
                    case Containers.Spoon:
                        BuildString.Append(VarSpoon.Putdown());

                        break;
                    case Containers.Cup:
                        BuildString.Append(VarCup.Putdown());

                        break;
                    default:
                        break;
                }
            }
            else
            {
                BuildString.Append("Your hands are already empty!");
            }
            CheckHands();
            VarStory.InHand = BuildString.ToString();
            VarStory.AddInHandMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            BuildString.Clear();
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            CheckHands();
            StringBuilder BuildString = new StringBuilder();
            if(VarPerson.CheckHandsFull() == false) {
                switch (CheckContainerNames())
                {
                    case Containers.Kettle:
                        BuildString.Append(VarKettle.Pickup());

                        break;
                    case Containers.MilkBottle:
                        BuildString.Append(VarBottle.Pickup());

                        break;
                    case Containers.Spoon:
                        BuildString.Append(VarSpoon.Pickup());

                        break;
                    case Containers.Cup:
                        BuildString.Append(VarCup.Pickup());

                        break;
                    default:
                        break;
                }
            }
            else
            {
                BuildString.Append("Your hands are already full!  Put something down!");
            }
            CheckHands();
            VarStory.InHand = BuildString.ToString();
            VarStory.AddInHandMessage = true;
            TxtOutput.Text = VarStory.UpdateStory();
            BuildString.Clear();

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {

            
            ReOrganiseObjects(true );

            
        }

        //Helper functions

        /// <summary>
        /// Function used to prepopulate and reset the objects
        /// </summary>

        public void ReOrganiseObjects(bool Reset)
        {
            

            VarPerson.ResetPerson();
            //Kettle object
            VarKettle = new Kettle()
            {
                Name = Kettle,
                Contains1 = Nothing,
                Contains2 = null, 
                Contains3 = null,
            
                InHand = false,
                IsFull = false,
                IsOn = false,

                
            };


                //Spoon object
            VarSpoon = new Spoon()
            {
                Name = Spoon,
                Contains1 = Nothing,
                Contains2 = null,
                Contains3 = null,
                InHand = false,
                IsFull = false,
                StirAmount = 0

            };




            //Milk Bottle object
            VarBottle = new MilkBottle()
            {
                Name = Bottle,
                Contains1 = Milk,
                Contains2 = null,
                Contains3 = null,
                InHand = false,
                IsFull = true,


            };


            ///Cup object
            VarCup = new Cup()
            {
                Name = Cup,
                Contains1 = Nothing,
                Contains2 = Nothing,
                Contains3 = Nothing,
                InHand = false, 
                IsFull = false,
                NumIngredients = 0
            };
            CheckHands();
            TxtOutput.Text = VarStory.ResetStory(Reset);
        }
        /// <summary>
        /// Checks what items are in hand, updates the text and ensures that the amount of items in the hands are correct
        /// </summary>
        public void CheckHands()
        {
            int value = 0;
            if (VarCup.InHand == true)
            {
                
                value++;
            }
            if (VarBottle.InHand == true)
            {
                
                value++;
            }
            if (VarKettle.InHand == true)
            {
                
                value++;
            }
            if (VarSpoon.InHand == true)
            {

                value++;
            }
            VarPerson.ObjectsInHand = value;

            TxtCupInHand.Text = "In hand: " + VarCup.InHand.ToString();
            TxtBotInHand.Text = "In hand: " + VarBottle.InHand.ToString();
            TxtKetInHand.Text = "In hand: " + VarKettle.InHand.ToString();
            TxtSpInHand.Text = "In hand: " + VarSpoon.InHand.ToString();


        }
        /// <summary>
        /// Checks the ingredient selected in Radio buttons
        /// </summary>
        /// <returns>The value of Ingredient that was selected</returns>

        public IngredientNames CheckIngredent()
        {
            //Values for each of the ingredients where declared at the start of code
            IngredientNames Value;
            if (RadMilk.IsChecked == true)
            {
                Value = Milk;
            }
            else if (RadTeabag.IsChecked == true)
            {
                Value = Teabag;
            }
            else if (RadWater.IsChecked == true)
            {
                Value = Water;
            }
            else
            {
                Value = Nothing;
            }
            return Value;
        }
        /// <summary>
        /// Checks the check buttons to where to pour contents
        /// </summary>
        /// <returns>value where to pour contents to </returns>
        public Containers CheckToContainer()
        {
            Containers Container = Containers.None;
            if(CheckToBottle.IsChecked == true)
            {
                Container = VarBottle.Name;
            }
            if(CheckToCup.IsChecked == true)
            {
                Container = VarCup.Name;
            }
            if(CheckToKettle.IsChecked == true)
            {
                Container = VarKettle.Name;
            }
            if(CheckToSpoon.IsChecked == true)
            {
                Container = VarSpoon.Name;
            }
            return Container;
        }
        /// <summary>
        /// Checks the containers this selected by the user
        /// </summary>
        /// <returns>The enumeration for the container selected</returns>
        public Containers CheckContainerNames()
        {
            Containers value;
            if (RadKettle.IsChecked == true)
            {
                value = Kettle;
            }
            else if (RadBottle.IsChecked == true)
            { 
                value = Bottle; 
            }
            else if (RadCup.IsChecked == true)
            {
                value = Cup;
            }
            else if (RadSpoon.IsChecked == true)
            {
                value = Spoon;
            }
            else
            {
                value = Containers.None;
            }
            return value;
        }
        
        /// <summary>
        /// Alternate container check that returns turns the ingredient checks on and off
        /// </summary>
        /// <param name="container">The enumerations for container</param>
        public void CheckContainerContents(Containers container)
        {
            //Values for each of the ingredients where declared at the start of code
            switch (container)
            {

                //Checks Kettle
                case Containers.Kettle:
                    if (VarKettle.Contains1 == Milk)
                    {
                        CheckMilk.IsChecked = true;
                    }
                    else
                    {
                        CheckMilk.IsChecked = false;
                    }
                    //Water
                    if (VarKettle.Contains1 == Water)
                    {
                        CheckWater.IsChecked = true;
                    }
                    else
                    {
                        CheckWater.IsChecked = false;
                    }
                    //Teabag
                    if (VarKettle.Contains1 == Teabag)
                    {
                        CheckTeabag.IsChecked = true;
                    }
                    else
                    {
                        CheckTeabag.IsChecked = false;
                    }
                    break;

                //Checks Bottle
                case Containers.MilkBottle:
                    //Milk
                    if (VarBottle.Contains1 == Milk)
                    {
                        CheckMilk.IsChecked = true;
                    }
                    else
                    {
                        CheckMilk.IsChecked = false;
                    }
                    //Water
                    if (VarBottle.Contains1 == Water)
                    {
                        CheckWater.IsChecked = true;
                    }
                    else
                    {
                        CheckWater.IsChecked = false;
                    }
                    //Teabag
                    if (VarBottle.Contains1 == Teabag)
                    {
                        CheckTeabag.IsChecked = true;
                    }
                    else
                    {
                        CheckTeabag.IsChecked = false;
                    }
                    break;

                //Checks Spoon
                case Containers.Spoon:
                    if (VarSpoon.Contains1 == Milk)
                    {
                        CheckMilk.IsChecked = true;
                    }
                    else
                    {
                        CheckMilk.IsChecked = false;
                    }
                    //Water
                    if (VarSpoon.Contains1 == Water)
                    {
                        CheckWater.IsChecked = true;
                    }
                    else
                    {
                        CheckWater.IsChecked = false;
                    }
                    //Teabag
                    if (VarSpoon.Contains1 == Teabag)
                    {
                        CheckTeabag.IsChecked = true;
                    }
                    else
                    {
                        CheckTeabag.IsChecked = false;
                    }
                    break;


                //Checks cup
                case Containers.Cup:
                    VarCup.NumIngredients = 0;
                    //Checks status every time Kettle is clicked
                    //Milk
                    if (VarCup.Contains1 == Milk ||
                        VarCup.Contains2 == Milk ||
                        VarCup.Contains3 == Milk)
                    {
                        CheckMilk.IsChecked = true;
                        VarCup.NumIngredients ++;
                    }
                    else
                    {
                        CheckMilk.IsChecked = false;
                    }
                    //Water
                    if (VarCup.Contains1 == Water ||
                        VarCup.Contains2 == Water ||
                        VarCup.Contains3 == Water )
                    {
                        CheckWater.IsChecked = true;
                        VarCup.NumIngredients ++;
                    }
                    else
                    {
                        CheckWater.IsChecked = false;
                    }
                    //Teabag
                    if (VarCup.Contains1 == Teabag ||
                        VarCup.Contains2 == Teabag ||
                        VarCup.Contains3 == Teabag)
                    {
                        CheckTeabag.IsChecked = true;
                        VarCup.NumIngredients ++;
                    }
                    else
                    {
                        CheckTeabag.IsChecked = false;
                    }
                    if(VarCup.NumIngredients == 3)
                    {
                        VarCup.IsFull = true;
                        VarCup.Tea = true;
                    }
                    break;
                default:
                    break;
            }

        }

    }
}