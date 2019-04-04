using AutoMapper;
using Toptin.Api.Data.Dtos;
using Toptin.Api.Models;

namespace Toptin.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
         public AutoMapperProfiles()
        {
            //<Source, Destination>
            // CreateMap<User, UserForListDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //     })
            //     .ForMember(dest => dest.Age, opt => {
            //         opt.MapFrom(d => d.DateOfBirth.CalculatesAge());
            //     });
            // CreateMap<User, UserForDetailedDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //     })
            //     .ForMember(dest => dest.Age, opt => {
            //         opt.MapFrom(d => d.DateOfBirth.CalculatesAge());
            //     });
            CreateMap<User, UserForDetailedDto>().ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
            // CreateMap<Photo, PhotosForDetailedDto>();
            // CreateMap<UserForUpdateDto, User>();
            // CreateMap<Photo, PhotoForReturnDto>();
            // CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>().ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
            CreateMap<UserForLoginDto, User>().ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
            // CreateMap<MessageForCreationDto, Message>().ReverseMap();
            // CreateMap<Message, MessageToReturnDto>()
            //     .ForMember(m => m.SenderPhotoUrl, opt => opt
            //         .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            //     .ForMember(m => m.RecipientPhotoUrl, opt => opt
            //         .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}