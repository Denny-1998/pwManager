using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwManager.Models
{
    public class DecryptedAccount
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
