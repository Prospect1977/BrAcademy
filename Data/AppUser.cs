using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class AppUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
        
    }
}
