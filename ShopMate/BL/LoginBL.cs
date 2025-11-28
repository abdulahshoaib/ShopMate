using ShopMate.DTOs;
using ShopMate.DL;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
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
            var userdto = await loginDL.ValidateLoginAsync(loginDTO);
            if (userdto == null) return false;

            if (userdto.Role == "Admin")
            {
                App.MainFrame?.Navigate(typeof(AdminDashboardPage), userdto);
                return true;
            }
            else if (userdto.Role == "Salesperson")
            {
                App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage), userdto);
                return true;
            }

            return false;
        }
    }
}
