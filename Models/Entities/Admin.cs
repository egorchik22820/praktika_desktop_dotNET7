using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika_desktop_dotNET7.Models.Entities
{
    public class AspNetUsers
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }
    }
}
