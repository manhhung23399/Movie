using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface IAccountReponsitory
    {
        Task<bool> SignIn(Account account);
    }
}
