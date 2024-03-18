using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pwManager
{
    /// <summary>
    /// Interaktionslogik für AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public string NameValue { get; private set; }
        public string PasswordValue { get; private set; }
        public string UserNameValue { get; private set; }



        public AddAccount()
        {

            InitializeComponent();

        }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            NameValue = nameTextBox.Text;
            PasswordValue = passwordBox.Text;
            UserNameValue = userNameTextBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Define the length of the random password
            int length = 20; 

            // Characters to be used in generating the random password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789,.?[]{}!@#$%&*()-+=";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            passwordBox.Text = password.ToString();
        }



    }
}


