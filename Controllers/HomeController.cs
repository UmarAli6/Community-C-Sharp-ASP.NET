using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Community.Data;
using Community.ViewModels;
using Community.Areas.Identity.Data;

namespace Community.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CommunityMessageContext _messagescontext;
        private readonly UserManager<CommunityUser> _userManager;

        public HomeController(CommunityMessageContext messagescontext, UserManager<CommunityUser> userManager)
        {
            _messagescontext = messagescontext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            UserViewModel userView = new UserViewModel(
                user.UserName,
                CommunityUser.LastLogin,
                user.NoOfLogins,
                user.NoOfReadMsg,
                user.NoOfDeletedMsg);

            List<CommunityMessage> currentUsersMessages = await _messagescontext.Messages.Where(message => message.ReceiverId == user.Id).ToListAsync();

            var NoOfUnreadMsgTemp = 0;

            foreach (var item in currentUsersMessages)
            {
                if (!item.IsRead)
                {
                    NoOfUnreadMsgTemp++;
                }
            }

            userView.NoOfUnreadMsg = NoOfUnreadMsgTemp;

            return View(userView);
        }
    }
}