﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentHunter.Web.Models;
using ContentHunter.Web.Models.Engines;

namespace ContentHunter.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            /*string url = "http://leitoracompulsiva.wordpress.com/";
            LeitoraCompulsiva lc = new LeitoraCompulsiva();
            string content = lc.ParseHtml(url);

            ViewBag.Message = content;
            return View();*/

            Instruction input = new Instruction()
            {
                Id = 1,
                Url = "http://leitoracompulsiva.wordpress.com/",
                IsRecursive = false,
                Engine = "LeitoraCompulsiva",
                Type = Instruction.GetType(Instruction.InputType.Html),
                IsRecurrent = true,
                Category = "Literatura"
            };

            /*ContentHunterDB db = new ContentHunterDB();
            db.Instructions.Add(input);
            db.SaveChanges();*/

            ContentHunterDB db = new ContentHunterDB();
            string[] ids = { "1" };
            var aux = db.Instructions.FindByIds<Instruction>(ids);
            //var aux = db.Instructions.SqlQuery("Select Id", new { });
                

            CrawlerResult output = input.Execute();
            
            ViewBag.Message = output.Content;
            ViewBag.Engines = Crawler.GetEngines();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
