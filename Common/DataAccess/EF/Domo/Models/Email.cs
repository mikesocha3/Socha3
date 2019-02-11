using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class Email
    {
        public long EmailId { get; set; }

        public string To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public DateTime? SentDate { get; set; }

        public int NumberAttempts { get; set; }
    }
}
