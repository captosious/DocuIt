using System;
using DocuitWeb.Models;
using Microsoft.AspNetCore.ProtectedBrowserStorage;

namespace DocuitWeb.Services
{
    public class StorageTools
    {
        ProtectedSessionStorage _protectedStorage;

        public StorageTools(ProtectedSessionStorage protectedLocalStorage)
        {
            _protectedStorage = protectedLocalStorage;
        }

        public async System.Threading.Tasks.Task BufferPutLocalStorageAsync(Login login)
        {
            await _protectedStorage.SetAsync("CompanyId", login.CompanyId.ToString());
            await _protectedStorage.SetAsync("UserName", login.UserName);
            await _protectedStorage.SetAsync("Name", login.Name);
            await _protectedStorage.SetAsync("FamilyName", login.FamilyName);

        }

        public async System.Threading.Tasks.Task<Login> BufferGetLocalStorageAsync()
        {
            Login login = new Login();

            login.UserName = await _protectedStorage.GetAsync<string>("UserName");
            login.Name = await _protectedStorage.GetAsync<string>("Name");
            login.FamilyName = await _protectedStorage.GetAsync<string>("FamilyName");
            login.CompanyId =
            login.Locked =
            login.
            return login;
        }

        public void BufferClear()
        {
            _protectedStorage.DeleteAsync("UserName");
            _protectedStorage.DeleteAsync("Name");
            _protectedStorage.DeleteAsync("FamilyName");
        }
    }
}
