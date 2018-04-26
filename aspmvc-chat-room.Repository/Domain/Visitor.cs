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
            this.MessageHistory = new List<ChatHistory>();
            this.ConnectionLogs = new List<ConnectionLog>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public EnumState StateId { get; set; }
        public Nullable<int> AssignedAgentId { get; set; }

        [ForeignKey("AssignedAgentId")]
        public virtual Agent AssignedAgent { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        public virtual ICollection<CurrentConnection> CurrentConnections { get; set; }
        public virtual ICollection<ChatHistory> MessageHistory { get; set; }
        public virtual ICollection<ConnectionLog> ConnectionLogs { get; set; }

    }
}
