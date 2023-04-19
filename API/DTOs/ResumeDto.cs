using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ResumeDto
    {
        public string Name {get; set;}
        public MemoryStream ResumeContents {get; set;} 
        public string BucketName { get; set; }
    }
}