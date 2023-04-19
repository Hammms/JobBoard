using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Extensions;
using API.Entities;
using Amazon.Runtime;
using Microsoft.Extensions.Options;
using API.Helpers;
namespace API.Controllers
{
    public class ResumeController : BaseAPiController
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;
        public readonly IOptions<AmazonOptions> _options; 
        private readonly IUserRepository _userRepository;
        public ResumeController(IResumeRepository resumeRepository, IMapper mapper, IUserRepository userRepository, IOptions<AmazonOptions> options)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _options = options;
        }

       [HttpPost("{resumeupload}")]

       public async Task<IActionResult> UploadFile(IFormFile file)
       {
            // process file 
            await using var MemoryStream = new MemoryStream();
            await file.CopyToAsync(MemoryStream);

            var fileExtension = Path.GetExtension(file.Name);
            var objName = $"{Guid.NewGuid()}.{fileExtension}";

            var resume = new ResumeDto() {
              BucketName = "com.deathcarejobs.dev.assets",
              ResumeContents = MemoryStream,
              Name = objName
            };

            var cred = new AmazonOptions
            {
                AccessKey = _options.Value.AccessKey,
                SecretAccessKey = _options.Value.SecretAccessKey
            };

            var result = await _resumeRepository.UploadFileAsync(resume, cred);
            return Ok(result);
       }
    }
}