using System;
using System.Collections.Generic;

namespace GridisBackend.Models
{
    public partial class District : EntityBase
    {
        public District()
        {
            Engineers = new HashSet<Engineer>();
            Streets = new HashSet<Street>();
        }

        public string Name { get; set; } = null!;
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Engineer> Engineers { get; set; }
        public virtual ICollection<Street> Streets { get; set; }
    }
}
