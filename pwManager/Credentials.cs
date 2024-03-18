using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwManager.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace pwManager
{
    public class Credentials
    {
        private string _username;
        private string _password;
        public User _user { get; set; }
        Database _db;

        HashHelper hs = new HashHelper();


        public Credentials(string username, string password, Database db)
        {
            _username = username;
            _password = password;
            _db = db;
        }

        public bool checkUsername()
        {
            _user = _db.GetUser(_username);
            if (_user == null)
            {
                //calculate fake hash for same time delay 
                hs.IterateHash(_password, _username, GlobalVariables.iterations);
                return false;
            }
            return true; 
        }

        public bool checkPassword()
        {
            
            string salt = _db.GetUserSalt(_user);
    
            string hash = hs.IterateHash(_password, salt, GlobalVariables.iterations);


            if (hash == Convert.ToBase64String(_user.PasswordHash))
                return true;
            return false;
        }

        public byte[] getKey() 
        {
            string key = hs.ComputeHash_SHA256(_username + _password);
            return Convert.FromBase64String(key);
        }
    }
}
