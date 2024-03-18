using System.Collections.ObjectModel;
using System.Runtime.Serialization.DataContracts;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using pwManager.Models;
//using pwManager.Entities;

namespace pwManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Credentials _credentials;
        private Database _database;


        public ObservableCollection<DecryptedAccount> decryptedAccounts {  get; private set; }




        public MainWindow(Credentials credentials, Database database)
        {
            InitializeComponent();
            _database = database;
            _credentials = credentials;

            DataContext = this;
            decryptedAccounts = new ObservableCollection<DecryptedAccount>();


            refreshAccountList();


        }




        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddAccount dialog = new AddAccount();
            if (dialog.ShowDialog() == true)
            {
                // Values retrieved from the dialog
                string name = dialog.NameValue;
                string password = dialog.PasswordValue;
                string userName = dialog.UserNameValue;

                _database.AddNewAccount(name, userName, password, _credentials);

                ResetEditSection();

                refreshAccountList();
            }
        }



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the IsReadOnly property of the TextBoxes
            NameTextBox.IsReadOnly = !NameTextBox.IsReadOnly;
            UsernameTextBox.IsReadOnly = !UsernameTextBox.IsReadOnly;
            PasswordTextBox.IsReadOnly = !PasswordTextBox.IsReadOnly;

            // Change the text of the button based on the editing state
            if (NameTextBox.IsReadOnly)
            {
                EditButton.Content = "Edit";
                // Set background color to indicate read-only mode
                NameTextBox.Background = UsernameTextBox.Background = PasswordTextBox.Background = Brushes.LightGray;
                EditAccount(accountListBox.SelectedItem as DecryptedAccount);

            }
            else
            {
                EditButton.Content = "Save";
                // Set background color to indicate editable mode
                NameTextBox.Background = UsernameTextBox.Background = PasswordTextBox.Background = Brushes.White;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DecryptedAccount account = accountListBox.SelectedItem as DecryptedAccount;

            _database.DeleteAccount(account.Id);
            refreshAccountList();
        }


        private void accountListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetEditSection();
        }



        private void EditAccount (DecryptedAccount decryptedAccount)
        {
            DecryptedAccount updatedAccount = new DecryptedAccount();
            updatedAccount.Id = decryptedAccount.Id;
            updatedAccount.Name = NameTextBox.Text;
            updatedAccount.UserName = UsernameTextBox.Text;
            updatedAccount.Password = PasswordTextBox.Text;



            _database.EditAccount(updatedAccount, _credentials);
            refreshAccountList();
        }




        private void refreshAccountList ()
        {
            decryptedAccounts.Clear();

            List<DecryptedAccount> decryptedAccountsList = _database.GetAccounts(_credentials);
            foreach (DecryptedAccount decryptedAccount in decryptedAccountsList)
            {
                decryptedAccounts.Add(decryptedAccount);
            }
        }

        private void ResetEditSection()
        { 
            NameTextBox.IsReadOnly = true;
            UsernameTextBox.IsReadOnly = true;
            PasswordTextBox.IsReadOnly = true;
            EditButton.Content = "Edit";
            NameTextBox.Background = UsernameTextBox.Background = PasswordTextBox.Background = Brushes.LightGray;

        }

        
    }
}