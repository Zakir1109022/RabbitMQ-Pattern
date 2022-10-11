using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Infrastructure.TenantConfig
{
    public class Tenant
    {
        public string Name { get; set; }
        public string TID { get; set; }
        public string ConnectionString { get; set; }
    }
}
