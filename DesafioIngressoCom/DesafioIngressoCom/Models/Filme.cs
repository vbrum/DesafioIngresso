using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioIngressoCom.Models
{
    public class Filme
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("titulo")]
        public string Titulo { get; set; }
        [BsonElement("genero")]
        public string Genero { get; set; }
        [BsonElement("versao")]
        public string Versao { get; set; }
    }
}
