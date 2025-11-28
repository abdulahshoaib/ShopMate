using ShopMate.DTOs;
using ShopMate.DL;
<<<<<<< HEAD
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
=======
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2
using ShopMate.GUI;

namespace ShopMate.BL
{
    internal class LoginBL
    {
        private LoginDL loginDL;

        public LoginBL()
        {
            loginDL = new LoginDL();

        }

        public async Task<bool> loginuserAsync(LoginDTO loginDTO)
        {
<<<<<<< HEAD
            var userdto = await loginDL.ValidateLoginAsync(loginDTO);
            if (userdto == null) return false;

            if (userdto.Role == "Admin")
            {
                App.MainFrame?.Navigate(typeof(AdminDashboardPage), userdto);
                return true;
=======
            userdto = loginDL.ValidateLogin(logindto);
            if (userdto != null)
            {
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
                else 
                {
                    return false;
                }
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2
            }
            else 
            {
<<<<<<< HEAD
                App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage), userdto);
                return true;
            }

            return false;
=======
                return false;
            }
>>>>>>> 35e9e22d25c513d466883fe8ec25ae9cb75792c2
        }
    }
}
