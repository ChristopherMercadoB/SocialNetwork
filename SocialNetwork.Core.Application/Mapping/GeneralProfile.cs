using AutoMapper;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.FriendShip;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Mapping
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewModel>()
            .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

            CreateMap<User, UserSaveViewModel>()
                .ForMember(dest => dest.File, obj => obj.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

            CreateMap<Post, PostViewModel>()
            .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

            CreateMap<Post, PostSaveViewModel>()
            .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

            CreateMap<Comment, CommentSaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());
            CreateMap<FriendShip, FriendShipSaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());
            CreateMap<FriendShip, FriendShipViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, obj => obj.Ignore())
                .ForMember(dest => dest.CreatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedDate, obj => obj.Ignore())
                .ForMember(dest => dest.UpdatedBy, obj => obj.Ignore());

        }
    }
}
