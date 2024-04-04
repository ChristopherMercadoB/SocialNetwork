using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class ReplyService : GenericService<ReplyViewModel, ReplySaveViewModel, Reply>, IReplyService
    {

        private readonly IReplyRepository _repository;
        private readonly IMapper _mapper;

        public ReplyService(IMapper mapper, IReplyRepository repository):base(mapper, repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

       
    }
}
