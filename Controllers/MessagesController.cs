using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Community.Data;
using Community.ViewModels;
using Community.Areas.Identity.Data;

namespace Community.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly CommunityUserContext _communityContext;
        private readonly CommunityMessageContext _messagescontext;
        private readonly UserManager<CommunityUser> _userManager;

        public MessagesController(CommunityUserContext communityContext, CommunityMessageContext messagescontext, UserManager<CommunityUser> userManager)
        {
            _communityContext = communityContext;
            _messagescontext = messagescontext;
            _userManager = userManager;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            List<String> sendersUser = new List<String>();

            List<CommunityMessage> currentUsersMessages = await _messagescontext.Messages.Where(Message => Message.ReceiverId == currentUser.Id).ToListAsync();


            var NoOfReadMsgTemp = 0;

            foreach (var item in currentUsersMessages)
            {
                if (item.IsRead)
                {
                    NoOfReadMsgTemp++;
                }
                var sender = await _userManager.FindByIdAsync(item.SenderId);
                sendersUser.Add(sender.UserName);
            }

            ReadMessageViewModel msgViewModel = new ReadMessageViewModel
            {
                NoOfReadMsg = NoOfReadMsgTemp,
                NoOfDeletedMsg = currentUser.NoOfDeletedMsg,
                NoOfTotMsg = currentUsersMessages.Count,
                senders = new SelectList(sendersUser.Distinct().ToList())
            };

            return View(msgViewModel);
        }

        // POST: Messages/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Choose([Bind("Sender")] ReadMessageViewModel viewMessage)
        {
            if (ModelState.IsValid)
            {
                var senderUser = await _communityContext.Users.FirstOrDefaultAsync(User => User.Email == viewMessage.Sender);

                List<CommunityMessage> currentUsersMessages = await _messagescontext.Messages.Where(Message => Message.SenderId == senderUser.Id).ToListAsync();

                List<ReadMessageViewModel> messagesList = new List<ReadMessageViewModel>();

                foreach (var item in currentUsersMessages)
                {
                    ReadMessageViewModel message = new ReadMessageViewModel
                    {
                        Content = item.Content,
                        Subject = item.Subject,
                        DateTime = item.DateTime,
                        Id = item.id,
                        Read = item.IsRead
                    };
                    messagesList.Add(message);
                }

                return View(messagesList);
            }
            return View();
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            CreateMessageViewModel msgViewModel = new CreateMessageViewModel();
            msgViewModel.receivers = new SelectList(_communityContext.Users, "UserName");

            return View(msgViewModel);
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject,Content,Receiver")] CreateMessageViewModel viewMessage)
        {
            if (ModelState.IsValid)
            {
                var receiver = await _userManager.FindByNameAsync(viewMessage.Receiver);
                var senderId = _userManager.GetUserId(User);

                CommunityMessage message = new CommunityMessage(viewMessage.Subject, viewMessage.Content, senderId, receiver.Id);

                _messagescontext.Add(message);
                await _messagescontext.SaveChangesAsync();

                ViewBag.Message = string.Format("Meddelande skickad till {0}, {1}", viewMessage.Receiver, DateTime.Now.ToString());

                viewMessage.receivers = new SelectList(_communityContext.Users, "UserName");

                return View(viewMessage);
            }
            return View();
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _messagescontext.Messages
                .FirstOrDefaultAsync(m => m.id == id);

            if (message == null)
            {
                return NotFound();
            }

            message.IsRead = true;

            _messagescontext.Update(message);
            await _messagescontext.SaveChangesAsync();

            ReadMessageViewModel viewMessage = new ReadMessageViewModel
            {
                Content = message.Content,
                Subject = message.Subject,
                DateTime = message.DateTime,
                Id = message.id,
                Read = message.IsRead
            };

            var currentUserId = _userManager.GetUserId(User);

            if (message.ReceiverId == currentUserId)
            {
                return View(viewMessage);
            }
            else
            {
                return RedirectToAction(nameof(Wronguser));
            }
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _messagescontext.Messages
                .FirstOrDefaultAsync(m => m.id == id);

            if (message == null)
            {
                return NotFound();
            }

            ReadMessageViewModel viewMessage = new ReadMessageViewModel
            {
                Content = message.Content,
                Subject = message.Subject,
                DateTime = message.DateTime
            };

            var currentUserId = _userManager.GetUserId(User);

            if (message.ReceiverId == currentUserId)
            {
                return View(viewMessage);
            }
            else
            {
                return RedirectToAction(nameof(Wronguser));
            }
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                currentUser.NoOfDeletedMsg++;

                _communityContext.Update(currentUser);
                await _communityContext.SaveChangesAsync();

                var message = await _messagescontext.Messages.FindAsync(id);

                _messagescontext.Messages.Remove(message);
                await _messagescontext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Messages/Wronguser
        public IActionResult Wronguser()
        {
            return View();
        }
    }
}