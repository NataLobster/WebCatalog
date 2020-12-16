using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCatalog.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
    }
}
