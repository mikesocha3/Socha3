using System;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class Error
    {
        public long ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string ExceptionData { get; set; }
        public string Logger { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string InnerException { get; set; }
        public string ObjectData { get; set; }
        public DateTime Date { get; set; }
    }
}