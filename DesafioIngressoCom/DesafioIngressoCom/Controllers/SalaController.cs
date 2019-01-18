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
    public class SalaController : Controller
    {
        public IActionResult Index()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Sala> listaSalas = dbContext.Salas.Find(new BsonDocument()).ToList();     
            return View(listaSalas);
        }

        public IActionResult Details(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Salas.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Salas.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Sala entity)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Cinema> listaCinemas = dbContext.Cinemas.Find(new BsonDocument()).ToList();
            foreach(var item in listaCinemas)
            {
                var sala = item.Salas.Find(m => m.Id == entity.Id);
                if (sala != null)
                {
                    item.Salas.Remove(sala);
                    item.Salas.Add(entity);
                    dbContext.Cinemas.ReplaceOne(m => m.Id == item.Id, item);
                }
            }

            List<Sessao> listaSessoes = dbContext.Sessoes.Find(new BsonDocument()).ToList();
            foreach (var item in listaSessoes)
            {
                if(item.Sala != null && item.Sala.Id == entity.Id)
                {
                    item.Sala = entity;
                    dbContext.Sessoes.ReplaceOne(m => m.Id == item.Id, item);
                }
            }
            dbContext.Salas.ReplaceOne(m => m.Id == entity.Id, entity);
            return RedirectToAction("Index", "Sala");
        }

        public IActionResult AddToCinema()
        {
            MongoDbContext dbContext = new MongoDbContext();
            ViewBag.Cinemas = dbContext.Cinemas.Find(new BsonDocument()).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Add(Cinema cinema)
        {
            if(cinema.Id != null)
            {
                string id = cinema.Id.ToString();
                TempData["Cinema"] = id;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Sala sala)
        {
            MongoDbContext dbContext = new MongoDbContext();
            sala.Id = ObjectId.GenerateNewId().ToString();

            string idCinema = TempData["Cinema"].ToString();
            var cinema = dbContext.Cinemas.Find(n => n.Id == idCinema).FirstOrDefault();

            if (cinema.Salas == null)
            {
                cinema.Salas = new List<Sala>();
            }

            cinema.Salas.Add(sala);
        
            dbContext.Salas.InsertOne(sala);
            dbContext.Cinemas.ReplaceOne(m => m.Id == cinema.Id, cinema);

            return RedirectToAction("Index", "Sala");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Cinema> listaCinemas = dbContext.Cinemas.Find(new BsonDocument()).ToList();

            foreach(var item in listaCinemas)
            {
                var sala = item.Salas.Find(m => m.Id == id);
                if(sala != null)
                {
                    item.Salas.Remove(sala);
                    dbContext.Cinemas.ReplaceOne(m => m.Id == item.Id, item);
                }
            }

            List<Sessao> listaSessoes = dbContext.Sessoes.Find(new BsonDocument()).ToList();

            foreach (var item in listaSessoes)
            {
                if(item.Sala.Id == id)
                {
                    item.Sala = null;
                    dbContext.Sessoes.ReplaceOne(m => m.Id == item.Id, item);
                }                   
            }

            dbContext.Salas.DeleteOne(m => m.Id == id);
            return RedirectToAction("Index", "Sala");
        }
    }
}