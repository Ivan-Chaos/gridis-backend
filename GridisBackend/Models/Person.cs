using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class Person : EntityBase
    {
        public Person()
        {
            CallOperators = new HashSet<CallOperator>();
            Engineers = new HashSet<Engineer>();
            Residences = new HashSet<Residence>();
        }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<CallOperator> CallOperators { get; set; }
        public virtual ICollection<Engineer> Engineers { get; set; }
        public virtual ICollection<Residence> Residences { get; set; }
    }
}
