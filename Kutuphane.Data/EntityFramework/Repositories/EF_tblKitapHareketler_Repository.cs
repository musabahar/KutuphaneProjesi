using Kutuphane.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kutuphane.Data.EntityFramework.Repositories
{
    public class EF_tblKitapHareketler_Repository : EF_EntityRepositoryBase<tblKitapHareketler>, IEF_tblKitapHareketler_Repository
    {
        public EF_tblKitapHareketler_Repository(DbContext context) : base(context)
        {

        }
    }
}
