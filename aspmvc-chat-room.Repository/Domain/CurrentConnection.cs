using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("CurrentConnections")]
    public class CurrentConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CurrenConnectionId { get; set; }
        public string ConnectionId { get; set; }
        public Nullable<int> VisitorId { get; set; }
        public Nullable<int> AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agents { get; set; }
        [ForeignKey("VisitorId")]
        public virtual Visitor Visitors { get; set; }
    }
}
