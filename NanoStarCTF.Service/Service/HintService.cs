using AutoMapper;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.HintsDto;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
using System.Linq.Expressions;
#pragma warning disable
namespace NanoStarCTF.Service.Service;

public class HintService : IHintsService
{
    private readonly IGenericRepository<Hints> repository;
    private readonly IGenericRepository<Challenge> CTFrepository;
    private readonly IMapper mapper;

    public HintService(IGenericRepository<Hints> repository, IGenericRepository<Challenge> CTFrepository, 
       IMapper mapper)
    {
        this.repository = repository;
        this.CTFrepository = CTFrepository;
        this.mapper = mapper;
    }

   public async ValueTask<HintsForViewDto> CreateAndAddAsync(HintsForCreationDto creationDto)
    {
        if (creationDto is null)
            throw new HttpStatusCodeException(404, "Not that the will be null");

        var hint = await repository.CreateAsync(mapper.Map<Hints>(creationDto));
        await repository.SaveChangesAsync();
        var ctf = await CTFrepository.GetAsync(ctf => ctf.Id == creationDto.CTFId);

        if (ctf is null)
            throw new HttpStatusCodeException(404, "Not that the will be null");

         ctf.Hints.Add(hint);

        CTFrepository.Update(ctf);
        await CTFrepository.SaveChangesAsync();
        return mapper.Map<HintsForViewDto>(hint);

    }




    public async ValueTask<HintsForViewDto> CreateAsync(HintsForViewDto entity)
    {
        if (entity.Discription is null)
            throw new HttpStatusCodeException(404, "Not that the will be null");


        var newHint = mapper.Map<Hints>(entity);
        newHint = await repository.CreateAsync(newHint);

        await repository.SaveChangesAsync();
        return mapper.Map<HintsForViewDto>(newHint);
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Hints, bool>> expression)
    {
        bool isDeleted = await repository.DeleteAsync(expression);


        if (!isDeleted)
            throw new HttpStatusCodeException(404, "Hints not found");

        await repository.SaveChangesAsync();
        return isDeleted;
    }

    public IEnumerable<HintsForViewDto> GetAll(Expression<Func<Hints, bool>> expression = null, string[] includes = null)
    {
        var address = repository.GetAll(expression: expression, includes: includes);

        return mapper.Map<List<HintsForViewDto>>(address);
    }

    public async ValueTask<HintsForViewDto> GetAsync(Expression<Func<Hints, bool>> expression, string[] includes = null)
    {
        var hint = await repository.GetAsync(expression);

        if (hint is null)
            throw new HttpStatusCodeException(404, "Hint not found");

        return mapper.Map<HintsForViewDto>(hint);
    }

    public async ValueTask<HintsForViewDto> Update(long id, HintsForViewDto entity)
    {
        var oldHint = await repository.GetAsync(p => p.Id == id);

        if (oldHint is null)
            throw new HttpStatusCodeException(404, "Hint not found");


        oldHint.UpdatedAt = DateTime.UtcNow;
        oldHint.UpdatedBy = oldHint.UpdatedBy is null ? 1 : oldHint.UpdatedBy++;
        oldHint = repository.Update(mapper.Map(entity, oldHint));
        await repository.SaveChangesAsync();

        await repository.SaveChangesAsync();

        return mapper.Map<HintsForViewDto>(oldHint);


    }
}
