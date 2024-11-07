using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Dto_s.Groups
{
    public class GroupForViewDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public Attachment? Image { get; set; }
        public virtual List<Challenge>? CTFsList { get; set; }
        public GroupForViewDto()
        {
            CTFsList = new List<Challenge>();
        }
    }
}
