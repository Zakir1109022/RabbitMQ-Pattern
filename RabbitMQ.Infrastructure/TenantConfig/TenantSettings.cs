using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Infrastructure.TenantConfig
{
    public class TenantSettings : ITenantSettings
    {
        public List<Tenant> Tenants { get; set; }
    }
}
