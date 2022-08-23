using Kutuphane.Data.EntityFramework;
using Kutuphane.Data.EntityFramework.Repositories;

namespace Kutuphane.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KutuphaneDBContext _context;
        private EF_tblKitaplar_Repository tblKitaplarRepository;
        private EF_tblKitapHareketler_Repository tblKitapHareketlerRepository;
        public UnitOfWork(KutuphaneDBContext context)
        {
            _context = context;
            tblKitaplarRepository = new EF_tblKitaplar_Repository(_context);
            tblKitapHareketlerRepository = new EF_tblKitapHareketler_Repository(_context);
        }

        public IEF_tblKitaplar_Repository ItblKitaplar => tblKitaplarRepository ?? new EF_tblKitaplar_Repository(_context);
        public IEF_tblKitapHareketler_Repository ItblKitapHareketler => tblKitapHareketlerRepository ?? new EF_tblKitapHareketler_Repository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
        public int fnKaydet()
        {
            return _context.SaveChanges();
        }
    }
}
