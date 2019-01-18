using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioIngressoCom.Models;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioIngressoCom.Controllers
{
    public class SessaoController : Controller
    {
        public IActionResult Index()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Sessao> listaSessoes = dbContext.Sessoes.Find(m => true).ToList();
            return View(listaSessoes);
        }

        public IActionResult List(List<Filme> listaFilmes)
        {
            return View();
        }

        public IActionResult ListAll()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Sessao> listaSessoes = dbContext.Sessoes.Find(new BsonDocument()).ToList();
            return View("List", listaSessoes);
        }

        public IActionResult ListPerMovie(string titulo)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Sessao> listaSessoes = dbContext.Sessoes.Find(m => m.Filme.Titulo == titulo).ToList();
            return View("List", listaSessoes);
        }
       
        public IActionResult Details(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Sessoes.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Sessoes.Find(n => n.Id == id).FirstOrDefault();
            ViewBag.Filmes = dbContext.Filmes.Find(new BsonDocument()).ToList();
            ViewBag.Salas = dbContext.Salas.Find(new BsonDocument()).ToList();
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Sessao entity)
        {
            MongoDbContext dbContext = new MongoDbContext();
            string filmeId = Request.Form["editSelect"];
            var filme = dbContext.Filmes.Find(n => n.Id == filmeId).FirstOrDefault();
            entity.Filme = filme;
            string salaId = Request.Form["editSelect2"];
            var sala = dbContext.Salas.Find(n => n.Id == salaId).FirstOrDefault();
            entity.Sala = sala;
            dbContext.Sessoes.ReplaceOne(m => m.Id == entity.Id, entity);
            return RedirectToAction("Index", "Sessao");
        }

        [HttpGet]
        public IActionResult Add(Filme filme)
        {
            MongoDbContext dbContext = new MongoDbContext();
            ViewBag.Filmes = dbContext.Filmes.Find(new BsonDocument()).ToList();
            ViewBag.Salas = dbContext.Salas.Find(new BsonDocument()).ToList();
            return View();
            
        }

        [HttpPost]
        public IActionResult Add(Sessao sessao)
        {
            MongoDbContext dbContext = new MongoDbContext();
            sessao.Id = ObjectId.GenerateNewId().ToString();
            string filmeId = Request.Form["testeSelect"];
            var filme = dbContext.Filmes.Find(n => n.Id == filmeId).FirstOrDefault();
            string salaId = Request.Form["testeSelect2"];
            var sala = dbContext.Salas.Find(n => n.Id == salaId).FirstOrDefault();
            sessao.Filme = filme;
            sessao.Sala = sala;
            dbContext.Sessoes.InsertOne(sessao);
            return RedirectToAction("Index", "Sessao");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            string controller = "Sessao";

            var entity = dbContext.Sessoes.Find(x => x.Id == id).FirstOrDefault();
            if(entity == null)
            {
                controller = "Filme";
                dbContext.Sessoes.DeleteMany(x => x.Filme.Id == id);
            }
            else
            {
                dbContext.Sessoes.DeleteOne(m => m.Id == id);
            }
            return RedirectToAction("Index", controller);
        }
    }
}