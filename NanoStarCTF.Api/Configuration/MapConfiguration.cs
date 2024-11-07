using AutoMapper;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Address;
using NanoStarCTF.Service.Dto_s.Attachments;
using NanoStarCTF.Service.Dto_s.CTFs;
using NanoStarCTF.Service.Dto_s.Groups;
using NanoStarCTF.Service.Dto_s.HintsDto;
using NanoStarCTF.Service.Dto_s.Users;

namespace NanoStarCTF.Api.Configuration;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        //User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForViewDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        //Group for CTF
        CreateMap<Group, GroupForCreationDto>().ReverseMap();
        CreateMap<Group, GroupForViewDto>().ReverseMap();

        //CTF model
        CreateMap<Challenge, CTFForCreationDto>().ReverseMap();
        CreateMap<Challenge, CTFForViewDto>().ReverseMap();

        //Hints
        CreateMap<Hints, HintsForViewDto>().ReverseMap();
        CreateMap<Hints, HintsForCreationDto>().ReverseMap();

        //Attachment
        CreateMap<Attachment, AttachmentForCreateDto>().ReverseMap();

        //Address
        CreateMap<HomeAddress, AddressForCreationDto>().ReverseMap();
    }
}