using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Models
{
    public partial class Contacts
    {
        public int ContactId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public int IsRead { get; set; } 
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public string? RepMessage { get; set; }  
    }
}
