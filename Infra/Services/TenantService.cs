using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infra.Services;

public class TenantService: ITenantService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TenantService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public string GetTenant()
    {
        ArgumentNullException.ThrowIfNullOrEmpty(_contextAccessor?.HttpContext?.Request.RouteValues["slugTenant"]?.ToString());
        
        var tenant = _contextAccessor.HttpContext!.Request.RouteValues["slugTenant"]!.ToString();
        return tenant!;
    }
}