using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PazumAniaWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace PazumAniaWebService.Controllers
{
    public class HelpController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // POST: /Help/ForgotPassword
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null /*|| !(await UserManager.IsEmailConfirmedAsync(user.Id))*/)
                {
                    // Nie ujawniaj informacji o tym, że użytkownik nie istnieje lub nie został potwierdzony

                }

                // Aby uzyskać więcej informacji o sposobie włączania potwierdzania konta i resetowaniu hasła, odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=320771
                // Wyślij wiadomość e-mail z tym łączem
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "AccountMVC", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Resetuj hasło", "Resetuj hasło, klikając <a href=\"" + callbackUrl + "\">tutaj</a>");
            }
            return null;
        }
    }
}