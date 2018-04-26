using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("HistoryTypes")]
    public class HistoryType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int HistoryTypeId { get; set; }
        public string Name { get; set; }
        public string LegendTemplate { get; set; }
    }
}
