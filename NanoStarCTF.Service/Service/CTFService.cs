using System.Linq.Expressions;
using AutoMapper;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.CTFs;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Service.Security;
#pragma warning disable
namespace NanoStarCTF.Service.Service;

public class CTFService : ICTFService
{
    private readonly IGenericRepository<Challenge> repository;
    private readonly IGenericRepository<Group> groupRepository;
    private readonly IMapper mapper;

    public CTFService(IGenericRepository<Challenge> repository,
        IGenericRepository<Group> groupRepository, IMapper mapper)
    {
        this.repository = repository;
        this.groupRepository = groupRepository;
        this.mapper = mapper;
    }

    public IEnumerable<CTFForViewDto> GetAll(Expression<Func<Challenge, bool>> expression = null, string[] includes = null)
    {  
      return mapper.Map<List<CTFForViewDto>>(repository.GetAll(expression,  ["Hints", "Image", "Group"])); 
    }


    public async ValueTask<CTFForViewDto> GetAsync(Expression<Func<Challenge, bool>> expression, string[] includes = null)
    {
        var ctf = await repository.GetAsync(expression, ["Hints", "Image","Group"]);

        if (ctf is null)
            throw new HttpStatusCodeException(404, "CTF not found");

        return mapper.Map<CTFForViewDto>(ctf);
    }

    public async ValueTask<CTFForViewDto> CreateAsync(CTFForCreationDto entity)
    {
        if (entity.Description is null )
            throw new HttpStatusCodeException(404, "Not that the will be null");


        var newCTF = mapper.Map<Challenge>(entity);
        newCTF.FlagChek = SecurePasswordHasher.Hash(newCTF.FlagChek);

        newCTF = await repository.CreateAsync(newCTF);

        await repository.SaveChangesAsync();






        return mapper.Map<CTFForViewDto>(newCTF);
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Challenge, bool>> expression)
    {
        bool isDeleted = await repository.DeleteAsync(expression);

        if (!isDeleted)
            throw new HttpStatusCodeException(404, "CTF not found");

        await repository.SaveChangesAsync();
        return isDeleted;
    }

    public async ValueTask<CTFForViewDto> Update(long id, CTFForCreationDto entity)
    {
        var oldCTF= await repository.GetAsync(p => p.Id == id);

        if (oldCTF is null)
            throw new HttpStatusCodeException(404, "CTF not found");


        oldCTF.UpdatedAt = DateTime.UtcNow;
        oldCTF.UpdatedBy = oldCTF.UpdatedBy is null ? 1 : oldCTF.UpdatedBy++;
        oldCTF = repository.Update(mapper.Map(entity, oldCTF));
        await repository.SaveChangesAsync();

        await repository.SaveChangesAsync();

        return mapper.Map<CTFForViewDto>(oldCTF);
    }
}