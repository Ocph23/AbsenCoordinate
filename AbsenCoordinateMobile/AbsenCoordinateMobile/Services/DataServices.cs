using AbsenCoordinateMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenCoordinateMobile.Services
{
    public interface IDataServices
    {
        Task<bool> Login(UserLogin model);
        Task<IEnumerable<TempatDetail>> GetTempatAbsen();
        Task<IEnumerable<Absen>> GetAbsens(bool v);
        Task<bool> DoAbsen();

        Task<IEnumerable<Cuti>> GetCuties(bool v);
        Task AddCutiAsync(Cuti newItem);
    }

    public class DataServices : IDataServices
    {
        readonly string controller = "/api/mobile";
        private List<Absen> list= new List<Absen>();
        private List<Cuti> cuties= new List<Cuti>();

        public async Task AddCutiAsync(Cuti newItem)
        {
            try
            {
                using var client = new RestService();
                var response = await client.PostAsync($"{controller}/createcuti", client.GenerateHttpContent(newItem));
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.GetResult<Cuti>();
                    if (result != null)
                    {
                        cuties.Add(result);
                    }
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<bool> DoAbsen()
        {
            try
            {
                using var client = new RestService();
                var response = await client.GetAsync($"{controller}/absen");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.GetResult<Absen>();
                    if (result != null)
                    {

                        var existsData = list.Where(x => x.Id == result.Id).FirstOrDefault();
                        if (existsData != null)
                            existsData.Pulang = result.Pulang;
                        else
                            list.Add(result);
                        return true;
                    }
                    return false;
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<Absen>> GetAbsens(bool v)
        {
            try
            {
                using var client = new RestService();
                var response = await client.GetAsync($"{controller}/getabsen");
                if (response.IsSuccessStatusCode)
                {
                    list = await response.GetResult<List<Absen>>();
                    return list;
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<Cuti>> GetCuties(bool v)
        {
            try
            {
                using var client = new RestService();
                var response = await client.GetAsync($"{controller}/getcuti");
                if (response.IsSuccessStatusCode)
                {
                    cuties = await response.GetResult<List<Cuti>>();
                    return cuties;
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<TempatDetail>> GetTempatAbsen()
        {
            try
            {
                using var client = new RestService();
                var response = await client.GetAsync($"{controller}/tempat");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.GetResult<IEnumerable<TempatDetail>>();
                    return result;
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<bool> Login(UserLogin model)
        {
            try
            {
                using var client = new RestService();
                var response = await client.PostAsync($"{controller}/login", client.GenerateHttpContent(model));
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.GetResult<AuthenticateResponse>();
                    if (result != null)
                    {
                        await Account.SetUser(result);
                        client.SetToken(result.Token);
                        response = await client.GetAsync($"{controller}/profile");
                        if (response.IsSuccessStatusCode)
                        {
                            var profile = await response.GetResult<Karyawan>();
                            if (profile != null)
                            {
                                await Account.SetProfile(profile);
                            }
                        }
                        return true;
                    }
                    return false;
                }
                else
                    throw new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }
    }
}
