using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RabbitMQ.Common.TenantConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RabbitMQ.Common.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantSettings _tenantSettings;
        private Tenant _currentTenant;
        private HttpContext _httpContext;
        public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor contextAccessor)
        {
            _tenantSettings = tenantSettings.Value;
            _httpContext = contextAccessor.HttpContext;

            if (_httpContext != null)
            {
                if (_httpContext.Request.Headers.TryGetValue("TenantId", out var tenantId))
                {
                    SetTenant(tenantId);
                }
                else
                {
                    var identity = _httpContext.User.Identity;
                    tenantId = (identity as ClaimsIdentity).FindFirst("TenantId").Value.ToString();

                    if (string.IsNullOrEmpty(tenantId)) throw new Exception("Invalid Tenant!");
                     
                    SetTenant(tenantId);
  
                }
            }
        }
        private void SetTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Tenants.Where(a => a.TID == tenantId).FirstOrDefault();
            if (_currentTenant == null) throw new Exception("Invalid Tenant!");
           
        }

        public Tenant GetTenant()
        {
            return _currentTenant;
        }
    }
}