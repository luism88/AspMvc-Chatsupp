using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    public enum EnumState
    {
        Connected,
        Disconnected,
        WaitingAnswer
    }
    [Table("States")]
    public class State
    {
        [Key]
        public EnumState StateId { get; set; }
        public string Name { get; set; }
    }
}
