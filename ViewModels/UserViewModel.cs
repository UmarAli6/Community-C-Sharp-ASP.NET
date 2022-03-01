using System;
using System.ComponentModel;

namespace Community.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Last Login")]
        public DateTime ViewLastLogin { get; set; }

        [DisplayName("Logins this month")]
        public long NoOfLogins { get; set; }

        [DisplayName("Read messages")]
        public long NoOfReadMsg { get; set; }

        [DisplayName("Unread messages")]
        public long NoOfUnreadMsg { get; set; }

        [DisplayName("Deleted Messages")]
        public long NoOfDeletedMsg { get; set; }

        public UserViewModel(string username, DateTime viewLastLogin, long noOfLogins, long noOfReadMsg, long noOfDeletedMsg)
        {
            this.Username = username;
            this.ViewLastLogin = viewLastLogin;
            this.NoOfLogins = noOfLogins;
            this.NoOfReadMsg = noOfReadMsg;
            this.NoOfUnreadMsg = 0;
            this.NoOfDeletedMsg = noOfDeletedMsg;
        }
    }
}