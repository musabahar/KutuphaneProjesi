using Kutuphane.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kutuphane.Data.EntityFramework.Repositories
{
    public class EF_tblKitaplar_Repository : EF_EntityRepositoryBase<tblKitaplar>, IEF_tblKitaplar_Repository
    {
        public EF_tblKitaplar_Repository(DbContext context) : base(context)
        {

        }
    }
}
