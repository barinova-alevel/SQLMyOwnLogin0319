﻿using MyOwnLogin.Core.Models;
using MyOwnLogin.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLogin.BusinesLogic.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(LoginVm loginVm);

        bool IsExitedByName(string login);
    }
}
