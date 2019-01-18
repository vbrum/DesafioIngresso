using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DesafioIngressoCom.Models
{
    public class Cinema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nome")]
        public string Nome { get; set; }
        [BsonElement("local")]
        public string Local { get; set; }
        [BsonElement("salas")]
        public List<Sala> Salas { get; set; }
    }
}
