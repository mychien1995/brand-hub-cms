using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Data.EF.Repositories.Adresses
{
    public interface IAddressRepository : IEntityRepository<Address>
    {
    }
    [ServiceTypeOf(typeof(IAddressRepository))]
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(BrandHubDbContext context) : base(context)
        {

        }
    }
}
