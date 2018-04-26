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
    public class ConnectionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ConnectionInfoId { get; set; }
        public int VisitorId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public DateTime UserConnectionDate { get; set; }
        public string OperatingSystem { get; set; }
        public string Location { get; set; }
       
        [ForeignKey("VisitorId")]
        public virtual Visitor Visitor { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        
    }
}
