using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class Blog
    {
        public int Idblog { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? ShortContent { get; set; }
        public string? Contents { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
    }
}
