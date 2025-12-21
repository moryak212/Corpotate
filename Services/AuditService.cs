using Corporate.Data;
using Corporate.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Corporate.Services;

public class AuditService
{
    private readonly CorporateDbContext _db;
    private readonly AuthenticationStateProvider _auth;

    public AuditService(CorporateDbContext db, AuthenticationStateProvider auth)
    {
        _db = db;
        _auth = auth;
    }

    public async Task LogAsync(string action, string entityName, object entityId)
    {
        var authState = await _auth.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity?.IsAuthenticated ?? true)
            return;

        var log = new AuditLog
        {
            UserId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
            UserEmail = user.Identity!.Name ?? "",
            Action = action,
            EntityName = entityName,
            EntityId = entityId.ToString() ?? ""
        };

        _db.AuditLogs.Add(log);
        await _db.SaveChangesAsync();
    }
}
