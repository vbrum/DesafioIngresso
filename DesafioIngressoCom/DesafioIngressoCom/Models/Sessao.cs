using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DesafioIngressoCom.Models
{
    public class Sessao
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nome")]
        public string Nome { get; set; }
        [BsonElement("data")]
        public string Date { get; set; }
        [BsonElement("hora")]
        public string Time { get; set; }
        [BsonElement("sala")]
        public Sala Sala { get; set; }
        [BsonElement("filme")]
        public Filme Filme { get; set; }
    }
}
