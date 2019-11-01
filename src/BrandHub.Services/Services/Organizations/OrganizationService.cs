using BrandHub.Data.EF.Entities;
using BrandHub.Data.EF.Extensions;
using BrandHub.Data.EF.Repositories.Adresses;
using BrandHub.Data.EF.Repositories.Organizations;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Services.Organizations
{
    public interface IOrganizationService
    {
        OperationResult<OrganizationModel> CreateOrganization(CreateOrganizationRequest request)
    }

    [ServiceTypeOf(typeof(IOrganizationService))]
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IHostDefinitionRepository _hostDefinitionRepository;
        public OrganizationService(IOrganizationRepository organizationRepository, IAddressRepository addressRepository, IHostDefinitionRepository hostDefinitionRepository)
        {
            _organizationRepository = organizationRepository;
            _addressRepository = addressRepository;
            _hostDefinitionRepository = hostDefinitionRepository;
        }

        public OperationResult<OrganizationModel> CreateOrganization(CreateOrganizationRequest request)
        {
            var existingHost = _hostDefinitionRepository.FindByName(request.HostName);
            if (existingHost != null)
            {
                return new OperationResult<OrganizationModel>(false, Constants.Messages.ORGANIZATION_DUPLICATE_HOST);
            }
            var address = new Address();
            address.AddressLine = request.AddressLine;
            address.CountryId = request.CountryId;
            address.DistrictId = request.DistrictId;
            var newAddress = _addressRepository.Insert(address);
            _addressRepository.SaveChanges();
            var organization = request.ToEntity();
            organization.AddressId = newAddress.ID;
            var newOrganization = _organizationRepository.Insert(organization);
            _organizationRepository.SaveChanges();
            _hostDefinitionRepository.Insert(new HostDefinition()
            {
                HostName = request.HostName,
                OrganizationId = newOrganization.ID
            });
            _hostDefinitionRepository.SaveChanges();
            return new OperationResult<OrganizationModel>(newOrganization.ToModel(), true);
        }
    }
}
