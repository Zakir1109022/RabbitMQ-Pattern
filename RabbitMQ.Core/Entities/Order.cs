using RabbitMQ.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Core
{

    [BsonCollection("order")]
    public class Order: Document
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string EmailAddress { get; set; }
    }
}
