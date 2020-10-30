using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTPReg4.Models
{
    public class Type
    {
        //ID типу правопорушення
        public int Id { get; set; }
        //Тип правопорушення
        public string Name { get; set; }
        //Тип покарання за скоєне ДТП
        public string Punishment { get; set; }
        //Перерахування реєстрацій порушників
        public IEnumerable<Registration> Registrations { get; set; }
    }
}