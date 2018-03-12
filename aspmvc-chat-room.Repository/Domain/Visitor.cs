using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("Visitors")]
    public class Visitor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VistorId { get; set; }
        public string Name { get; set; }
        public string OperatingSystem { get; set; }
        public string Location { get; set; }
   
    }
}
