using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Admin : EntityBase
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }
    }
}
