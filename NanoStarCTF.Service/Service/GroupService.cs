using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Address;
using NanoStarCTF.Service.Dto_s.Groups;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Service.Service;

public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group> repository;
    private readonly IMapper mapper;

    public GroupService(IGenericRepository<Group> repository,IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public IEnumerable<GroupForViewDto> GetAll(Expression<Func<Group, bool>> expression = null, string[] includes = null)
    {  
     return mapper.Map<List<GroupForViewDto>>(repository.GetAll(expression, ["Image", "CTFsList"])); 
    }
    

    public async ValueTask<GroupForViewDto> GetAsync(Expression<Func<Group, bool>> expression, string[] includes = null)
    {
        var group = await repository.GetAsync(expression, ["Image", "CTFsList"]);

        if (group is null)
            throw new HttpStatusCodeException(404, "Group not found");

        return  mapper.Map<GroupForViewDto>(group);
    }

    public async ValueTask<GroupForViewDto> CreateAsync(GroupForCreationDto entity)
    {

        if (entity.Name is null )
            throw new HttpStatusCodeException(404, "Not that the will be null");


        var newGroup = mapper.Map<Group>(entity);
        newGroup = await repository.CreateAsync(newGroup);

        await repository.SaveChangesAsync();
        return mapper.Map<GroupForViewDto>(newGroup);
       
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Group, bool>> expression)
    {
        bool isDeleted = await repository.DeleteAsync(expression);

        if (!isDeleted)
            throw new HttpStatusCodeException(404, "Group not found");

        await repository.SaveChangesAsync();
        return isDeleted;
    }

    public async ValueTask<GroupForViewDto> Update(long id, GroupForCreationDto entity)
    {
        var oldGroup= await repository.GetAsync(p => p.Id == id);

        if (oldGroup is null)
            throw new HttpStatusCodeException(404, "Address not found");


        oldGroup.UpdatedAt = DateTime.UtcNow;
        oldGroup.UpdatedBy = oldGroup.UpdatedBy is null ? 1 : oldGroup.UpdatedBy++;
        oldGroup = repository.Update(mapper.Map(entity, oldGroup));
        await repository.SaveChangesAsync();

        await repository.SaveChangesAsync();

        return mapper.Map<GroupForViewDto>(oldGroup);
    }
}