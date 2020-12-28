using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Data
{
    public class CutiService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public CutiService(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _db = dbcontext;
            _userManager = userManager;
        }


        public Task<Cuti> Get(int id)
        {
            var result = _db.Cutis.Where(x => x.Id == id)
                .Include(x => x.Karyawan)
                .Include(x=>x.Persetujuan)
                .SingleOrDefault();
            return Task.FromResult(result);
        }



        public Task<IEnumerable<Cuti>> Get()
        {
            var data = _db.Cutis.Include(x => x.Karyawan)
                .Include(x => x.Persetujuan).AsEnumerable();
            return Task.FromResult(data);
        }


        public Task<bool> Delete(int id)
        {
            var data = _db.Cutis.SingleOrDefault(x => x.Id == id);
            if (data == null)
                throw new SystemException("Data Tidak Ditemukan !");
            _db.Cutis.Remove(data);
            _db.SaveChanges();
            return Task.FromResult(true);
        }

        public async Task<Cuti> Save(Cuti model)
        {
            try
            {
                if (model.Id <= 0)
                    _db.Add(model);
                else
                {
                    var oldData = _db.Cutis.SingleOrDefault(x => x.Id == model.Id);
                    if (oldData == null)
                        throw new SystemException("Data Not Found !");

                    _db.Entry(oldData).CurrentValues.SetValues(model);
                }

                 await _db.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Cuti> Approve(string hrdName, Cuti model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(hrdName.ToUpper());
                if (user == null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");

                var karyawan = _db.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
                  if(karyawan==null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");

                model.Persetujuan.KaryawanId = karyawan.Id;
                if(model.Persetujuan.Id<=0)
                {
                    _db.Persetujuan.Add(model.Persetujuan);
                }
                else
                {
                    var oldData = _db.Persetujuan.SingleOrDefault(x => x.Id == model.Persetujuan.Id);
                        _db.Entry(oldData).CurrentValues.SetValues(model.Persetujuan);

                }
                await _db.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        internal async Task<IEnumerable<Cuti>> GetCutiByUser(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName.ToUpper());
                if (user == null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");
                var karyawan = _db.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
                if (karyawan == null)
                    throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");
                var result = _db.Cutis.Where(x=>x.KaryawanId==karyawan.Id).Include(x => x.Persetujuan).AsEnumerable();
                return result;

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
