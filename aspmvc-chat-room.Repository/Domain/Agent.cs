﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Domain
{
    [Table("Agents")]
    public class Agent
    {
        public Agent()
        {
            this.CurrentConnections = new List<CurrentConnection>();
            this.MessageHistory = new List<ChatHistory>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AgentId { get; set; }
        public string Name { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Group Room { get; set; }
        public virtual ICollection<CurrentConnection> CurrentConnections { get; set; }
        public virtual ICollection<ChatHistory> MessageHistory { get; set; }
    }
}
