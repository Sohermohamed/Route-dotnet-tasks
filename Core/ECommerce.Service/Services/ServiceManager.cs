using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Abstraction.Services;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class ServiceManager(IMapper mapper,IUnitOfWork unitOfWork) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductServices = new Lazy<IProductService>(()=>new ProductService(unitOfWork, mapper));
        public IProductService ProductService => LazyProductServices.Value;

    }
}
