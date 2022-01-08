using Microsoft.AspNetCore.Mvc;
using Movie.Core.Constants;
using Movie.Core.Dtos;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Request;
using Movie.Core.Resources.Response;
using System;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]AccountRequest account)
        {
            var accountSignIned = await _unitOfWork.Account.SignInAsync(account);

            return Ok(ResponseBase.Success(accountSignIned));
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]AccountRegister accountRegister)
        {
            await _unitOfWork.Account.RegisterAsync(accountRegister);
            return Ok(ResponseBase.Success(Notify.Account.NOTIFY_ACCOUT_REGISTER_SUCCESS));
        }
        [HttpPost]
        [Route("password")]
        public async Task<IActionResult> ChangePassword([FromBody]PasswordChangeRequest password)
        {
            if (!string.IsNullOrEmpty(password.Email))
            {
                await _unitOfWork.Account.ForgotPasswordAsync(password.Email);
                return Ok();
            }
            await _unitOfWork.Account.ChangePasswordAsync(password.Token, password.Password);
            return Ok(ResponseBase.Success(Notify.Account.NOTIFY_ACCOUT_CHANGEPASSWORD_SUCCESS));
        }
        [HttpPost]
        [Route("refresh_token")]
        public async Task<IActionResult> RefreshToken([FromBody]string refreshToken)
        {
            TokenDto refreshTokenResult = await _unitOfWork.Account.RefreshToken(refreshToken);
            return Ok(ResponseBase.Success(refreshTokenResult));
        }
    }
}
