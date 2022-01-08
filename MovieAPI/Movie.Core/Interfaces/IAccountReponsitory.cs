using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Resources.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Interfaces
{
    public interface IAccountReponsitory
    {
        Task<IEnumerable<Account>> GetAccountsAsync(string movieId);
        Task<AccountDto> SignInAsync(AccountRequest account);
        Task<bool> RegisterAsync(AccountRegister account);
        Task<bool> ChangePasswordAsync(string token, string password);
        Task<AccountDto> SignInAdminAsync(AccountRequest account);
        Task ForgotPasswordAsync(string email);
        Task<TokenDto> RefreshToken(string refresh_token);
    }
}
