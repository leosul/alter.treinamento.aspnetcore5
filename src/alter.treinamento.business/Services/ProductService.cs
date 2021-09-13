using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Add(Product product)
        {
            _productRepository.Add(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Update(Product product)
        {
            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remove(Guid id)
        {
            _productRepository.Remove(id);
            return await _productRepository.UnitOfWork.Commit();
        }
        public void SetIsActive(bool isActive)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
