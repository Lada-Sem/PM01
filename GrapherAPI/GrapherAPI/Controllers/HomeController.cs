using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GrapherAPI.Controllers
{
    public class HomeController : Controller
    {
        private Численные_методыEntities db = new Численные_методыEntities(); 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CharterHelp()
        {
            // Получение количества методов и тестов из базы данных
            int methodsCount = db.Методы.Count();
            int testsCount = db.Тесты.Count();
            int primerCount = db.Примеры.Count();
            int grapherCount = db.Графики.Count();
            int otveyCount = db.Ответы_на_тесты.Count();

            // Создание диаграммы
            new Chart(width: 500, height: 300)
                .AddTitle("Количество данных в базе данных")
                .AddSeries(chartType: "column",
                    xValue: new[] { "Методы", "Тесты", "Примеры", "Графики", "Ответы"},
                    yValues: new[] { methodsCount.ToString(), testsCount.ToString(), primerCount.ToString(), grapherCount.ToString(), otveyCount.ToString() })
                .Write("bmp");

            return null;
        }
    }
}