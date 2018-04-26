using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("ChatHistory")]
    public class ChatHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MessageHistoryId { get; set; }
        public int VisitorId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public EnumEventType EventTypeId { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
       
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        [ForeignKey("VisitorId")]
        public virtual Visitor Visitor { get; set; }
        [ForeignKey("EventTypeId")]
        public virtual EventType EventType { get; set; }
    }
}
