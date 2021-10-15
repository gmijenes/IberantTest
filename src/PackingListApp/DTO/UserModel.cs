using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackingListApp.Models
{
    public class NewUserModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        
        public bool IsAdmin { get; set; } 

        public int AdminType { get; set; }
    }

    
}
