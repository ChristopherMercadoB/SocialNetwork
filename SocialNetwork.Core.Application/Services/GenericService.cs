using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, Entity> : IGenericService<ViewModel, SaveViewModel, Entity> 
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Entity> _repository;

        public GenericService(IMapper mapper, IGenericRepository<Entity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ViewModel>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

        public async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task Create(SaveViewModel vm)
        {
            var entity = _mapper.Map<Entity>(vm);
            await _repository.AddAsync(entity);
        }

        public async Task Update(SaveViewModel vm, int id)
        {
            var entity = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }
    }
}
