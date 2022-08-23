using Kutuphane.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kutuphane.Data.EntityFramework
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity fnGetir(Expression<Func<TEntity, bool>> expFiltre, params Expression<Func<TEntity, object>>[] parEklenecekOzellikler);
        IList<TEntity> fnListele(int? intBaslangic_Degeri, int? intGosterim_Adeti, out int intToplam_Kayit, Expression<Func<TEntity, bool>> expFiltre = null, params Expression<Func<TEntity, object>>[] parEklenecekOzellikler);
        void fnEkle(TEntity tablo);
        void fnGuncelle(TEntity tablo);
        void fnSil(TEntity tablo);
        bool fnKontrol(Expression<Func<TEntity, bool>> expFiltre);
        int fnToplam_Kayit(Expression<Func<TEntity, bool>> expFiltre);
    }
}
