using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Dtos
{
    public class AccountDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public AccountDto(string token, string refreshToken, string userId, string displayName, string email, bool isEmail, string? role, string? photoUrl)
        {
            Token = token;
            RefreshToken = refreshToken;
            UserId = userId;
            DisplayName = displayName;
            Email = email;
            IsEmailVerified = isEmail;
            Role = role;
            PhotoUrl = photoUrl;
        }
    }

    public class AccountBuilder
    {
        private string _token { get; set; }
        private string _refreshToken { get; set; }
        private string _userId { get; set; }
        private string _displayName { get; set; }
        private string _email { get; set; }
        private bool _isEmailVerified { get; set; }
        private string _role { get; set; }
        private string _photoUrl { get; set; }
        public AccountBuilder WithToken(string token)
        {
            _token = token;
            return this;
        }
        public AccountBuilder WithPhotoUrl(string photoUrl)
        {
            _photoUrl = photoUrl;
            return this;
        }
        public AccountBuilder WithRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
            return this;
        }
        public AccountBuilder WithUserId(string localId)
        {
            _userId = localId;
            return this;
        }
        public AccountBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }
        public AccountBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public AccountBuilder WithIsEmailVerified(bool isEmailVerified)
        {
            _isEmailVerified = isEmailVerified;
            return this;
        }
        public AccountBuilder WithRole(string role)
        {
            _role = role;
            return this;
        }
        public AccountDto Build()
        {
            return new AccountDto(_token, _refreshToken, _userId, _displayName, _email, _isEmailVerified, _role, _photoUrl);
        }
    }
}
