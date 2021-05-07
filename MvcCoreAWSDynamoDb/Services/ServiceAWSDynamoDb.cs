using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MvcCoreAWSDynamoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAWSDynamoDb.Services
{
    public class ServiceAWSDynamoDb
    {
        private DynamoDBContext context;

        public ServiceAWSDynamoDb()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateCoche(Coche car)
        {
            await this.context.SaveAsync<Coche>(car);
        }

        public async Task<List<Coche>> GetCoches()
        {
            //PRIMERO TENEMOS QUE RECUPERAR LA TABLA
            //LA RECUPERACIÓN DE LA TABLA ES SÚPER SENCILLA
            //PARA RECUPERAR LA TABLA, BASTA CON HABER MAPEADO
            //EL MODEL CON [DynamoDBTable]
            var tabla = this.context.GetTargetTable<Coche>();
            var scanOptions = new ScanOperationConfig();
            //scanOptions.PaginationToken = "";
            var results = tabla.Scan(scanOptions);
            //LOS DATOS QUE RECUPERAMOS SON Document
            //Y DEVUELVE UNA COLECCIÓN
            List<Document> data = await results.GetNextSetAsync();
            //DEBEMOS TRANSFORMAR DICHOS DATOS A SU TIPADO
            //ESTO ES AUTOMÁTICO MEDIANTE UN MÉTODO
            IEnumerable<Coche> cars = this.context.FromDocuments<Coche>(data);
            return cars.ToList();
            //return this.context.FromDocuments<Coche>(data).ToList();
        }

        public async Task<Coche> FindCoche(int idcoche)
        {
            //SI ESTAMOS BUSCANDO POR PARTITION KEY (PRIMARY KEY)
            //HASHKEY SOLAMENTE DEBEMOS HACERLO CON LOAD
            //ESTO ES EQUIVALENTE A BUSCAR CON CONSULTA
            return await this.context.LoadAsync<Coche>(idcoche);
        }

        public async Task DeleteCoche(int idcoche)
        {
            await this.context.DeleteAsync<Coche>(idcoche);
        }
    }
}
