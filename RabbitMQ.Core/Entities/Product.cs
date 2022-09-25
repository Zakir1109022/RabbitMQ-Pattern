using RabbitMQ.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Core
{
    [BsonCollection("product")]
    public class Product:Document
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
