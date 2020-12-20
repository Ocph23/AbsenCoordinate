using AbsenCoordinatWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Data
{
    public class AbsenService
    {
        private ApplicationDbContext _db;

        public AbsenService(ApplicationDbContext dbcontext)
        {
            _db = dbcontext;
        }


        public Task<Absen> Get(int id)
        {
            var result = _db.Absens.Where(x => x.Id == id)
                .Include(x => x.Karyawan).SingleOrDefault();
            return Task.FromResult(result);
        }



        public Task<IEnumerable<Absen>> Get()
        {
            return Task.FromResult(_db.Absens.AsEnumerable());
        }


        public Task<bool> Delete(int id)
        {
            var data = _db.Absens.SingleOrDefault(x => x.Id == id);
            if (data == null)
                throw new SystemException("Data Tidak Ditemukan !");
            _db.Absens.Remove(data);
            _db.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<Absen> Save(Absen model)
        {
            try
            {
                if (model.Id <= 0)
                    _db.Add(model);
                else
                {
                    var oldData = _db.Absens.SingleOrDefault(x => x.Id == model.Id);
                    if (oldData == null)
                        throw new SystemException("Data Not Found !");

                    _db.Entry(oldData).CurrentValues.SetValues(model);
                }

                _db.SaveChangesAsync();

                return Task.FromResult(model);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

    }
}
