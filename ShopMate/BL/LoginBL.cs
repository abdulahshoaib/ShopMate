using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopMate.DL;
using ShopMate.DTOs;
using ShopMate.GUI;

namespace ShopMate.BL
{
    internal class LoginBL
    {
        private LoginDTO logindto;
        private LoginDL loginDL;
        private UserDTO userdto;

        public LoginBL()
        {
            loginDL = new LoginDL();
        }

        // Example function
        public bool loginuser(LoginDTO logindto)
        {
            this.logindto = logindto;
            userdto= loginDL.ValidateLogin(this.logindto);
            if (userdto.Role == "Admin")
            {
                App.MainFrame?.Navigate(typeof(AdminDashboardPage));
                return true;
            }
            else if (userdto.Role == "Salesperson")
            {
                App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage));
                return true;
            }
            else { 
            return false;
            }
        }
    }
}
