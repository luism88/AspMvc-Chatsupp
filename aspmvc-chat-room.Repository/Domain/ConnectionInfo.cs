using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("ConnectionsInfo")]
    public class ConnectionInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ConnectionInfoId { get; set; }
        public string VisitorConnectionId { get; set; }
        public int VisitorId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public string AgentConnectionId { get; set; }
        public DateTime UserConnectionDate { get; set; }
        public Nullable<DateTime> AgentConnectionDate { get; set; }
        public Nullable<int> StateId { get; set; }

        [ForeignKey("AgentId")]
        public virtual Agent Agente { get; set; }
        [ForeignKey("VisitorId")]
        public virtual Visitor Visitor { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}
