using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Amazon.S3;
using Amazon.Runtime;
using Microsoft.Extensions.Options;
using Amazon.S3.Transfer;

namespace API.Data
{
    public class ResumeRepository : IResumeRepository
    {
        public DataContext _context { get; }
        private readonly IMapper _mapper;
        public readonly IOptions<AmazonOptions> _options;

        public ResumeRepository(DataContext context, IMapper mapper,IOptions<AmazonOptions> options)
        {
            _options = options;
            _mapper = mapper;
            _context = context;
        }

        async Task<S3ResponseDTO>IResumeRepository.UploadFileAsync(ResumeDto file, AmazonOptions cred)
        {   //Add AWS Credentials
            var credentials = new BasicAWSCredentials(_options.Value.AccessKey, _options.Value.SecretAccessKey);
            // Specify The Region
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            var response = new S3ResponseDTO();
            //Create a new upload request
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = file.ResumeContents,
                    Key = file.Name,
                    BucketName = file.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // Created an S3 client
                using var client = new AmazonS3Client(credentials, config);

                //upload utility to s3
                var transferUtility = new TransferUtility(client);
                // Calling to upload file to S3
                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 200;
                response.Message = $"{file.Name} has been uploaded successfully";
               
            }
            catch(AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }

        public void DeleteResume(Resume resume)
        {
            _context.Resume.Remove(resume);
        }

        public async Task<Resume> GetResume(int id)
        {
            return await _context.Resume.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

       
    }
}