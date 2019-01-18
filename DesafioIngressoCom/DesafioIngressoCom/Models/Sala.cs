using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DesafioIngressoCom.Models
{
    public class Sala
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nome")]
        public string NomeSala { get; set; }
        [BsonElement("capacidade")]
        [BsonRequired]
        public int Capacidade { get; set; }
    }
}
