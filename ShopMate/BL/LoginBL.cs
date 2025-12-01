using ShopMate.DL;
using ShopMate.DTOs;
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

            GlobalSession.CurrentUser = user;

            if (user.EmployeeID != null)
            {
                var emBL = new EmployeeManagementBL();
                GlobalSession.CurrentEmployee = await emBL.GetEmployee(user.EmployeeID.Value);
            }

            if (user.RoleID == 7)  // Assuming 7 = Admin
                App.MainFrame?.Navigate(typeof(AdminDashboardPage), user);
            else if (user.RoleID == 8)  // Assuming 8 = Salesperson
                App.MainFrame?.Navigate(typeof(SalesPersonDashboardPage), user);
            else
                return false;

            return true;
        }
    }
}
