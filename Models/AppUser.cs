using Microsoft.AspNetCore.Identity;

namespace Corporate.Models;

public class AppUser : IdentityUser
{
    // ФИО
    public string FullName { get; set; } = string.Empty;

    // Отдел (выпадающий список)
    public string Department { get; set; } = string.Empty;

    // Роль в компании (не путать с ролью безопасности)
    public string CompanyRole { get; set; } = string.Empty;

    // Дата найма (опционально)
    public DateTime? HireDate { get; set; }
}
