﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.LogicLayer.Interfaces
{
    public interface IUserDAO
    {
        Task<bool> Register(User _user);
        Task<string> Login(string Userid, string password);
        Task<string> GetUserByUserID(string Userid);
    }
}
