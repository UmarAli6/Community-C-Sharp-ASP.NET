using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Community.Areas.Identity.Data
{
    public class CommunityUser : IdentityUser
    {
        [NotMapped]
        public static DateTime LastLogin { get; set; }
        public virtual DateTime CurrentLogin { get; set; }

        public virtual long Days { get; set; }

        public long NoOfLogins { get; set; }

        public long NoOfReadMsg { get; set; }

        public long NoOfDeletedMsg { get; set; }
    }
}