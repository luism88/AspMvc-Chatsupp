using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("Rooms")]
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Agent> AdministratorAgents { get; set; }
        public virtual ICollection<RoomEvent> Events { get; set; }
        public virtual ICollection<ConnectionInfo> Connections { get; set; }
    }
}
