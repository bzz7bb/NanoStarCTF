using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.CTFs;
using System.Linq.Expressions;
#pragma warning disable
namespace NanoStarCTF.Service.IService;

public interface ICTFService
{
    IEnumerable<CTFForViewDto> GetAll(Expression<Func<Challenge, bool>> expression = null, string[] includes = null);
    ValueTask<CTFForViewDto> GetAsync(Expression<Func<Challenge, bool>> expression, string[] includes = null);
    ValueTask<CTFForViewDto> CreateAsync(CTFForCreationDto entity);
    ValueTask<bool> DeleteAsync(Expression<Func<Challenge, bool>> expression);
    ValueTask<CTFForViewDto> Update(long id, CTFForCreationDto entity);
}
