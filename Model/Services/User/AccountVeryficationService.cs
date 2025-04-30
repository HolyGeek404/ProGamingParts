using Model.DataAccess.Interfaces;
using Model.Services.Interfaces;

namespace Model.Services.User
{
    public class AccountVerificationService(IUserDao userDao) : IAccountVerifycationService
    {
        public bool VerifyAccount(string userEmail, Guid key)
        {
            var result = userDao.VerifyAccount(userEmail, key);
            return result;
        }
    }
}
