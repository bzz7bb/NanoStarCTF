using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Dto_s.HintsDto
{
    public class HintsForCreationDto
    {
        public string Discription { get; set; }
        public string? Links { get; set; }
        public long CTFId { get; set; }
    }
}
