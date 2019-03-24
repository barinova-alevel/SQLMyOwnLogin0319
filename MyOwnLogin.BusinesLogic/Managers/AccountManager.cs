using MyOwnLogin.BusinesLogic.Interfaces;
using MyOwnLogin.Core.Models;
using MyOwnLogin.Core.ViewModels;
using MyOwnLogin.DataAccess.Helpers;
using MyOwnLogin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyOwnLogin.BusinesLogic.Managers
{
    public class AccountManager : IAccountManager
    {
        private AccountRepository _repo;

        public AccountManager()
        {
            _repo = new AccountRepository();
        }

        public Account GetAccount(LoginVm loginVm)
        {
            return _repo.GetAccount(loginVm);
        }

        public bool IsExitedByName(string login)
        {
            return _repo.IsExitedByName(login);
        }

        public Token GetToken(LoginVm loginVm)
        {
            TokenHelper tokenHelper = new TokenHelper();
            var token = tokenHelper.GetRefreshToken(loginVm.Login, loginVm.Password);

            return token;
        }

    }
}
