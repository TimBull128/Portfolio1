using System;
using System.Collections.Generic;
using System.Linq;
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


namespace CthulhuTechDice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List of dice rolled
        readonly IList<int> Dice = new List<int>();
        
        //Declaration of system functions
        readonly DiceFunctions DiceFunctions = new DiceFunctions();
        readonly List<TextBox> DiceBoxes = new List<TextBox>();
        readonly ScoreFunctions ScoreFunctions = new ScoreFunctions();

        readonly Status Status = new Status();
        
        
        public MainWindow()
        {
            InitializeComponent();
            //Declare list of dice Textboxes
            DiceBoxes = new List<TextBox> {
                    DiceTxt1,
                    DiceTxt2,
                    DiceTxt3,
                    DiceTxt4,
                    DiceTxt5,
                    DiceTxt6,
                    DiceTxt7,
                    DiceTxt8
            };

            //Set up screen
            NumDiceTxt.Text = "0";
            StatAdditionTxt.Text = "0";
            CustomModifierTxt.Text = "0";
            DiffNoneRad.IsChecked = true;
            StatusTxt.Text = Status.UpdateStatus(StatusTypes.Fine);
            SuccessTxt.Text = "";
            Status.DiceRolled = false;
            ClearDice();

            //Events to be used
            BtnRoll.Click += BtnRoll_Click;
            NumDiceTxt.TextChanged += NumDiceTxt_TextChanged;
            DiffCustomTxt.TextChanged += DiffCustomTxt_TextChanged;
            StatAdditionTxt.TextChanged += StatAdditionTxt_TextChanged;
            CustomModifierTxt.TextChanged += CustomModifierTxt_TextChanged;

            DiffEasyRad.Checked += DiffEasyRad_Checked;
            DiffAverageRad.Checked += DiffAverageRad_Checked;
            DiffChallengingRad.Checked += DiffChallengingRad_Checked;
            DiffHardRad.Checked += DiffHardRad_Checked;
            DiffIncHardRad.Checked += DiffIncHardRad_Checked;
            DiffLegendaryRad.Checked += DiffLegendaryRad_Checked;
            DiffNoneRad.Checked += DiffNoneRad_Checked;
            DiffCustomRad.Checked += DiffCustomRad_Checked;

            
        }

        private void CustomModifierTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustomModifierTxt.Text.Length != 0)
            {


                //When the text changes - check to see if the input contains a noninteger
                //If it does - remove the last entry and alert user
                if (!int.TryParse(CustomModifierTxt.Text, out int value))
                {
                    CustomModifierTxt.Text = DiffCustomTxt.Text.Remove(CustomModifierTxt.Text.Length - 1);
                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.InvalidEntry);
                }

                //else everything is fine - update user to say everything is ready to go
                else
                {

                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.Fine);
                }

            }
        }

        private void StatAdditionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StatAdditionTxt.Text.Length != 0)
            {


                //When the text changes - check to see if the input contains a noninteger
                //If it does - remove the last entry and alert user
                if (!int.TryParse(StatAdditionTxt.Text, out int Value))
                {
                    StatAdditionTxt.Text = DiffCustomTxt.Text.Remove(CustomModifierTxt.Text.Length - 1);
                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.InvalidEntry);
                }

                //else everything is fine - update user to say everything is ready to go
                else
                {

                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.Fine);
                }

            }
        }

        private void DiffCustomTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DiffCustomTxt.Text.Length != 0)
            {
                
                
                    //When the text changes - check to see if the input contains a noninteger
                    //If it does - remove the last entry and alert user
                    if (!int.TryParse(DiffCustomTxt.Text, out int Value))
                    {
                        DiffCustomTxt.Text = DiffCustomTxt.Text.Remove(DiffCustomTxt.Text.Length - 1);
                        StatusTxt.Text = Status.UpdateStatus(StatusTypes.InvalidEntry);
                    }
                    //Check to see if the value is within the boundary of 8 and 1
                    //If it does - remove the last entry and alert user
                    else if (Value < 1 && DiffNoneRad.IsChecked == false)
                    {
                        DiffCustomTxt.Text = NumDiceTxt.Text.Remove(NumDiceTxt.Text.Length - 1);
                        StatusTxt.Text = Status.UpdateStatus();
                    }
                    //else everything is fine - update user to say everything is ready to go
                    else
                    {

                        StatusTxt.Text = Status.UpdateStatus(StatusTypes.Fine);
                    }
                
            }
        }

        private void DiffCustomRad_Checked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DiffNoneRad_Checked(object sender, RoutedEventArgs e)
        {
            DiffCustomTxt.Text = "0";
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Hidden;
            SuccessTxt.Text =Status.UpdateSuccess(SuccessTypes.NotApplicable, 0);
            SuccessTxt.Visibility = Visibility.Hidden;
            
        }

        private void DiffLegendaryRad_Checked(object sender, RoutedEventArgs e)
        {

            int DegreeComparison = 34;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        if (ScoreFunctions.Degree < 5)
                        {
                            SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                        }
                        else
                        {
                            SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritPass, ScoreFunctions.Degree);
                        }
                    }
                    else
                    {

                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                        
                    }
                }
                
            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }



        }

        private void DiffIncHardRad_Checked(object sender, RoutedEventArgs e)
        {

            int DegreeComparison = 28;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }
                }

            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }
        }

        private void DiffHardRad_Checked(object sender, RoutedEventArgs e)
        {


            int DegreeComparison = 22;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }
                }

            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }
        }

        private void DiffChallengingRad_Checked(object sender, RoutedEventArgs e)
        {
            int DegreeComparison = 16;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }
                }

            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }
        }

        private void DiffAverageRad_Checked(object sender, RoutedEventArgs e)
        {
            int DegreeComparison = 12;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }
                }

            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }
        }

        private void DiffEasyRad_Checked(object sender, RoutedEventArgs e)
        {
            int DegreeComparison = 8;
            DiffCustomTxt.Text = DegreeComparison.ToString();
            DiffCustomTxt.IsEnabled = false;
            DiffCustomTxt.Visibility = Visibility.Visible;

            SuccessTxt.Visibility = Visibility.Visible;

            if (Status.DiceRolled)
            {
                if (DiceFunctions.CritFail)
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail, 0);
                }
                else
                {
                    if (ScoreFunctions.CompareScore(DegreeComparison))
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }
                }

            }
            else
            {
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotDetermined, 0);
            }
        }

        /// <summary>
        /// Rolls all dice when clicked
        /// </summary>

        private void BtnRoll_Click(object sender, RoutedEventArgs e)
        {

            StatusTxt.Text = "";
            Status.DiceRolled = true;
            int ScoreCompare = int.Parse(DiffCustomTxt.Text);
            int Stat = int.Parse(StatAdditionTxt.Text);
            int Custom = int.Parse(CustomModifierTxt.Text);
            ClearDice();

            //Should not be any non digit entries - changes when typing - redundancy in testing - returns the value of of the textbox
            if (int.TryParse(NumDiceTxt.Text, out int Value))
            {
                if(UntrainedChk.IsChecked == false)
                {
                    for (int Count = 0; Count < Value; Count++)
                    {
                        Dice.Add(DiceFunctions.Roll(1, 10));
                    

                    }
                }
                else
                {
                    Dice.Add(DiceFunctions.Roll(1, 5));
                    
                }
               
            }
            UpdateDice();

            int Score = DiceFunctions.Score(Dice);

            

            if (DiceFunctions.CritFail)
            {
                
                StatusTxt.Text = Status.UpdateStatus(StatusTypes.CritFail);
                if (UntrainedChk.IsChecked == true)
                {
                    StatusTxt.Text += Status.UpdateStatus(StatusTypes.Untrained);
                }
                ScoreTxt.Text = "0";
                SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.CritFail,0);
                SuccessTxt.Visibility = Visibility.Visible;
            }
            else
            {
                
                StatusTxt.Text = Status.UpdateStatus(StatusTypes.DiceRolled);
                if (UntrainedChk.IsChecked == true)
                {
                    StatusTxt.Text += Status.UpdateStatus(StatusTypes.Untrained);
                }
                

                
                ScoreFunctions.Score = Score + Stat + Custom;
                ScoreTxt.Text = ScoreFunctions.Score.ToString();
                
                if(DiffNoneRad.IsChecked == false)
                {


                    if (ScoreFunctions.CompareScore(ScoreCompare) ){
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Succeeded, ScoreFunctions.Degree);
                    }
                    else
                    {
                        SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.Failed, ScoreFunctions.Degree);
                    }

                }
                else
                {
                    SuccessTxt.Text = Status.UpdateSuccess(SuccessTypes.NotApplicable, ScoreFunctions.Degree);
                    SuccessTxt.Visibility = Visibility.Hidden;
                }
            }

        }
        //Clears dice
        private void ClearDice()
        {
            Dice.Clear();
            DiceFunctions.NumberDiceRolled = 0;
            //reset all Diceboxes
            for (int Count = 0; Count < DiceBoxes.Count; Count++)
            {
                DiceBoxes[Count].Text = "0";
                DiceBoxes[Count].Visibility = Visibility.Hidden;

            }
            DiceFunctions.ClearScores();
        }
        //Updates Dice
        private void UpdateDice()
        {
            //Repeat until all the dice have been entered
            for (int Count = 0; Count < DiceBoxes.Count; Count++)
            {


                if (Count < Dice.Count)
                {
                    //Get the value of the dice
                    int Value = Dice[Count];
                    //If the dice is equal to 10 - output 0 instead
                    if (Value == 10)
                    {
                  //      Value = 0;
                    }

                    //Enable the box with the new value
                    DiceBoxes[Count].Visibility = Visibility.Visible;
                    DiceBoxes[Count].Text = Value.ToString();
                }
                else
                {
                    DiceBoxes[Count].Visibility = Visibility.Hidden;
                }
            }

        }

        private void NumDiceTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumDiceTxt.Text.Length != 0)
            {
                //When the text changes - check to see if the input contains a noninteger
                //If it does - remove the last entry and alert user
                if (!int.TryParse(NumDiceTxt.Text, out int Value))
                {
                    NumDiceTxt.Text = NumDiceTxt.Text.Remove(NumDiceTxt.Text.Length - 1);
                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.InvalidEntry);
                }
                //Check to see if the value is within the boundary of 8 and 1
                //If it does - remove the last entry and alert user
                else if (Value < 0 && Value > 8)
                {
                    NumDiceTxt.Text = NumDiceTxt.Text.Remove(NumDiceTxt.Text.Length - 1);
                    StatusTxt.Text = Status.UpdateStatus(1,8);
                }
                //else everything is fine - update user to say everything is ready to go
                else
                {

                    StatusTxt.Text = Status.UpdateStatus(StatusTypes.Fine);
                }
            }
        }

        
    }
}
 
    