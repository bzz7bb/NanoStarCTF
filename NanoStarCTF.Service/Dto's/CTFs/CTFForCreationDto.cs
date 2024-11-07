using NanoStarCTF.Domain.Entitys.CTFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable
namespace NanoStarCTF.Service.Dto_s;

public class CTFForCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public string FlagChek { get; set; }
    public long GroupId { get; set; }
}
