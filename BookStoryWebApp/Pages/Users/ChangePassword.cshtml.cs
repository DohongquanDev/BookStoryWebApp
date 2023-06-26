using BookStoryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BookStoryWebApp.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        private readonly StoryDBContext _context;
        public ChangePasswordModel(StoryDBContext context)
        {
            _context = context; 
        }
        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string opassword, string npassword, string cpassword)
        {
            User u = null;
            string json = HttpContext.Session.GetString("user");
            if(json != null) u = JsonConvert.DeserializeObject<User>(json);
            string message = "";
            var entity = _context.Users.FirstOrDefault(s => s.Uid == u.Uid);
            if(!entity.Password.Equals(opassword))
            {
                message = "Mat khau cu khong dung. Vui long nhap lai";

            }
            else if(entity.Password.Equals(npassword))
            {
                message = "Mat khau moi giong voi mat khau cu. Vui long nhap lai";
            }else if (!npassword.Equals(cpassword))
            {
                message = "Mat khau nhap lai khong khop voi mat khau cu";
            }
            else
            {
                entity.Password = npassword;
                _context.Entry(entity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                message = "Doi mat khau thanh cong";
            }
            Message = message;
            return Page();
        }
    }
}
