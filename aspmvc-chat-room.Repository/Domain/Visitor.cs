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
        public Visitor()
        {
            this.CurrentConnections = new List<CurrentConnection>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public string OperatingSystem { get; set; }
        public string Location { get; set; }

        public virtual ICollection<CurrentConnection> CurrentConnections { get; set; }
        public virtual ICollection<MessageHistory> MessageHistory { get; set; }

    }
}
