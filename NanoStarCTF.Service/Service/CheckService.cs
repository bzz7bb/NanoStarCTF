using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Submissions;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.Helpers;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Service.Security;

namespace NanoStarCTF.Service.Service
{
    public class CheckService : ICheckerService
    {
        private readonly IGenericRepository<Challenge> repository;
        private readonly IGenericRepository<Submission> subRepository;
        private readonly IGenericRepository<User> userRepository;
        public CheckService(IGenericRepository<Challenge> repository,IGenericRepository<User> userRepository,IGenericRepository<Submission> subRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.subRepository = subRepository;
        }
        public async ValueTask<bool> Check(Checker checkDto)
        {
           if(checkDto.CheckFlag is null && checkDto.CTFId == null)
              throw new HttpStatusCodeException(404, "Flag not found");

            var ctF = await repository.GetAsync(ctf => ctf.Id == checkDto.CTFId);
            var user = await userRepository.GetAsync(user => user.Id == HttpContextHelper.UserId);
            Submission sub = new Submission()
            {
                ChallengeId = checkDto.CTFId,
                UserId = user.Id,
                SubmittedFlag = checkDto.CheckFlag, 
                IsCorrect = ctF != null
            };

            await subRepository.CreateAsync(sub);
            await subRepository.SaveChangesAsync();
            
            if (ctF is null)
                throw new HttpStatusCodeException(404, "CTF not found");

            if (ctF.FlagChek == SecurePasswordHasher.Hash(checkDto.CheckFlag))
                return true;
            return true;
        }
    }
}
