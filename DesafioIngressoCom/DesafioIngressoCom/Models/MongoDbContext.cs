using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioIngressoCom.Models
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }

        public IMongoDatabase _database { get; }
      

        public MongoDbContext()
        {
            try
            {
                IMongoClient mongoClient = new MongoClient(ConnectionString);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<Filme> Filmes
        {
            get
            {
                return _database.GetCollection<Filme>("Filmes");
            }
        }

        public IMongoCollection<Sessao> Sessoes
        {
            get
            {
                return _database.GetCollection<Sessao>("Sessao");
            }
        }

        public IMongoCollection<Cinema> Cinemas
        {
            get
            {
                return _database.GetCollection<Cinema>("Cinema");
            }
        }

        public IMongoCollection<Sala> Salas
        {
            get
            {
                return _database.GetCollection<Sala>("Sala");
            }
        }

    }
}
