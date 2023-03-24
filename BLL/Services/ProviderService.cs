using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class ProviderService
    {
        public readonly IRepository<Provider> _repository;
        public readonly ILogger<ProviderService> _logger;
        public ProviderService(IRepository<Provider> repository, ILogger<ProviderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Provider Create(Provider provider)
        {
            try
            {
                if (provider != null)
                {
                    return _repository.Create(provider);
                }
                else
                {
                    _logger.LogError("Provider is null when Create");
                    throw new ArgumentNullException(nameof(provider));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Create Provider with error: {ex.Message}");
                throw;
            }
        }

        public void Delete(Provider provider)

        {
            try
            {
                    if (provider != null) { _repository.Delete(provider); }
                    else
                    {
                        _logger.LogError("Provider is null when Delete");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Delete Provider with error: {ex.Message}");
                throw;
            }
        }

        public void Update(Provider provider)

        {
            try
            {
                    if (provider != null) { _repository.Update(provider); }
                    else
                    {
                        _logger.LogError("Provider is null when Update");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Update Provider with error: {ex.Message}");
                throw;
            }

        }

        public IQueryable<Provider> GetAll()
        {
            try
            {
                IQueryable<Provider> providers = _repository.GetAll().AsQueryable();

                if (providers != null && providers.Count() > 0)
                {
                    return providers;
                }
                else
                {
                    _logger.LogError("Providers is null or empty when GetAll");
                    return Enumerable.Empty<Provider>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetAll Providers with error: {ex.Message}");
                throw;
            }
        }

        public Provider GetProviderById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Provider provider = _repository.GetById(id);
                    if (provider != null)
                        return provider;
                    else
                        _logger.LogError("GetById is null, return null");
                    return new Provider();
                }
                else
                {
                    _logger.LogError("Incorrect Id when GetProviderById");
                    return new Provider();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetProviderById: {ex.Message}");
                throw;
            }
        }

        public IQueryable<SelectListItem> GetProvidersLikeSelectListItems()
        {
            IQueryable<SelectListItem> providerListItems = _repository.GetAll().Select(provider => new SelectListItem
            {
                Text = provider.Name,
                Value = provider.Id.ToString()
            });

            return providerListItems;
        }

    }
}
