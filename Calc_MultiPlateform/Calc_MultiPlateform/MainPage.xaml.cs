using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Calc_MultiPlateform
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private static string _clacValue;
        private static string _operator;
        private static string _actualValue;

        public MainPage()
        {
            InitializeComponent();

            _actualValue = "0";
            _clacValue = "";

            ActualValue.Text = _actualValue.ToString();
            CalcValue.Text = "";
        }

        void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            string number = button.Text;

            _ = _actualValue == "0" ? _actualValue = number : _actualValue += number;
            ActualValue.Text = _actualValue;

            Console.WriteLine("LOG === Number {0} clicked", number);
        }

        void OnOperatorClicked(object sender, EventArgs args)
        {
            var button = sender as Button;
            string special = button.Text;

            switch (special)
            {
                case "C":
                    if (_actualValue == "0")
                        CalcValue.Text = _clacValue = "";
                    ActualValue.Text = _actualValue = "0";
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    if (_actualValue != "0")
                    {
                        CalcValue.Text = _clacValue += _actualValue + special;
                        ActualValue.Text = _actualValue = "0";
                    }
                    break;
            }

            Console.WriteLine("LOG === Special {0} clicked", special);
        }

        void OnEqualClicked(object sender, EventArgs args)
        {
            if (_clacValue != "" && _actualValue != "0")
            {
                // TODO : do the calc
                CalcValue.Text = _clacValue = _clacValue + _actualValue;
                
                ActualValue.Text = _actualValue = "TotalResult";
            }

            Console.WriteLine("LOG === Equal button clicked");
        }
    }

}
