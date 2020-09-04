using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidations
{
    public class Validations
    {
        public static int CalculateAge(DateTime birthdate)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(birthdate.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
            return age;
        }
    }
}
