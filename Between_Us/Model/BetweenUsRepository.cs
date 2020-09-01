using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Us.Model
{
    class BetweenUsRepository
    {
        internal int GetUserDataId(string userName)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    var user = conn.tblUsers.FirstOrDefault(x => x.Username == userName);
                    if (user != null)
                        return user.UserID;
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
