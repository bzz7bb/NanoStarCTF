using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NanoStarCTF.Domain.Enums;


namespace NanoStarCTF.Domain.Domain.Commons
{
    public class Auditble{  
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public ItemState State { get; set; } = ItemState.Created;
    }
}