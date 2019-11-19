using BrandHub.Data.EF.Entities;
using BrandHub.Data.EF.Extensions;
using BrandHub.Data.EF.Repositories.Adresses;
using BrandHub.Data.EF.Repositories.Organizations;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using BrandHub.Models.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Services.Organizations
{
    public interface IOrganizationService
    {
        OperationResult<OrganizationModel> CreateOrganization(UpdateOrganizationRequest request);
        OperationResult<OrganizationModel> UpdateOrganization(UpdateOrganizationRequest request);
        OperationResult<bool> DeleteOrganization(int id);

        SearchResult<OrganizationModel> SearchOrganization(SearchOrganizationRequest request);

        OrganizationModel GetOrganization(int id);
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

        public OperationResult<OrganizationModel> CreateOrganization(UpdateOrganizationRequest request)
        {
            if (request.IncludeHostname)
            {
                foreach (var hostName in request.HostNames)
                {
                    var existingHost = _hostDefinitionRepository.FindByName(hostName);
                    if (existingHost != null)
                    {
                        return new OperationResult<OrganizationModel>(false, Constants.Messages.ORGANIZATION_DUPLICATE_HOST);
                    }
                }
            }
            var address = new Address();
            address.AddressLine = request.AddressLine;
            address.CountryId = request.CountryId;
            address.ProvinceId = request.ProvinceId;
            address.DistrictId = request.DistrictId;
            var newAddress = _addressRepository.Insert(address);
            _addressRepository.SaveChanges();
            var organization = request.ToEntity();
            organization.AddressId = newAddress.ID;
            var newOrganization = _organizationRepository.Insert(organization);
            _organizationRepository.SaveChanges();
            if (request.IncludeHostname)
            {
                foreach (var hostName in request.HostNames)
                {
                    _hostDefinitionRepository.Insert(new HostDefinition()
                    {
                        HostName = hostName,
                        OrganizationId = newOrganization.ID
                    });
                }
                _hostDefinitionRepository.SaveChanges();
            }
            return new OperationResult<OrganizationModel>(newOrganization.ToModel(), true);
        }

        public OperationResult<OrganizationModel> UpdateOrganization(UpdateOrganizationRequest request)
        {
            var existingOrganization = _organizationRepository.GetById(request.ID);
            if (existingOrganization == null || existingOrganization.IsDeleted)
            {
                return new OperationResult<OrganizationModel>(false, Constants.Messages.ORGANIZATION_DONT_EXIST);
            }
            if (request.IncludeHostname)
            {
                foreach (var hostName in request.HostNames)
                {
                    var existingHost = _hostDefinitionRepository.FindByName(hostName);
                    if (existingHost != null && existingHost.OrganizationId != existingOrganization.ID)
                    {
                        return new OperationResult<OrganizationModel>(false, Constants.Messages.ORGANIZATION_DUPLICATE_HOST);
                    }
                }
            }
            var address = new Address();
            address.AddressLine = request.AddressLine;
            address.CountryId = request.CountryId;
            address.ProvinceId = request.ProvinceId;
            address.DistrictId = request.DistrictId;
            Address newAddress;
            if (request.AddressId == 0)
                newAddress = _addressRepository.Insert(address);
            else
            {
                address.ID = existingOrganization.AddressId;
                newAddress = _addressRepository.Update(address);
            }
            _addressRepository.SaveChanges();
            var organization = request.ToEntity();
            organization.AddressId = newAddress.ID;
            _organizationRepository.Update(organization);
            _organizationRepository.SaveChanges();
            if (request.IncludeHostname)
            {
                var organizationHosts = _hostDefinitionRepository.GetByOrganization(existingOrganization.ID);
                foreach (var oldHost in organizationHosts)
                {
                    if (request.HostNames.All(x => !x.Equals(oldHost.HostName, StringComparison.OrdinalIgnoreCase)))
                    {
                        _hostDefinitionRepository.Delete(oldHost.ID);
                    }
                }
                foreach (var newHost in request.HostNames)
                {
                    if (organizationHosts.All(x => !x.HostName.Equals(newHost, StringComparison.OrdinalIgnoreCase)))
                    {
                        _hostDefinitionRepository.Insert(new HostDefinition()
                        {
                            HostName = newHost,
                            OrganizationId = existingOrganization.ID
                        });
                    }
                }
                _hostDefinitionRepository.SaveChanges();
            }
            return new OperationResult<OrganizationModel>(existingOrganization.ToModel(), true);
        }

        public OperationResult<bool> DeleteOrganization(int id)
        {
            var existingOrganization = _organizationRepository.GetById(id);
            if (existingOrganization == null || existingOrganization.IsDeleted)
            {
                return new OperationResult<bool>(false, Constants.Messages.ORGANIZATION_DONT_EXIST);
            }
            existingOrganization.IsDeleted = true;
            _organizationRepository.Update(existingOrganization);
            _organizationRepository.SaveChanges();
            return new OperationResult<bool>(true, true);
        }

        public SearchResult<OrganizationModel> SearchOrganization(SearchOrganizationRequest request)
        {
            var searchResult = _organizationRepository.Search(request);
            var result = new SearchResult<OrganizationModel>();
            searchResult.Result.ForEach(x =>
            {
                var organizationModel = x.ToModel();
                organizationModel.AddressName = x.Address?.AddressLine;
                result.Result.Add(organizationModel);
            });
            result.Total = searchResult.Total;
            return result;
        }

        public OrganizationModel GetOrganization(int id)
        {
            var organization = _organizationRepository.GetById(id);
            if (organization != null) return organization.ToModel();
            return null;
        }
    }
}
