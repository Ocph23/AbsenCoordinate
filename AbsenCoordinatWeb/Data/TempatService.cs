using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Data
{
    public class TempatService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public TempatService(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _db = dbcontext;
            _userManager = userManager;
        }


        public Task<Tempat> Get(int id)
        {
            var result = _db.Tempats.Where(x => x.Id == id).Include(x => x.Karyawans).SingleOrDefault();
            return Task.FromResult(result);
        }



        public Task<IEnumerable<Tempat>> Get()
        {
            return Task.FromResult(_db.Tempats.AsEnumerable());
        }


        public Task<bool> Delete(int id)
        {
            var data = _db.Tempats.SingleOrDefault(x => x.Id == id);
            if (data == null)
                throw new SystemException("Data Tidak Ditemukan !");
            _db.Tempats.Remove(data);
            _db.SaveChanges();
            return Task.FromResult(true);
        }


        public Task<bool> DeleteKaryawan(int tempatId, int karyawanid)
        {
            try
            {
                var data = _db.TempatDetails.Where(x => x.TempatId == tempatId && x.KaryawanId == karyawanid).FirstOrDefault();
                if (data == null)
                    throw new SystemException("Data Tidak DItemukan !");

                _db.TempatDetails.Remove(data);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public Task<bool> AddKaryawan(int tempatId, int karyawanid)
        {
            try
            {
                var model = new TempatDetail { KaryawanId = karyawanid, TempatId = tempatId };
                _db.TempatDetails.Add(model);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }



        public Task <Tempat> Save (Tempat model)
        {
            try
            {
                if (model.Id <= 0)
                    _db.Add(model);
                else
                {
                    var oldData = _db.Tempats.SingleOrDefault(x => x.Id == model.Id);
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

        internal async Task<IEnumerable<TempatDetail>> GetTempat(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName.ToUpper());
            if (user == null)
                throw new UnauthorizedAccessException("Anda Tidak Memiliki Akses");

            var karyawan = _db.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
            if (karyawan == null)
                throw new UnauthorizedAccessException("Anda Tidak Memiliki Profile");

            var tempats = _db.TempatDetails.Where(x => x.KaryawanId == karyawan.Id).Include(x => x.Tempat).ToList().AsEnumerable();

            return tempats;


        }
    }
}
