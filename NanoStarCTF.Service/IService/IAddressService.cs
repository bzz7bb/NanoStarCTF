using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s.Address;
using System.Linq.Expressions;

namespace NanoStarCTF.Service.IService
{
    public interface IAddressService
    {
        IEnumerable<AddressForCreationDto> GetAll(Expression<Func<HomeAddress, bool>> expression = null, string[] includes = null);
        ValueTask<AddressForCreationDto> GetAsync(Expression<Func<HomeAddress, bool>> expression, string[] includes = null);
        ValueTask<AddressForCreationDto> CreateAsync(AddressForCreationDto entity);
        ValueTask<bool> DeleteAsync(Expression<Func<HomeAddress, bool>> expression);
        ValueTask<AddressForCreationDto> Update(long id, AddressForCreationDto entity);
    }
}
