using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace etity
{
    [Table("myproduct")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int congtyid { get; set; }

        public void hienthi() => Console.WriteLine($"{Id}  - {Name} - {Price} - {congtyid}");
    }
}
