using DataAccess.Interface.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Interface.UoW
{
    public interface IUnitOfWork
    {
        IWitnessReportRepository WitnessReport { get; }
        
        void Save();

        IDbContextTransaction OpenTransaction();
    }
}
