using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Common.TenantConfig
{
   public interface ITenantSettings
    {
        List<Tenant> Tenants { get; set; }
    }
}
