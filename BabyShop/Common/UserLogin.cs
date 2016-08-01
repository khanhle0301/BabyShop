using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyShop
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
    }
}