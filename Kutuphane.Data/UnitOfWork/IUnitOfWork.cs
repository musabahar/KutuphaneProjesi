using Kutuphane.Data.EntityFramework.Repositories;
namespace Kutuphane.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEF_tblKitaplar_Repository ItblKitaplar { get; }
        IEF_tblKitapHareketler_Repository ItblKitapHareketler { get; }

        int fnKaydet();
    }
}
