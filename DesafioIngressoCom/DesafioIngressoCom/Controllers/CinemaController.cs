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
    public class CinemaController : Controller
    {
        public IActionResult Index()
        {
            MongoDbContext dbContext = new MongoDbContext();
            List<Cinema> listaCinemas = dbContext.Cinemas.Find(m => true).ToList();
            return View(listaCinemas);
        }

        public IActionResult Details(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            if(id == null)
            {
                id = Request.Form["cinemaSelect"];
            }
            var entity = dbContext.Cinemas.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Cinemas.Find(n => n.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Cinemas.Find(n => n.Id == cinema.Id).FirstOrDefault();
            cinema.Salas = entity.Salas;
            dbContext.Cinemas.ReplaceOne(m => m.Id == cinema.Id, cinema);
            return RedirectToAction("Index", "Cinema");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Cinema cinema)
        {
            MongoDbContext dbContext = new MongoDbContext();
            cinema.Id = ObjectId.GenerateNewId().ToString();
            cinema.Salas = new List<Sala>();
            dbContext.Cinemas.InsertOne(cinema);
            return RedirectToAction("Index", "Cinema");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            MongoDbContext dbContext = new MongoDbContext();
            var entity = dbContext.Cinemas.Find(n => n.Id == id).FirstOrDefault();
            var sessoes = dbContext.Sessoes.Find(new BsonDocument()).ToList();
            bool alterou = false;
            foreach(var item in entity.Salas)
            {
                foreach(var sessao in sessoes)
                {
                    if(sessao.Sala != null && sessao.Sala.Id == item.Id)
                    {
                        sessao.Sala = null;
                        alterou = true;
                    }

                    if (alterou)
                    {
                        dbContext.Sessoes.ReplaceOne(m => m.Id == sessao.Id, sessao);
                        alterou = false;
                    }
                }
                dbContext.Salas.DeleteOne(m => m.Id == item.Id);
            }
            dbContext.Cinemas.DeleteOne(m => m.Id == id);
            return RedirectToAction("Index", "Cinema");
        }
    }
}