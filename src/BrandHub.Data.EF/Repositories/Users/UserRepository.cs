using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Data.EF.Repositories.Users
{
    public interface IUserRepository : IEntityRepository<ApplicationUser>
    {
        ApplicationUser FindByUsername(string username);
        Task<ApplicationUser> FindByUsernameAsync(string username);
    }
    [ServiceTypeOf(typeof(IUserRepository))]
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BrandHubDbContext context) : base(context)
        {

        }

        public ApplicationUser FindByUsername(string username)
        {
            return this.GetQueryable().AsNoTracking().FirstOrDefault(x => x.Username == username);
        }

        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            return await this.GetQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
