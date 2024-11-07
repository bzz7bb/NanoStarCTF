using AutoMapper;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Domain.Enums;
using NanoStarCTF.Service.Dto_s.Users;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.Extensions;
using NanoStarCTF.Service.Helpers;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Service.Security;
using System.Linq.Expressions;
#pragma warning disable
namespace NanoStarCTF.Service.Service;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> repository;
    private readonly IMapper mapper;

    public UserService(IGenericRepository<User> repository,
        IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<UserForViewDto> CreateAsync(UserForCreationDto entity)
    {

        if (entity.Password != entity.ConfirmPassword)
            throw new HttpStatusCodeException(404, "Passwords are different");

        // check for exist
        var existUser = await repository.GetAsync(p => p.Email == entity.Email);
        if (existUser != null)
            throw new HttpStatusCodeException(400, "User is already exist");

        var createdUser = await repository.CreateAsync(mapper.Map<User>(entity));

        createdUser.Password = SecurePasswordHasher.Hash(createdUser.Password);

        await repository.SaveChangesAsync();

        return mapper.Map<UserForViewDto>(createdUser);
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        var isDeleted = await repository.DeleteAsync(expression);

        if (!isDeleted)
            throw new HttpStatusCodeException(404, "User not found");

        await repository.SaveChangesAsync();
        return isDeleted;
    }

    public  IEnumerable<UserForViewDto> GetAll(Expression<Func<User, bool>> expression = null, string[] includes = null)
    {
        var users = repository.GetAll(expression: expression,includes: ["Image"]);

        return  mapper.Map<List<UserForViewDto>>(users);
    }


    public async ValueTask<UserForViewDto> GetAsync(Expression<Func<User, bool>> expression, string[] includes = null)
    {
        var user = await repository.GetAsync(expression, ["Image"]);

        if (user is null)
            throw new HttpStatusCodeException(404, "User not found");

        return mapper.Map<UserForViewDto>(user);
    }

    public async ValueTask<UserForViewDto> Update(long id, UserForUpdateDto entity)
    {
        var existUser = await repository.GetAsync(
           u => u.Id == id);

        if (existUser == null)
            throw new HttpStatusCodeException(404, "User not found");

        var alreadyExistUser = await repository.GetAsync(
            u => u.Email == entity.Email && u.Id != HttpContextHelper.UserId);

        if (alreadyExistUser != null)
            throw new HttpStatusCodeException(400, "User with such email already exists");


        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.UpdatedBy = existUser.UpdatedBy is null ? 1 : existUser.UpdatedBy++;
        existUser = repository.Update(mapper.Map(entity, existUser));
        await repository.SaveChangesAsync();

        return mapper.Map<UserForViewDto>(existUser);
    }

}
