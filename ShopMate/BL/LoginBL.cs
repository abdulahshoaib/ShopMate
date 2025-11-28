using ShopMate.DTOs;
using ShopMate.DL;
using ShopMate.GUI;

namespace ShopMate.BL
{
    internal class LoginBL
    {
        private LoginDL loginDL;
        private UserDTO userdto;

        public LoginBL()
        {
            loginDL = new LoginDL();

        }

        // Example function
        public bool loginuser(LoginDTO logindto)
        {
            userdto = loginDL.ValidateLogin(logindto);
            if (userdto != null)
            {
                if (userdto.Role == 1)
                {
                    App.MainFrame?.Navigate(typeof(AdminDashboardPage));
                    return true;
                }
                else if (userdto.Role == 2)
                {
                    App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage));
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }
    }
}
