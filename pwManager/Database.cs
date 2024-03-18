using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pwManager.Models;

namespace pwManager
{
    public class Database
    {
        PwManagerContext _context;
        EncryptionHelper _encryptionHelper;

        public Database(PwManagerContext dbContext)
        {
            this._context = dbContext;
            _encryptionHelper = new EncryptionHelper();

        }


        public User GetUser(string username)
        {
            User user = null;
            try
            {
                user = _context.Users.FirstOrDefault(x => x.UserName == username);


            }
            catch { }

            return user;
        }

        public string GetUserSalt(User user)
        {
            byte[] salt = GetUser(user.UserName).Salt;
            return Convert.ToBase64String(salt);
        }
        
        //this method is used for testing it to work. it does not check, if the username already exists
        public void CreateNewUser(string username, string password)
        { 
            byte[] salt = RandomNumberGenerator.GetBytes(50);
            HashHelper hs = new HashHelper();
            string passwordHash = hs.IterateHash(password, Convert.ToBase64String(salt), GlobalVariables.iterations);

            User user = new User();
            user.UserName = username;
            user.Salt = salt;
            user.PasswordHash = Convert.FromBase64String(passwordHash);
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddNewAccount (string name, string username, string password, Credentials credentials)
        {
            byte[] iv = RandomNumberGenerator.GetBytes(16);

            Account account = new Account();
            account.Name = name;
            account.UserName = username;
            account.Password = _encryptionHelper.EncryptStringToBytes_Aes(password, credentials.getKey(), iv);
            account.Iv = iv;
            account.BelongsToUser = credentials._user.UserId;

            _context.Accounts.Add(account);
            _context.SaveChanges();

        }

        public List<DecryptedAccount> GetAccounts(Credentials credentials)
        {
            List<Account> accounts = _context.Accounts.Where(a => a.BelongsToUser ==  credentials._user.UserId).ToList();
            List<DecryptedAccount> decryptedAccounts = new List<DecryptedAccount>();

            foreach (Account account in accounts)
            {
                DecryptedAccount decryptedAccount = new DecryptedAccount();
                decryptedAccount.Id = account.Id;
                decryptedAccount.Name = account.Name;
                decryptedAccount.UserName = account.UserName;
                decryptedAccount.Password = _encryptionHelper.DecryptStringFromBytes_Aes(account.Password, credentials.getKey(), account.Iv);

                decryptedAccounts.Add(decryptedAccount);
            }


            return decryptedAccounts;
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a =>  a.Id == id);
        }


        public void EditAccount (DecryptedAccount decryptedAccount, Credentials credentials)
        { 
            Account account = GetAccountById(decryptedAccount.Id);

            account.Name = decryptedAccount.Name;
            account.UserName = decryptedAccount.UserName;
            account.Password = _encryptionHelper.EncryptStringToBytes_Aes(decryptedAccount.Password, credentials.getKey(), account.Iv);

            _context.Accounts.Update(account);
            _context.SaveChanges();
        }


        public void DeleteAccount (int id)
        {
            Account a = GetAccountById(id);
            _context.Accounts.Remove(a);
            _context.SaveChanges();
        }


    }
}
