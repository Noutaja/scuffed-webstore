using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Core.src.Entities
{
    public class EntityWithOwner : BaseEntity
    {
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}