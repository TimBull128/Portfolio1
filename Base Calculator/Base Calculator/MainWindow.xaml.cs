using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Base_Calculator
{
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class NumberObject
        {
            public long NumberA { get; set; }
            public long NumberB { get; set; }

        }
        readonly NumberObject Numbers = new NumberObject();
        readonly Calculations Calculator = new Calculations();
        private const string StrBase10 = "(Base10)";
        private const string StrBase7 = "(Base7  = ";
        private const string StrAdd = " + ";
        private const string StrMult = " x ";
        private const string StrMinus = " - ";


        public MainWindow()
        {
            InitializeComponent();
            TxtInputValue.Text = "0";
            TxtOutputBase10.Text = "0";
            TxtStatus.Text = "Status: Ready!";
            TxtSum.Text = "0";
            TxtOutputBase7.Text = "0";
            RadBase10.IsChecked = true;
            Btn0.Click += Btn0_Click;
            Btn1.Click += Btn1_Click;
            Btn2.Click += Btn2_Click;
            Btn3.Click += Btn3_Click;
            Btn4.Click += Btn4_Click;
            Btn5.Click += Btn5_Click;
            Btn6.Click += Btn6_Click;
            Btn7.Click += Btn7_Click;
            Btn8.Click += Btn8_Click;
            Btn9.Click += Btn9_Click;
            BtnClear.Click += BtnClear_Click;
            BtnAdd.Click += BtnAdd_Click;
            BtnDiv.Click += BtnDiv_Click;
            BtnMult.Click += BtnMult_Click;
            BtnSub.Click += BtnSub_Click;
            BtnInput.Click += BtnInput_Click;
            RadBase10.Checked += RadBase10_Checked;
            RadBase7.Checked += RadBase7_Checked;
        }

        private void RadBase7_Checked(object sender, RoutedEventArgs e)
        {
            Btn7.IsEnabled = false;
            Btn8.IsEnabled = false;
            Btn9.IsEnabled = false;
            
        }

        private void RadBase10_Checked(object sender, RoutedEventArgs e)
        {
            Btn7.IsEnabled = true;
            Btn8.IsEnabled = true;
            Btn9.IsEnabled = true;
        }

        private void NumberConversions()
        {
            Numbers.NumberB = long.Parse(TxtInputValue.Text);

            if (RadBase10.IsChecked == true)
            {
                Numbers.NumberA = long.Parse(TxtInputValue.Text);

            }
            else
            {
                Numbers.NumberA = long.Parse(Calculator.Convert10ToBase(TxtInputValue.Text, 10));

            }
        }
        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            
            if(RadBase10.IsChecked == true)
            {
                TxtOutputBase10.Text = TxtInputValue.Text;
                TxtOutputBase7.Text = Calculator.Convert10ToBase(TxtInputValue.Text, 7);
                TxtSum.Text = "Base 10: " + TxtInputValue.Text + "= Base 7: " + TxtOutputBase7.Text;

            }
            else
            {
                TxtOutputBase7.Text = TxtInputValue.Text;
                TxtOutputBase10.Text = Calculator.ConvertBaseTo10(TxtInputValue.Text, 7);
                TxtSum.Text = "Base 7: " + TxtInputValue.Text + "= Base 10: " + TxtOutputBase10.Text;

            }
            

            TxtStatus.Text = "Status: Input!";
            TxtInputValue.Text = "";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
            TxtOutputBase10.Text = "0";
            TxtOutputBase7.Text = "0";
            TxtSum.Text = "0";
        }

        private void BtnSub_Click(object sender, RoutedEventArgs e)
        {
            NumberConversions();
            
            TxtOutputBase10.Text = Calculator.SubNumber(Numbers.NumberA, Numbers.NumberB).ToString();
            TxtOutputBase7.Text = Calculator.Convert10ToBase(TxtOutputBase10.Text, 7);

            if(RadBase10.IsChecked == true)
            {
                TxtSum.Text += StrMinus + Numbers.NumberA + StrBase10;
            }
            else
            {
                TxtSum.Text += StrMinus + Numbers.NumberA + StrBase7 + Calculator.ConvertBaseTo10(TxtInputValue.Text, 10).ToString() + ")";
            }
            TxtStatus.Text = "Status: Subtracted!";
            TxtInputValue.Text = "";

        }

        private void BtnMult_Click(object sender, RoutedEventArgs e)
        {

            NumberConversions();


            if (RadBase10.IsChecked == true)
            {
                TxtSum.Text += StrMult + Numbers.NumberA + StrBase10;
            }
            else
            {
                TxtSum.Text += StrMult + Numbers.NumberA + StrBase7 + Calculator.ConvertBaseTo10(TxtInputValue.Text, 10).ToString() + ")";
            }
            TxtOutputBase10.Text = Calculator.MultNumber(Numbers.NumberB, Numbers.NumberA).ToString();
            TxtOutputBase7.Text = Calculator.Convert10ToBase(TxtOutputBase10.Text, 7);
            TxtStatus.Text = "Status: Multiplied!";

            TxtInputValue.Text = "";


        }

        private void BtnDiv_Click(object sender, RoutedEventArgs e)
        {

            long.TryParse(TxtInputValue.Text, out long NumberA);
            long.TryParse(TxtOutputBase10.Text, out long NumberB);
            long? Result = Calculator.DivNumber(NumberA, NumberB);

            if(Result == null)
            {
                TxtStatus.Text = "Status: Divide by Zero Detected";
            }
            else
            {
                TxtOutputBase10.Text = Result.ToString();
                TxtStatus.Text = "Status: Divided";
            }
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            NumberConversions();

            if (RadBase10.IsChecked == true)
            {
                TxtSum.Text += StrAdd + Numbers.NumberA + StrBase10;
            }
            else
            {
                TxtSum.Text += StrAdd + Numbers.NumberA + StrBase10 + Calculator.ConvertBaseTo10(TxtInputValue.Text, 7).ToString() + ")";
            }

            TxtOutputBase10.Text = Calculator.AddNumber(Numbers.NumberB, Numbers.NumberA).ToString();
            TxtOutputBase7.Text = Calculator.Convert10ToBase(TxtOutputBase10.Text, 7);
            TxtStatus.Text = "Status: Added!";

            TxtInputValue.Text = "";
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "9";
            }
            else
            {
                TxtInputValue.Text = "9";
            }
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "8";
            }
            else
            {
                TxtInputValue.Text = "8";
            }
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "7";
            }
            else
            {
                TxtInputValue.Text = "7";
            }
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "6";
            }
            else
            {
                TxtInputValue.Text = "6";
            }
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "5";
            }
            else
            {
                TxtInputValue.Text = "5";
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "4";
            }
            else
            {
                TxtInputValue.Text = "4";
            }
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "3";
            }
            else
            {
                TxtInputValue.Text = "3";
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "2";
            }
            else
            {
                TxtInputValue.Text = "2";
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0")
            {
                TxtInputValue.Text += "1";
            }
            else
            {
                TxtInputValue.Text = "1";
            }
        }

        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text != "0"){
                TxtInputValue.Text += 0;
            }
        }

    }
}
