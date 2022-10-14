using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Common.TenantConfig
{
    public class TenantSettings : ITenantSettings
    {
        public List<Tenant> Tenants { get; set; }
    }
}
