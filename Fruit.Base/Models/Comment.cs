using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class Comment
    {
        public int Idcomment { get; set; }
        public int Idcommentator { get; set; }
        public string? CommentContent { get; set; }
        public int Idblog { get; set; }
        public DateTime? Date { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public string? UseName { get; set; }
        [NotMapped]
        public string? Title { get; set; }   
    }
}
