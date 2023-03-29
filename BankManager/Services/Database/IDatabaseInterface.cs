using DevExpress.Xpo;

namespace BankManager.Services.Database
{
    public interface IDatabaseInterface
    {
        public IDataLayer DataLayer { get; }

        public UnitOfWork ProvisionUnitOfWork();
    }
}