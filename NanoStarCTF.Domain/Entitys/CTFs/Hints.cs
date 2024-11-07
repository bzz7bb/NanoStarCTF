using NanoStarCTF.Domain.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Domain.Entitys.CTFs
{
    public class Hints : Auditble
    {
        public string Discription { get; set; }
        public string? Links { get; set; }
    }
}
