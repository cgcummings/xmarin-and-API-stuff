using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneApp.Models
{
    public class Recipes
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public string Instruction { get; set; }

        // what user it belongs to 

        public string UserId { get; set; }
    }
}