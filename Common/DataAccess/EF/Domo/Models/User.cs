using System;
using System.Collections.Generic;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class User
    {
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }        
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Meme> Memes { get; set; }
    }
}