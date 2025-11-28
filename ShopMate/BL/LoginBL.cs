using ShopMate.DTOs;
using ShopMate.DL;
using ShopMate.GUI;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    internal class LoginBL
    {
        private readonly LoginDL loginDL;

        public LoginBL()
        {
            loginDL = new LoginDL();

        }

        public async Task<bool> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await loginDL.ValidateLoginAsync(loginDTO);

            if (user == null)
                return false;

            // Navigate based on roleID instead of role string
            if (user.RoleID == 7)  // Assuming 1 = Admin
                App.MainFrame?.Navigate(typeof(AdminDashboardPage), user);
            else if (user.RoleID == 8)  // Assuming 2 = Salesperson
                App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage), user);
            else
                return false;

            return true;
        }
    }
}
