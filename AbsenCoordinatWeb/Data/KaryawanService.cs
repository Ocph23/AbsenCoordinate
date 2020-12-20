﻿using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Data
{
    public class KaryawanService
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public KaryawanService(ApplicationDbContext db, UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            _db = db;
            _userManager = usermanager;
            _roleManager = rolemanager;
        }

        public Task<IEnumerable<Karyawan>> Get()
        {
            var results = _db.Karyawans.AsEnumerable();
            return Task.FromResult(results);
        }

        public Task<Karyawan> Get(int karyawanId)
        {
            var results = _db.Karyawans.Where(x=>x.Id==karyawanId).FirstOrDefault();
            return Task.FromResult(results);
        }

        public async Task<Karyawan> Save(Karyawan model)
        {
            var trans = _db.Database.BeginTransaction();

            try
            {
                if (model.Id <= 0)
                {
                    var user = new IdentityUser(model.Email);
                    var identity= await _userManager.CreateAsync(user,"Sony@77");
                    if (identity.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Karyawan");
                        model.UserId = user.Id;
                        _db.Karyawans.Add(model);
                    }
                    else
                    {
                        throw new SystemException(identity.Errors.First().Description);
                    }
                }
                else
                {
                    var oldData = _db.Karyawans.SingleOrDefault(x => x.Id == model.Id);
                    if (oldData != null)
                    {
                        _db.Entry(oldData).CurrentValues.SetValues(model);
                    }

                }
                await _db.SaveChangesAsync();
                 await trans.CommitAsync();
                return model;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new SystemException(ex.Message);
            }
        }



    }
}
