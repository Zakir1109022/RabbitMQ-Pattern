using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Infrastructure.TenantConfig
{
   public interface ITenantSettings
    {
        List<Tenant> Tenants { get; set; }
    }
}
