using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community.Data
{
    public class CommunityMessage
    {
        [Key]
        public int id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        [ForeignKey("Id")]
        public string SenderId { get; set; }

        [Required]
        [ForeignKey("Id")]
        public string ReceiverId { get; set; }

        public CommunityMessage(string subject, string content, string senderId, string receiverId)
        {
            this.Subject = subject;
            this.Content = content;
            this.DateTime = DateTime.Now;
            this.IsRead = false;
            this.SenderId = senderId;
            this.ReceiverId = receiverId;
        }
    }
}