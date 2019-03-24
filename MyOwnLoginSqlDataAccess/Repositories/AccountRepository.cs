using System.Collections.Generic;
using System.Linq;
using MyOwnLogin.Core.Models;
using MyOwnLogin.Core.ViewModels;
using MyOwnLoginSqlDataAccess.Interfaces;

namespace MyOwnLoginSqlDataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private List<Account> accountList = new List<Account>
        {
            new Account
            {
                UserName = "Mr Garry Potter",
                Login = "garry",
                Password = "potter"
            },

            new Account
            {
                UserName = "Lady Gaga",
                Login = "lady",
                Password = "fox"
            }
        };

        public Account GetAccount(LoginVm loginVm)
        {
            return accountList.SingleOrDefault(a => a.Login == loginVm.Login && a.Password == loginVm.Password);
        }

        public bool IsExitedByName(string login)
        {
            return accountList.Any(a => a.Login == login);
        }
    }
}