using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Dto_s.Attachments
{
    public class AttachmentForCreateDto
    {
        public string Path { get; set; }
        public Stream Stream { get; set; }
    }
}
