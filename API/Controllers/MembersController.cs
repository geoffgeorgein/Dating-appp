using dating_app.Data;
using dating_app.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dating_app.Controllers
{
 
    public class MembersController(AppDbContext context) : BaseApiConroller
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members =context.Users.ToList();


            return members;
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetMember(string id)
        {
            var member =context.Users.Find(id);

            if(member == null)  return NotFound();
            return member;
        }
    }
}
