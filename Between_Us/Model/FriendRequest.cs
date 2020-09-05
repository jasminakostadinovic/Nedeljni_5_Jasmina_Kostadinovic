using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Us.Model
{
    public partial class tblFriendRequest
    {
        public string Surname
        {
            get
            {
                return tblUser.Surname;
            }
        }
        public string GivenName
        {
            get
            {
                return tblUser.GivenName;
            }
        }
        public string Username
        {
            get
            {
                return tblUser.Username;
            }
        }
        public string Sex
        {
            get
            {
                return tblUser.Sex;
            }
        }
    }
}
