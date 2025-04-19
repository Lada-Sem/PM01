using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrapherAPI.Models
{
    public class Responsetest
    {
        public Responsetest(Тесты test, string метод) 
        { 
            Id = test.id;
            Name = test.вопрос;
            Metod = метод;
            Otvet = test.правильный_ответ;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Metod { get; set; }
        public string Otvet { get; set;}
    }
}