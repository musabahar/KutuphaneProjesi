using Kutuphane.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Kutuphane.Data.EntityFramework
{
    public class EF_EntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        public EF_EntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public void fnEkle(TEntity tablo)
        {
            _context.Set<TEntity>().Add(tablo);
        }

        public TEntity fnGetir(Expression<Func<TEntity, bool>> expFiltre, params Expression<Func<TEntity, object>>[] parEklenecekOzellikler)
        {
            IQueryable<TEntity> iqSorgu = _context.Set<TEntity>();
            if (expFiltre != null)
            {
                iqSorgu = iqSorgu.Where(expFiltre);
            }
            if (parEklenecekOzellikler != null && parEklenecekOzellikler.Any())
            {
                foreach (var parOzellik in parEklenecekOzellikler)
                {
                    iqSorgu = iqSorgu.Include(parOzellik);
                }
            }
            return iqSorgu.FirstOrDefault();
        }

        public void fnGuncelle(TEntity tablo)
        {
            _context.Set<TEntity>().Update(tablo);
        }

        public bool fnKontrol(Expression<Func<TEntity, bool>> expFiltre)
        {
            return _context.Set<TEntity>().Any(expFiltre);
        }

        public IList<TEntity> fnListele(int? intBaslangic_Degeri, int? intGosterim_Adeti, out int intToplam_Kayit, Expression<Func<TEntity, bool>> expFiltre = null, params Expression<Func<TEntity, object>>[] expIncludeTablolar)
        {
            intToplam_Kayit = 0;
            IQueryable<TEntity> iqSorgu = _context.Set<TEntity>();
            if (expFiltre != null)
            {
                if (intBaslangic_Degeri != null && intGosterim_Adeti != null)
                    iqSorgu = iqSorgu.Where(expFiltre).Skip((int)intBaslangic_Degeri).Take((int)intGosterim_Adeti);
                else
                    iqSorgu = iqSorgu.Where(expFiltre);

                intToplam_Kayit = _context.Set<TEntity>().Where(expFiltre).Count();
            }
            if (expIncludeTablolar != null && expIncludeTablolar.Any())
            {
                foreach (var expOzellik in expIncludeTablolar)
                {
                    iqSorgu = iqSorgu.Include(expOzellik);
                }

                intToplam_Kayit = iqSorgu.Count();
            }

            if (expFiltre == null && expIncludeTablolar == null)
                intToplam_Kayit = _context.Set<TEntity>().Count();

            return iqSorgu.ToList();
        }

        public void fnSil(TEntity tablo)
        {
            _context.Set<TEntity>().Remove(tablo);
        }

        public int fnToplam_Kayit(Expression<Func<TEntity, bool>> expFiltre)
        {
            return _context.Set<TEntity>().Count(expFiltre);
        }
    }
}
