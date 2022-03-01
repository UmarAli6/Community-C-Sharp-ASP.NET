using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Community.ViewModels
{
    public class ReadMessageViewModel
    {
        [DisplayName("Sender")]
        public string Sender { get; set; }

        public int Id { get; set; }

        public SelectList senders;
        public string Subject { get; set; }
        public string Content { get; set; }
        [DisplayName("Date")]
        public DateTime DateTime { get; set; }

        [DisplayName("Total messages")]
        public long NoOfTotMsg { get; set; }

        [DisplayName("Read messages")]
        public long NoOfReadMsg { get; set; }
        [DisplayName("Deleted Messages")]
        public long NoOfDeletedMsg { get; set; }

        public bool Read { get; set; }
    }
}
