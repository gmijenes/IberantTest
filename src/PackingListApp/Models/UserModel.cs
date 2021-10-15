using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PackingListApp.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [MaxLength(10)]
        public string Address { get; set; }

        public bool IsAdmin { get; set; }

        public TypOfAdmin AdminType { get; set; }

    }

    public enum TypOfAdmin
    {
        Normal = 0,
        Vip = 1,
        King = 2
    }
}
