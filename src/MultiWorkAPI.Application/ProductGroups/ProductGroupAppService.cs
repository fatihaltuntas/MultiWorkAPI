﻿using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using MultiWorkAPI.ProductGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiWorkAPI.ProductGroups
{
    public class ProductGroupAppService : MultiWorkAPIAppServiceBase, IProductGroupAppService
    {
        private readonly IRepository<ProductGroup, long> _productGroupRepository;

        public ProductGroupAppService(IRepository<ProductGroup, long> productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }


        public CreateProductGroupDto Create(CreateProductGroupDto createProductGroupDto)
        {
            var productGroupEntity = ObjectMapper.Map<ProductGroup>(createProductGroupDto);
            _productGroupRepository.Insert(productGroupEntity);
            return createProductGroupDto;
        }

        public bool Delete(long productGroupId)
        {
            var anyProductGroup = _productGroupRepository.GetAll().Any(x => x.Id == productGroupId);
            if (anyProductGroup)
            {
                _productGroupRepository.Delete(productGroupId);
                return true;

            }
            return false;
        }

        public ProductGroupDto Get(long productGroupId)
        {
            var productGroupEntity = _productGroupRepository.Get(productGroupId);
            ProductGroupDto productGroupDto = ObjectMapper.Map<ProductGroupDto>(productGroupEntity);
            return productGroupDto;
        }

        public ListResultDto<ProductGroupDto> GetAll(GetAllGroupInput input)
        {
            var productGroups = _productGroupRepository
                .GetAll()
                .WhereIf(input.Status.HasValue, b => b.Status == input.Status.Value)
                .OrderByDescending(b => b.CreationTime)
                .ToList();
            return new ListResultDto<ProductGroupDto>(ObjectMapper.Map<List<ProductGroupDto>>(productGroups));
        }

        public ListResultDto<ProductGroupDto> GetProductGroupByStatus(ProductGroupStatus status)
        {
            var productGroupStatus = _productGroupRepository
                .GetAll()
                .Where(x => x.Status == status)
                .ToList();

            return new ListResultDto<ProductGroupDto>(ObjectMapper.Map<List<ProductGroupDto>>(productGroupStatus));
        }

        public UpdateProductGroupDto Update(UpdateProductGroupDto updateProductGroupDto)
        {
            var productEntity = ObjectMapper.Map<ProductGroup>(updateProductGroupDto);
            _productGroupRepository.Update(productEntity);
            return updateProductGroupDto;
        }
    }


}