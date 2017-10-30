using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
        Grid mainGrid = new Grid();
        Entry resultEntry = new Entry();
		public MainPage()
        {
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            mainGrid.Children.Add(resultEntry, 0, 0);
            Grid.SetColumnSpan(resultEntry, mainGrid.ColumnDefinitions.Count/* =4 */);

            for(int i=0;i<=9;i++)
            {
                Button numButton = new Button { Text = i.ToString() };
                numButton.Clicked += NumberPoked;
                if (i == 0) mainGrid.Children.Add(numButton, 1, 4);
                else mainGrid.Children.Add(numButton, (i - 1) % 3, (i - 1) / 3 + 1);
            }

            {
                Button button = new Button { Text = "+" };
                button.Clicked += NumberPoked;
                mainGrid.Children.Add(button, 3, 1);
            }

            {
                Button button = new Button { Text = "-" };
                button.Clicked += NumberPoked;
                mainGrid.Children.Add(button, 3, 2);
            }

            {
                Button button = new Button { Text = "*" };
                button.Clicked += NumberPoked;
                mainGrid.Children.Add(button, 3, 3);
            }

            {
                Button button = new Button { Text = "/" };
                button.Clicked += NumberPoked;
                mainGrid.Children.Add(button, 3, 4);
            }

            {
                Button button = new Button { Text = "=" };
                button.Clicked += CalculateResult;
                mainGrid.Children.Add(button, 0, 4);
            }

            {
                Button button = new Button { Text = "←" };
                button.Clicked += BackspaceClicked;
                mainGrid.Children.Add(button, 2, 4);
            }

            this.Content = mainGrid;
        }

        private void BackspaceClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(resultEntry.Text)) resultEntry.Text = resultEntry.Text.Remove(resultEntry.Text.Length - 1);
        }

        private async void CalculateResult(object sender, EventArgs e)
        {
            double answer = double.NaN;
            try
            {
                var number = resultEntry.Text.Split(new char[] { '+', '-', '*', '/' });
                var sign = resultEntry.Text[number[0].Length];
                double first = double.Parse(number[0]);
                double second = double.Parse(number[1]);
                switch (sign)
                {
                    case '+':
                        answer = first + second;
                        break;
                    case '-':
                        answer = first - second;
                        break;
                    case '*':
                        answer = first * second;
                        break;
                    case '/':
                        answer = first / second;
                        break;
                }
                resultEntry.Text = answer.ToString();
            }
            catch (Exception error)
            {
                await App.Current.MainPage.DisplayAlert("", error.ToString(), "OK");
            }
        }
        
        private void NegativeButton_Clicked(object sender, EventArgs e)
        {
            if (resultEntry.Text.StartsWith("-")) resultEntry.Text = resultEntry.Text.Substring(1);
            else resultEntry.Text = "-" + resultEntry.Text;
        }

        private void NumberPoked(object sender, EventArgs e)
        {
            resultEntry.Text += (sender as Button).Text;
        }
    }
}
