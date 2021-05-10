using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAWSDynamoDb.Models
{
    [DynamoDBTable("coches")]
    public class Coche
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("idcoche")]
        public int IdCoche { get; set; }
        [DynamoDBProperty("marca")]
        public String Marca { get; set; }
        [DynamoDBProperty("modelo")]
        public String Modelo { get; set; }
        [DynamoDBProperty("velocidadmaxima")]
        public int VelocidadMaxima { get; set; }
        [DynamoDBProperty("motor")]
        public Motor Motor { get; set; }
    }
}
