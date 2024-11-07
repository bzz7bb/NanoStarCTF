using AutoMapper;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s.Address;
using NanoStarCTF.Service.Dto_s.Users;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
using System.Linq.Expressions;
#pragma warning disable
namespace NanoStarCTF.Service.Service;

public class AddressService : IAddressService
{
    private readonly IGenericRepository<HomeAddress> repository;
    private readonly IGenericRepository<User> userRepository;

    private readonly IMapper mapper;

    public AddressService(IGenericRepository<HomeAddress> repository, 
         IGenericRepository<User> userRepository, IMapper mapper)
    {
        this.repository = repository;
        this.userRepository = userRepository;
        this.mapper = mapper;
    }
    public async ValueTask<AddressForCreationDto> CreateAsync(AddressForCreationDto entity)
    {
        if (entity.Country is null || entity.Region is null || entity.District is null || entity.AddressLine is null)
            throw new HttpStatusCodeException(404, "Not that the will be null");
       
      
        var newAddress = mapper.Map<HomeAddress>(entity);
        newAddress = await repository.CreateAsync(newAddress);


        var user = await userRepository.GetAsync(user => user.Id == entity.UserId);
        if (user is null )
            throw new HttpStatusCodeException(404, "Not that the will be null");
        user.Address = newAddress;
        userRepository.Update(user);

        await repository.SaveChangesAsync();
        userRepository.SaveChangesAsync();

        return mapper.Map<AddressForCreationDto>(newAddress);
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<HomeAddress, bool>> expression)
    {
        bool isDeleted = await repository.DeleteAsync(expression);

        if (!isDeleted)
            throw new HttpStatusCodeException(404, "Address not found");

        await repository.SaveChangesAsync();
        return isDeleted;
    }

    public IEnumerable<AddressForCreationDto> GetAll(Expression<Func<HomeAddress, bool>> expression = null, string[] includes = null)
    {
            var address = repository.GetAll(expression: expression, includes: includes);

            return mapper.Map<List<AddressForCreationDto>>(address);
     }


    public async ValueTask<AddressForCreationDto> GetAsync(Expression<Func<HomeAddress, bool>> expression, string[] includes = null)
    {
        var address = await repository.GetAsync(expression);

        if(address is null)
            throw new HttpStatusCodeException(404, "Address not found");

        return mapper.Map<AddressForCreationDto>(address);
    }

    public async ValueTask<AddressForCreationDto> Update(long id, AddressForCreationDto entity)
    {
        var oldAddress = await repository.GetAsync(p => p.Id == id);

        if (oldAddress is null)
            throw new HttpStatusCodeException(404, "Address not found");


        oldAddress.UpdatedAt = DateTime.UtcNow;
        oldAddress.UpdatedBy = oldAddress.UpdatedBy is null ? 1 : oldAddress.UpdatedBy++;
        oldAddress = repository.Update(mapper.Map(entity, oldAddress));
        await repository.SaveChangesAsync();

        await repository.SaveChangesAsync();

        return mapper.Map<AddressForCreationDto>(oldAddress);
    }
}
