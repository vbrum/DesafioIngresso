using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesafioIngressoCom.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace DesafioIngressoCom.Controllers
{
    public class FilmeController : Controller
    {
        public IActionResult Index()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Filme> listaFilmes = dbContext.Filmes.Find(new BsonDocument()).ToList();
            return View(listaFilmes);
        }

        public IActionResult List(List<Filme> listaFilmes)
        {
            return View();
        }

        public IActionResult ListAll()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Filme> listaFilmes = dbContext.Filmes.Find(new BsonDocument()).ToList();
            return View("List", listaFilmes);
        }

        public IActionResult ListPerGenre(string genero)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Filme> listaFilmes = dbContext.Filmes.Find(m => m.Genero == genero).ToList();
            return View("List",listaFilmes);
        }

        public IActionResult ListPerVersion(string versao)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Filme> listaFilmes = dbContext.Filmes.Find(m => m.Versao == versao).ToList();
            return View("List", listaFilmes);
        }

        public IActionResult ListPerVersionAndGenre(string genero, string versao)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Filme> listaFilmes = dbContext.Filmes.Find(m => m.Versao == versao && m.Genero == genero).ToList();
            return View("List", listaFilmes);
        }

        public IActionResult Details(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Filmes.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {                      
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Filmes.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Filme entity)
        {
            MongoDbContext dbContext = new MongoDbContext();

            List<Sessao> listaSessoes = dbContext.Sessoes.Find(new BsonDocument()).ToList();
            foreach (var item in listaSessoes)
            {
                if(item.Filme.Id == entity.Id)
                {
                    item.Filme = entity;
                    dbContext.Sessoes.ReplaceOne(m => m.Id == item.Id, item);
                }
            }

            dbContext.Filmes.ReplaceOne(m => m.Id == entity.Id, entity);
            return RedirectToAction("Index", "Filme");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Filme filme)
        {
            MongoDbContext dbContext = new MongoDbContext();
            filme.Id = ObjectId.GenerateNewId().ToString();      
            dbContext.Filmes.InsertOne(filme);
            return RedirectToAction("Index", "Filme");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Filmes.Find(n => n.Id == id).FirstOrDefault();
            dbContext.Filmes.DeleteOne(m => m.Id == id);
            return RedirectToAction("Delete", "Sessao", entity);
        }
    }
}