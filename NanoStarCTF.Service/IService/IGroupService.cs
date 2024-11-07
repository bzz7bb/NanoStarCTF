using System.Linq.Expressions;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Groups;
#pragma warning disable
namespace NanoStarCTF.Service.IService;

public interface IGroupService
{
    IEnumerable<GroupForViewDto> GetAll(Expression<Func<Group, bool>> expression = null, string[] includes = null);
    ValueTask<GroupForViewDto> GetAsync(Expression<Func<Group, bool>> expression, string[] includes = null);
    ValueTask<GroupForViewDto> CreateAsync(GroupForCreationDto entity);
    ValueTask<bool> DeleteAsync(Expression<Func<Group, bool>> expression);
    ValueTask<GroupForViewDto> Update(long id, GroupForCreationDto entity);
}