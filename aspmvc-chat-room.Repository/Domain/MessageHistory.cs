using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("MessageHistory")]
    public class MessageHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MessageHistoryId { get; set; }
        public int VisitorId { get; set; }
        public int AgentId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsAgentMessage { get; set; }

        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        [ForeignKey("VisitorId")]
        public virtual Visitor Visitor { get; set; }
    }
}
