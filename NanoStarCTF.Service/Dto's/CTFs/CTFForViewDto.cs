using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Dto_s.CTFs
{
    public class CTFForViewDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Hints>? Hints { get; set; }
        public Attachment? Image { get; set; }
        public virtual Group Group { get; set; }
        public string Link { get; set; }
        public CTFForViewDto()
        {
            Hints = new List<NanoStarCTF.Domain.Entitys.CTFs.Hints>();
        }
    }
}
