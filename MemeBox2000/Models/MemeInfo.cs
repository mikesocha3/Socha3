using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Socha3.Common.Tools;

namespace Socha3.MemeBox2000.Models
{
    public class MemeInfo
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Genre { get; set; }

        public string UserEmail { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
        public long? UserId { get; set; }

        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public List<MemeThumbnail> Thumbnails { get; set; } = new List<MemeThumbnail>();

        public MemeThumbnail GetThumbnail(int largestSide)
        {
            var thumb = Thumbnails.FirstOrDefault(mt => mt.LargestSide == largestSide);
            if (thumb == null)
            {
                thumb = new MemeThumbnail
                {
                    Bytes = ImageUtil.CreateThumbnail(Image, largestSide),
                    LargestSide = largestSide
                };
                Thumbnails.Add(thumb);
            }
            return thumb;
        }
    }
}
