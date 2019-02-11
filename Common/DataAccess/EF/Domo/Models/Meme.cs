using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class Meme
    {
        public long MemeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string MimeType { get; set; }

        public byte[] File { get; set; }
        public virtual User User { get; set; }
        public long? UserId { get; set; }
    }
}
