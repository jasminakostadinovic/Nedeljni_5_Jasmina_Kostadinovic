using DataValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Us.Model
{
    class BetweenUsValidation
    {
        internal bool IsCorrectUser(string userName, string password)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    var user = conn.tblUsers.FirstOrDefault(x => x.Username == userName);

                    if (user != null)
                    {
                        var passwordFromDb = conn.tblUsers.First(x => x.Username == userName).Password;
                        return SecurePasswordHasher.Verify(password, passwordFromDb);
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
