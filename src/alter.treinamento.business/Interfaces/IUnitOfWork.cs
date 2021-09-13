using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        bool Rollback();
    }
}
