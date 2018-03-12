using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("RoomEvents")]
    public class RoomEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoomEventId { get; set; }
        public int RoomId { get; set; }
        public int EventTypeId { get; set; }
        public int Date { get; set; }
        public string Description { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room {get; set;}


    }
}
