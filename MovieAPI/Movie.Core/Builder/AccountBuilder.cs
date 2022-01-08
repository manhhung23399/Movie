using Movie.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Builder
{
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
