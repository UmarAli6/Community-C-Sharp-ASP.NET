using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Community.ViewModels
{
    public class CreateMessageViewModel
    {
        public string Subject { get; set; }
        public string Content { get; set; }

        [DisplayName("Receiver")]
        public string Receiver { get; set; }

        public SelectList receivers;
    }
}