using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete;
using Entities.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFBrandDAL : EFRepositoryBase<Brand, AppDbContext>, IBrandDAL
    {
        public async Task<bool> CreateBrandAsync(CreateBrandDTO brandDTO)
        {
            var brand = await GetAsync(x=>x.BrandName == brandDTO.BrandName);
            if (brand != null) return false;
            var newBrand = new Brand()
            {
                BrandName = brandDTO.BrandName,
            };
            await AddAsync(newBrand);
            return true;
        }

        public async Task<List<GetBrandListDTO>> GetAllBrandAsync()
        {
            var brands = await GetAllAsync();
            var brandListDTO = brands
                .Where(x=>!x.IsDeleted)
                .Select(x => new GetBrandListDTO()
            {
                BrandId = x.Id,
                BrandName = x.BrandName,
            }).ToList();
            return brandListDTO;
        }

        public async Task<List<GetBrandListDTO>> GetAllDeletedBrandAsync()
        {
            var brands = await GetAllAsync();
            var brandListDTO = brands
                .Where(x => x.IsDeleted)
                .Select(x => new GetBrandListDTO()
                {
                    BrandId = x.Id,
                    BrandName = x.BrandName,
                }).ToList();
            return brandListDTO;
        }

        public async Task<GetBrandDTO> GetBrandAsync(int brandId)
        {
            var brand = await GetAsync(x=>x.Id == brandId);
            if (brand == null) return null;
            var brandDTO = new GetBrandDTO()
            {
                BrandName = brand.BrandName,
            };
            return brandDTO;
        }

        public bool HardDeleteBrand(int brandId)
        {
            var brand = Get(x => x.Id == brandId);
            if (brand == null) return false;
            Remove(brand);
            return true;
        }

        public bool RestoreBrand(int brandId)
        {
            var brand = Get(x=>x.Id == brandId);
            if (brand == null) return false;
            brand.IsDeleted = false;
            Update(brand);
            return true;
        }

        public bool SoftDeleteBrand(int brandId)
        {
            var brand = Get(x => x.Id == brandId);
            if (brand == null) return false;
            brand.IsDeleted = true;
            Update(brand);
            return true;
        }

        public async Task<bool> UpdateBrandAsync(UpdateBrandDTO brandDTO)
        {
            var brand = await GetAsync(x => x.Id == brandDTO.BrandId && !x.IsDeleted);
            if (brand == null) return false;
            brand.BrandName = brandDTO.BrandName ?? brand.BrandName;
            Update(brand);
            return true;
        }
    }
}
