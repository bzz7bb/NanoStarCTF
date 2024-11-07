using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.HintsDto;
using System.Linq.Expressions;

namespace NanoStarCTF.Service.IService
{
    public interface IHintsService
    {
        IEnumerable<HintsForViewDto> GetAll(Expression<Func<Hints, bool>> expression = null, string[] includes = null);
        ValueTask<HintsForViewDto> GetAsync(Expression<Func<Hints, bool>> expression, string[] includes = null);
        ValueTask<HintsForViewDto> CreateAsync(HintsForViewDto entity);
        ValueTask<bool> DeleteAsync(Expression<Func<Hints, bool>> expression);
        ValueTask<HintsForViewDto> Update(long id, HintsForViewDto entity);

        ValueTask<HintsForViewDto> CreateAndAddAsync(HintsForCreationDto creationDto);
    }
}
