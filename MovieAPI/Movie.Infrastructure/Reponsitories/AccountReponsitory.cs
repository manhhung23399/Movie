using Movie.Core.Constants;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Reponsitories
{
    public class AccountReponsitory : IAccountReponsitory
    {
        private readonly IFirebaseManager _firebaseManager;
        public AccountReponsitory(IFirebaseManager firebase)
        {
            _firebaseManager = firebase;
        }
        public async Task<bool> SignIn(Account account)
        {
            try
            {
                var checkUserCurrent = await _firebaseManager.Database().GetAsync(ArgumentEntities.Account);
                if (checkUserCurrent.Body != "null") return false;
                throw new Exception(Notify.NOTIFY_ACCOUT_FAIL);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
