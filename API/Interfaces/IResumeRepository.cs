using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IResumeRepository
    {

        Task<S3ResponseDTO>UploadFileAsync(ResumeDto file, AmazonOptions cred);
        void DeleteResume(Resume resume);
        Task<Resume> GetResume(int id);
        Task<bool> SaveAllAsync();
        
    }
   
}