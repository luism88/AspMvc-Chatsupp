using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("Groups")]
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Agent> AdministratorAgents { get; set; }
        public virtual ICollection<ConnectionLog> Connections { get; set; }
    }
}
