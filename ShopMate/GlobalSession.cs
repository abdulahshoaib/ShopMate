using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate
{
    public static class GlobalSession
    {
        public static UserDTO? CurrentUser { get; set; }
        public static EmployeeDTO? CurrentEmployee { get; set; }

        public static string DisplayUsername =>
            CurrentEmployee?.Name?? "Unknown User";
    }
}
