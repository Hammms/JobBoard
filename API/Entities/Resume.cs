using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Resume
    {
        public int Id { get; set; }
        public byte[] ResumeContents{ get; set; }
        public int AppUserId { get; set; }

    }
}