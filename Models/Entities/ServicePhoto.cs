using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika_desktop_dotNET7.Models.Entities
{
    public class ServicePhoto
    {
        public int Id { get; set; }

        public string? FileName { get; set; }

        public byte[] Data { get; set; } = null!;
    }
}
