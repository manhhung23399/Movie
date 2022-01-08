using Microsoft.Extensions.Options;
using Movie.Core;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Reponsitories
{
    public class AccountReponsitory : IAccountReponsitory
    {
        private readonly IFirebaseManager _firebaseManager;
        private readonly string _endpointRefreshToken;
        public AccountReponsitory(IFirebaseManager firebase, IOptions<AppSettings> options)
        {
            _firebaseManager = firebase;
            _endpointRefreshToken = options.Value.RefreshToken ?? "";
        }
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(string token, string password)
        {
            try
            {
                var changePassword = await _firebaseManager.Authentication().ChangeUserPassword(token, password);
                if (changePassword == null) throw new Exception(Notify.Account.NOTIFY_ACCOUT_CHANGEPASSWORD_FAILTURE);
                return true;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Register account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(AccountRegister account)
        {
            try
            {
                var registerAccount = await _firebaseManager.Authentication().CreateUserWithEmailAndPasswordAsync(
                    account.Email,
                    account.Password,
                    account.DisplayName,
                    true);
                if (registerAccount == null) throw new Exception(Notify.Account.NOTIFY_ACCOUT_REGISTER_FAILURE);
                AccountDto accountDatabase = new AccountBuilder()
                        .WithDisplayName(registerAccount.User.DisplayName)
                        .WithUserId(registerAccount.User.LocalId)
                        .WithRole(account.Role)
                        .WithPhotoUrl(account.PhotoUrl)
                        .Build();
                await _firebaseManager.Database().SetAsync($"{ArgumentEntities.Account}/{registerAccount.User.LocalId}", accountDatabase);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Sign in admin
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<AccountDto> SignInAdminAsync(AccountRequest account)
        {
            try
            {
                var signinInfor = await SignInAsync(account);
                var checkUserCurrent = await _firebaseManager.Database().GetAsync($"{ArgumentEntities.Account}/{signinInfor.UserId}");
                if (checkUserCurrent.Body == "null") throw new Exception(Notify.Account.NOTIFY_ACCOUT_FAIL);
                var data = checkUserCurrent.ResultAs<AccountDto>();
                signinInfor.Role = data.Role;
                return signinInfor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Sign in account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<AccountDto> SignInAsync(AccountRequest account)
        {
            try
            {
                var signinInfor = await _firebaseManager.Authentication().SignInWithEmailAndPasswordAsync(account.Email, account.Password);
                if (signinInfor == null) throw new Exception(Notify.Account.NOTIFY_ACCOUT_FAIL);
                if(!signinInfor.User.IsEmailVerified) throw new Exception(Notify.Account.NOTIFY_ACCOUT_VERIED);
                return new AccountBuilder()
                        .WithToken(signinInfor.FirebaseToken)
                        .WithRefreshToken(signinInfor.RefreshToken)
                        .WithDisplayName(signinInfor.User.DisplayName)
                        .WithUserId(signinInfor.User.LocalId)
                        .WithIsEmailVerified(signinInfor.User.IsEmailVerified)
                        .WithEmail(signinInfor.User.Email)
                        .Build();
            }
            catch(Exception ex)
            {
                if (ex.Message.IndexOf("INVALID_PASSWORD") > -1)
                {
                    throw new Exception(Notify.Account.NOTIFY_ACCOUNT_INVALID_PASSWORD);
                }
                if (ex.Message.IndexOf("EMAIL_NOT_FOUND") > -1)
                {
                    throw new Exception(Notify.Account.NOTIFY_ACCOUNT_EMAIL_NOT_FOUND);
                }
                if (ex.Message.IndexOf("TOO_MANY_ATTEMPTS_TRY_LATER") > -1)
                {
                    throw new Exception(Notify.Account.NOTIFY_ACCOUNT_TOO_MANY_ATTEMPTS_TRY_LATER);
                }
                if (ex.Message.IndexOf("INVALID_EMAIL") > -1)
                {
                    throw new Exception(Notify.Account.NOTIFY_ACCOUT_INVALID_EMAIL);
                }
                throw ex;
            }
        }
        public async Task ForgotPasswordAsync(string email)
        {
            try
            {
                await _firebaseManager.Authentication().SendPasswordResetEmailAsync(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TokenDto> RefreshToken(string refresh_token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var pairs = new Dictionary<string, string>();
                pairs.Add("grant_type", "refresh_token");
                pairs.Add("refresh_token", refresh_token);

                var req = await client.PostAsync(_endpointRefreshToken, new FormUrlEncodedContent(pairs));
                string res = await req.Content.ReadAsStringAsync();

                TokenDto refreshToken = JsonConvert.DeserializeObject<TokenDto>(res);
                return refreshToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(string movieId)
        {
            try
            {
                var accounts = await _firebaseManager.Database().GetAsync($"{ArgumentEntities.Account}/{movieId}");
                var data = accounts.ResultAs<Dictionary<string, Account>>().Values.ToList();
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
