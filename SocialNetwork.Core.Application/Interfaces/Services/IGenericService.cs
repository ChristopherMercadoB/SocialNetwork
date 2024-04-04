namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity>
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {
        Task Create(SaveViewModel vm);
        Task<List<ViewModel>> GetAll();
        Task<SaveViewModel> GetById(int id);
        Task Update(SaveViewModel vm, int id);
        Task Delete(int id);
    }
}