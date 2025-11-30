using ShopMate.DTOs;

namespace ShopMate
{
    public static class GlobalSession
    {
        public static UserDTO? CurrentUser { get; set; }
        public static EmployeeDTO? CurrentEmployee { get; set; }

        public static string DisplayUsername =>
            CurrentEmployee?.Name ?? "Unknown User";
    }
}
