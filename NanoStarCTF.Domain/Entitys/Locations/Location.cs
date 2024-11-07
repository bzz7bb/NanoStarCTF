using NanoStarCTF.Domain.Domain.Commons;
using NanoStarCTF.Domain.Enums;

namespace NanoStarCTF.Domain.Entitys.Locations;

public class Location : Auditble
{
        public Region Region { get; set; }
        public string? District { get; set; }
        public string?  GPSCode { get; set; }
}