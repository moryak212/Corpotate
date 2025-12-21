using Microsoft.AspNetCore.Identity;

namespace Corporate.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    // поле выбора: отдел
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    // поле выбора: должность/роль в компании (не security-role)
    public string Position { get; set; } = string.Empty;
}
