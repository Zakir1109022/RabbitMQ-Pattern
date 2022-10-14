using RabbitMQ.Common.TenantConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Common.Services
{
   public interface ITenantService
    {
        public Tenant GetTenant();
    }
}
