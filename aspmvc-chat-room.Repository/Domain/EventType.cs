using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    public enum EnumEventType
    {
        AgentConnected,
        AgentDisconnected,
        AgentMessage,
        VisitorConnected,
        VisitorDisconected,
        VisitorMessage
    }
    [Table("EventsType")]
    public class EventType
    {
        [Key]
        public EnumEventType EventTypeId { get; set; }
        public string Name { get; set; }
        public string LegendTemplate { get; set; }
    }
}
