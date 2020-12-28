using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Data
{
    public class AbsenService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSetting;

        public AbsenService(IOptions<AppSettings> appSetting,
            ApplicationDbContext dbcontext,
            UserManager<IdentityUser> userManager)
        {
            _db = dbcontext;
            _userManager = userManager;
            _appSetting = appSetting.Value;
        }

        public Task<Absen> Get(int id)
        {
            var result = _db.Absens.Where(x => x.Id == id)
                .Include(x => x.Karyawan).SingleOrDefault();
            return Task.FromResult(result);
        }


        public Task<IEnumerable<Absen>> Get(QueryAbsenParam aparam)
        {
            var dari = new DateTime(aparam.Dari.Value.Year, aparam.Dari.Value.Month, aparam.Dari.Value.Day);
            var sampai = new DateTime(aparam.Sampai.Value.Year, aparam.Sampai.Value.Month, aparam.Sampai.Value.Day).AddDays(1);
            var result = _db.Absens.Where(x => x.Masuk >= dari && x.Masuk <= sampai)
                .Include(x => x.Karyawan).AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Absen>> GetToday()
        {

            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var result = _db.Absens.Where(x=>x.Masuk.Value>=today).Include(x => x.Karyawan).AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Absen>> Get()
        {
            var result = _db.Absens.Include(x => x.Karyawan).AsEnumerable();
            return Task.FromResult(result);
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

        internal async Task<IEnumerable<Absen>> GetAbsenByUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new SystemException("User Not Found !");
            var karyawan = _db.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
            var absens = _db.Absens.Where(x => x.KaryawanId == karyawan.Id);
            return absens;

        }

        public async Task AddAbsen(Absen absen)
        {
            try
            {
                var result = _db.Absens.Add(absen);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Absen> DoAbsen(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    throw new SystemException("User Not Found !");
                var karyawan = _db.Karyawans.SingleOrDefault(x => x.UserId == user.Id);
                if (karyawan == null)
                    throw new SystemException("User Not Found !");
               var result= Save(karyawan.Id);
                return result;

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        private Absen Save(int karyawanId)
        {
            var _absenSetting = _appSetting.AbsenSetting;
            try
            {
                var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var masuk = new DateTime(now.Year, now.Month, now.Day, _absenSetting.Masuk.Hours,
                    _absenSetting.Masuk.Minutes, _absenSetting.Masuk.Seconds);

                var pulang = new DateTime(now.Year, now.Month, now.Day, _absenSetting.Pulang.Hours,
                    _absenSetting.Pulang.Minutes, _absenSetting.Pulang.Seconds);

                var absenToday = _db.Absens.Where(x => x.KaryawanId == karyawanId && x.Masuk.Value.Year == now.Year
                && x.Masuk.Value.Month == now.Month && x.Masuk.Value.Day == now.Day).FirstOrDefault();
                if (absenToday == null)
                {
                    //Absen Masuk
                    if (now.TimeOfDay > masuk.TimeOfDay)
                    {
                        if (!_absenSetting.Konpensasi)
                            throw new SystemException("Maaf Anda Terlambat !");
                        else
                        {
                            var konpensasi = masuk.Add(_absenSetting.KonpensasiMasuk);
                            if (now.TimeOfDay > konpensasi.TimeOfDay)
                                throw new SystemException("Maaf Anda Terlambat !");
                        }
                    }
                    absenToday = new Absen { KaryawanId = karyawanId, Masuk = now };
                    _db.Absens.Add(absenToday);
                }
                else
                {
                    //absen pulang
                    if (absenToday.Pulang != null)
                        throw new SystemException("Anda Sudah Absen Pulang Hari Ini !");

                    if (now.TimeOfDay < pulang.TimeOfDay)
                    {
                        if (!_absenSetting.Konpensasi)
                            throw new SystemException("Maaf Anda Belum Saatnya Pulang !");
                        else
                        {
                            var konpensasi = pulang.Subtract(_absenSetting.KonpensasiPulang);
                            if (now.TimeOfDay < konpensasi.TimeOfDay)
                                throw new SystemException("Maaf, Belum Saatnya Anda Pulang !");
                        }
                    }
                    absenToday.Pulang = now;
                    _db.Absens.Update(absenToday);
                }

                _db.SaveChanges();
                return absenToday;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }

    public enum CanAbsenType
    {
        None, Masuk, Pulang
    }
}
