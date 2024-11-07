using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.IService
{
    public interface IUserService
    {
        IEnumerable<UserForViewDto> GetAll(Expression<Func<User, bool>> expression = null, string[] includes = null);
        ValueTask<UserForViewDto> GetAsync(Expression<Func<User, bool>> expression, string[] includes = null);
        ValueTask<UserForViewDto> CreateAsync(UserForCreationDto entity);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<UserForViewDto> Update(long id, UserForUpdateDto entity);
    }
}
