using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socha3.MemeBox2000.Models
{
    public class MemeThumbnail
    {
        public byte[] Bytes { get; set; }
        public int LargestSide { get; set; }
    }
}