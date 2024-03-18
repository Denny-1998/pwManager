using System;
using System.Collections.Generic;
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
using pwManager.Models;

namespace pwManager
{
    /// <summary>
    /// Interaktionslogik für LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        Database db = new Database(new PwManagerContext());
        Credentials _credentials;


        public LogInWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckCredentials(usernameTextBox.Text, passwordTextBox.Password))
            {
                MessageBox.Show("wrong username or password.");
                return;
            }


            MainWindow mainWindow = new MainWindow(_credentials, db);
            mainWindow.Show();
            this.Close();
        }

        private bool CheckCredentials(string username, string password)
        {
            Credentials credentials = new Credentials(username, password, db);
            _credentials = credentials;

            return credentials.checkUsername() && credentials.checkPassword();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        { 
        
            db.CreateNewUser(usernameTextBox.Text, passwordTextBox.Password);
            MessageBox.Show("User created");
            usernameTextBox.Clear();
            passwordTextBox.Clear();
        }
    }
}
