using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model.Base;

namespace RestWithASPNETUdemy.Model 
{   
    [Table("books")]
    public class Book : BaseEntity
    { 
        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime LauchDate { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("title")]
        public string Title { get; set; }

    }
}
