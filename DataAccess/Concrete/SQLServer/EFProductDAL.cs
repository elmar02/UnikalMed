using Core.DataAccess.EntityFramework;
using Core.Helpers;
using Core.Helpers.Pagination;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.CategoryEntities;
using Entities.Concrete.HeaderEntities;
using Entities.Concrete.ProductEntities;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.ProductDTOs;
using Entities.DTOs.SubCategoryDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public async Task<IResult> CreateProductAsync(CreateProductDTO productDTO)
        {
            using var context = new AppDbContext();
            var newProduct = new Product()
            {
                IsNew = productDTO.IsNew,
                IsFeatured = productDTO.IsFeatured,
                Guarantee = productDTO.Guarantee,
                ShootCount = productDTO.ShootCount,
                WattValue = productDTO.WattValue
            };

            //Add Brand
            var brand = await context.Brands.FirstOrDefaultAsync(x=>x.Id == productDTO.BrandId);
            if (brand == null)
                return new ErrorDataResult<int>(message: "Belə bir brend mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            newProduct.BrandId = brand.Id;

            //Add SubCategory
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(x => x.Id == productDTO.SubCategoryId);
            if (subCategory == null)
                return new ErrorDataResult<int>(message: "Belə bir alt kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            newProduct.SubCategoryId = subCategory.Id;

            //Create new product
            await AddAsync(newProduct);

            //Add Product Language
            var languages = new List<ProductLanguage>();
            foreach (var languageDTO in productDTO.Languages)
            {
                var newLanguage = new ProductLanguage()
                {
                    ProductId = newProduct.Id,
                    ProductName = languageDTO.ProductName,
                    ProductDescription = languageDTO.ProductDescription,
                    LangCode = languageDTO.LangCode,
                    SeoUrl = languageDTO.ProductName.ConverToSeo(languageDTO.LangCode),
                };

                languages.Add(newLanguage);
            }
            await context.ProductLanguages.AddRangeAsync(languages);
            await context.SaveChangesAsync();
            //Add Product Specification
            for (int i = 0; i < productDTO.Languages.Count; i++)
            {
                if (productDTO.Languages[i].Specifications == null) continue;
                languages[i].Specifications = productDTO.Languages[i].Specifications.Select(x => new ProductSpecification()
                {
                    LanguageId = languages[i].Id,
                    Key = x.Key,
                    IsMain = x.IsMain,
                    Value = x.Value,
                }).ToList();
                await context.ProductSpecifications.AddRangeAsync(languages[i].Specifications);
            }

            //Add Product Pictures
            foreach (var photoUrl in productDTO.PhotoUrls)
            {
                var newPicture = new PictureProduct()
                {
                   PhotoUrl = photoUrl,
                   ProductId = newProduct.Id,
                };
                await context.PictureProducts.AddAsync(newPicture);
            }

            //Add Headers
            await AddHeadersAsync(productDTO.Headers, newProduct.Id, context);

            await context.SaveChangesAsync();
            return new SuccessResult(message: "Məhsul yaradıldı", statusCode: HttpStatusCode.OK);
        }

        public async Task<List<GetProductListDTO>> GetAllDeletedProductAsync(string langCode)
        {
            using var context = new AppDbContext();

            var products = await context.Products
                .Include(x => x.Languages)
                .Include(x => x.Brand)
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.Languages)
                .Where(x => x.IsDeleted)
                .Select(x => new GetProductListDTO()
                {
                    Id = x.Id,
                    BrandName = x.Brand.BrandName,
                    ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    SubCategoryName = x.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                }).ToListAsync();

            return products;
        }

        public async Task<List<GetProductListDTO>> GetAllProductAsync(string langCode)
        {
            using var context = new AppDbContext();

            var products = await context.Products
                .Include(x => x.Languages)
                .Include(x => x.Brand)
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.Languages)
                .Where(x => !x.IsDeleted)
                .Select(x => new GetProductListDTO()
                {
                    Id = x.Id,
                    BrandName = x.Brand.BrandName,
                    ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    SubCategoryName = x.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                }).ToListAsync();

            return products;
        }

        public async Task<GetProductDTO> GetProductAsync(int productId, string langCode)
        {
            using var context = new AppDbContext();
            var product = await context.Products
                .Include(x => x.Languages)
                .Include(x => x.Brand)
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.Languages)
                .Include(x => x.PictureProducts)
                .Include(x => x.Headers)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null) return null;

            var productDTO = new GetProductDTO()
            {
                BrandName = product.Brand.BrandName,
                ProductName = product.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                ProductDescription = product.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductDescription,
                IsFeatured = product.IsFeatured,
                IsNew = product.IsNew,
                PhotoUrls = product.PictureProducts.Select(x => x.PhotoUrl).ToList(),
                SubCategoryName = product.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                Headers = product.Headers?.Select(x =>
                {
                    var headerDTO = new GetProductHeadersDTO()
                    {
                        PhotoUrl = x.PhotoUrl,
                        WoltValue = x.WoltValue,
                    };

                    var headerSpecifications = context.HeaderLanguages
                        .Include(y => y.Specifications)
                        .FirstOrDefault(y => y.HeaderId == x.Id && y.LangCode == langCode).Specifications;
                    if(headerSpecifications != null)
                    {
                        headerDTO.HeaderSpecifications = headerSpecifications.Select(x => new HeaderSpecificationDTO()
                        {
                            Key = x.Key,
                            Value = x.Value,
                        }).ToList();
                    }
                    return headerDTO;
                }).ToList(),
            };

            var productSpecifications = context.ProductLanguages
                .Include(x => x.Specifications)
                .FirstOrDefault(x => x.ProductId == productId && x.LangCode == langCode)?.Specifications;

            if(productSpecifications != null)
            {
                productDTO.ProductSpecifications = productSpecifications.Select(x => new ProductSpecificationDTO()
                {
                    IsMain = x.IsMain,
                    Key = x.Key,
                    Value = x.Value,
                }).ToList();
            }
            return productDTO;
        }

        public async Task<GetProductLangDTO> GetProductWithLangAsync(int productId, string langCode)
        {
            using var context = new AppDbContext();

            var product = await context.Products
                .Include(x => x.PictureProducts)
                .FirstOrDefaultAsync(x => x.Id == productId && !x.IsDeleted);

            if (product == null) return null;

            var headers = context.Headers
                .Include(x=>x.Languages)
                .Where(x=>x.ProductId == productId);

            var headerSpecificationLanguage = context.HeaderLanguages
                .Include(x => x.Specifications)
                .Include(x => x.Header);

            var languages = context.ProductLanguages
                .Include(x => x.Specifications)
                .Include(x => x.Product)
                .Where(x => x.ProductId == product.Id && !x.Product.IsDeleted);

            var subCategories = context.SubCategories
                .Include(x => x.Languages);
            var productLangDTO = new GetProductLangDTO()
            {
                IsNew = product.IsNew,
                BrandId = product.BrandId,
                IsFeatured = product.IsFeatured,
                SubCategoryId = product.SubCategoryId,
                Languages = languages.Select(x => new ProductLanguageDTO()
                {
                    LangCode = x.LangCode,
                    ProductDescription = x.ProductDescription,
                    ProductName = x.ProductName,
                    Specifications = x.Specifications.Where(y => y.LanguageId == x.Id).Select(y => new ProductSpecificationDTO()
                    {
                        Key = y.Key,
                        IsMain = y.IsMain,
                        Value = y.Value
                    }).ToList()
                }).ToList(),
                ProductPictures = product.PictureProducts.Select(x => new GetProductPictureDTO()
                {
                    Id = x.Id,
                    PhotoUrl = x.PhotoUrl,
                }).ToList(),
                Headers = headers.Select(x => new GetHeaderLangDTO()
                {
                    HeaderId = x.Id,
                    PhotoUrl = x.PhotoUrl,
                    Languages = headerSpecificationLanguage.Where(y => y.HeaderId == x.Id).Select(y => new HeaderLangaugeDTO
                    {
                        LangCode = y.LangCode,
                        Specifications = y.Specifications
                        .Where(z => z.LanguageId == y.Id).Select(z => new HeaderSpecificationDTO()
                        {
                            Key = z.Key,
                            Value = z.Value
                        }).ToList(),
                    }).ToList(),

                }).ToList(),
                SubCategories = subCategories.Select(x => new SubCategoryListDTO()
                {
                    Id = x.Id,
                    SubCategoryName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SubCategoryName
                }).ToList(),
                AllBrands = await context.Brands.Select(x => new GetBrandListDTO()
                {
                    BrandId = x.Id,
                    BrandName = x.BrandName
                }).ToListAsync()
            };

            return productLangDTO;
        }

        public IResult RestoreProduct(int productId)
        {
            var product = Get(x => x.Id == productId && x.IsDeleted);
            if (product == null) return new ErrorResult(message: "Belə bir məhsul mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            product.IsDeleted = false;
            Update(product);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult SoftDeleteProduct(int productId)
        {
            var product = Get(x => x.Id == productId && !x.IsDeleted);
            if (product == null) return new ErrorResult(message: "Belə bir məhsul mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            product.IsDeleted = true;
            Update(product);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateProductAsync(UpdateProductDTO productDTO)
        {
            using var context = new AppDbContext();
            var product = await GetAsync(x => x.Id == productDTO.ProductId && !x.IsDeleted);
            if (product == null) return new ErrorResult(message: "Belə bir məhsul mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            //update brand
            var brand = await context.Brands.FirstOrDefaultAsync(x => x.Id == productDTO.BrandId);
            if (brand == null && productDTO.BrandId!=0)
                return new ErrorResult(message: "Belə bir brend mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            else if(productDTO.BrandId!=0) product.BrandId = productDTO.BrandId;

            //update subcategory
            var subCategory = await context.SubCategories.FirstOrDefaultAsync(x => x.Id == productDTO.SubCategoryId);
            if (subCategory == null && productDTO.SubCategoryId != 0) 
                return new ErrorResult(message: "Belə bir alt kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            else if (productDTO.SubCategoryId != 0) product.SubCategoryId = productDTO.SubCategoryId;

            product.IsFeatured = productDTO.IsFeatured;
            product.IsNew = productDTO.IsNew;
            product.WattValue = productDTO.WattValue ?? product.WattValue;
            product.Guarantee = productDTO.Guarantee ?? product.Guarantee;
            product.ShootCount = productDTO.ShootCount ?? product.ShootCount;
            context.Products.Update(product);

            //update languages
            var languages = await context.ProductLanguages
                .Include(x => x.Product)
                .Include(x => x.Specifications)
                .Where(x => x.ProductId == productDTO.ProductId && !x.Product.IsDeleted).ToListAsync();

            if(!productDTO.Languages.IsNullOrEmpty())
                foreach (var item in productDTO.Languages)
                {
                    var index = languages.FindIndex(x => x.LangCode == item.LangCode);
                    if (index == -1) return new ErrorResult(message: "Belə bir dil kodu mövcud deyil", statusCode: HttpStatusCode.BadRequest);
                    languages[index].ProductName = item.ProductName ?? languages[index].ProductName;
                    languages[index].ProductDescription = item.ProductDescription ?? languages[index].ProductDescription ;
                    languages[index].SeoUrl = item.ProductName != null ? item.ProductName.ConverToSeo(item.LangCode) : languages[index].SeoUrl;
                    if (item.Specifications == null) continue;
                    if (!languages[index].Specifications.IsNullOrEmpty())
                        context.ProductSpecifications.RemoveRange(languages[index].Specifications);
                    await context.ProductSpecifications.AddRangeAsync(item.Specifications.Select(x => new ProductSpecification()
                    {
                        LanguageId = languages[index].Id,
                        Key = x.Key,
                        Value = x.Value,
                        IsMain = x.IsMain,
                    }));
                }

            context.ProductLanguages.UpdateRange(languages);

            //update pictures
            if (!productDTO.DeletedProductPictureIds.IsNullOrEmpty())
            {
                var deletedPictures = context.PictureProducts.Where(x => x.ProductId == productDTO.ProductId && productDTO.DeletedProductPictureIds.Contains(x.Id));
                context.PictureProducts.RemoveRange(deletedPictures);

            }

            if (!productDTO.NewPhotoUrls.IsNullOrEmpty())
                foreach (var photoUrl in productDTO.NewPhotoUrls)
                {
                    await context.PictureProducts.AddAsync(new PictureProduct()
                    {
                        PhotoUrl = photoUrl,
                        ProductId = productDTO.ProductId,
                    });
                }

            //headers
            if (!productDTO.DeletedHeaderIds.IsNullOrEmpty())
            {
                var deletedHeaders = context.Headers.Where(x => productDTO.DeletedHeaderIds.Contains(x.Id));
                context.RemoveRange(deletedHeaders);
            }

            if(!productDTO.NewHeaders.IsNullOrEmpty())
                await AddHeadersAsync(productDTO.NewHeaders, productDTO.ProductId, context);

            if (!productDTO.Headers.IsNullOrEmpty()) 
            {
                var result = await UpdateHeadersAsync(productDTO.Headers, context);
                if (!result.Success) return result;
            }

            await context.SaveChangesAsync();
            return new SuccessResult(statusCode: HttpStatusCode.OK);
        }

        public async Task<List<NewComesDTO>> GetNewComesAsync(string langCode)
        {
            using var context = new AppDbContext();
            var products = await context.Products
                .Include(x=>x.PictureProducts)
                .Include(x=>x.SubCategory)
                .ThenInclude(x=>x.Category)
                .ThenInclude(x=>x.Languages)
                .Include(x=>x.Languages)
                .Where(x=>!x.IsDeleted && x.IsNew)
                .Select(x=> new NewComesDTO()
                {
                    CategoryName = x.SubCategory.Category.Languages.FirstOrDefault(y=>y.LangCode == langCode).CategoryName,
                    PhotoUrl = x.PictureProducts.FirstOrDefault().PhotoUrl,
                    SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                    ProductName = x.Languages.FirstOrDefault(y=>y.LangCode == langCode).ProductName,
                }).ToListAsync();

            return products;
        }

        public async Task<List<TrendyProductsDTO>> GetTrendyProductsAsync(string langCode)
        {
            using var context = new AppDbContext();
            var products = await context.Products
                .Include(x => x.PictureProducts)
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.Category)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Languages)
                .Where(x => !x.IsDeleted && x.IsFeatured)
                .Select(x => new TrendyProductsDTO()
                {
                    CategoryName = x.SubCategory.Category.Languages.FirstOrDefault(y => y.LangCode == langCode).CategoryName,
                    PhotoUrl = x.PictureProducts.FirstOrDefault().PhotoUrl,
                    SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                    ProductName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).ProductName,
                }).ToListAsync();

            return products;
        }

        public async Task<ProductDetailDTO> GetProductDetailAsync(int productId, string langCode)
        {
            using var context = new AppDbContext();

            var product = await context.Products
                .Include(x=>x.PictureProducts)
                .Include(x=>x.Headers)
                .ThenInclude(x=>x.Languages)
                .ThenInclude(x=>x.Specifications)
                .Include(x=>x.Languages)
                .ThenInclude(x=>x.Specifications)
                .FirstOrDefaultAsync(x => x.Id == productId && !x.IsDeleted);
            if (product == null) return null;
            var language = product.Languages.FirstOrDefault(x => x.LangCode == langCode);
            var productDetailDTO = new ProductDetailDTO()
            {
                PhotoUrls = product.PictureProducts.Select(x => x.PhotoUrl).ToList(),
                Guarantee = product.Guarantee,
                ShootCount = product.ShootCount,
                WattValue = product.WattValue,
                ProductDescription = language?.ProductDescription,
                MainSpecifications = language?.Specifications
                .Where(x => x.IsMain)
                .Select(x => new ProductSpecificationDetailDTO()
                {
                    Key = x.Key,
                    Value = x.Value
                }).ToList(),
                Headers = product?.Headers?.Select(x=> new HeaderDetailDTO()
                {
                    PhotoUrl = x.PhotoUrl,
                    WoltValue = x.WoltValue,
                    HeaderSpecifications = x.Languages.FirstOrDefault(x => x.LangCode == langCode)?
                    .Specifications.Select(y=>new HeaderSpecificationDetailDTO()
                    {
                        Key = y.Key,
                        Value = y.Value
                    }).ToList()
                }).ToList(),
                OtherSpecifications = language?.Specifications
                .Where(x => !x.IsMain)
                .Select(x => new ProductSpecificationDetailDTO()
                {
                    Key = x.Key,
                    Value = x.Value
                }).ToList(),
            };
            return productDetailDTO;
        }

        public async Task<List<SimilarProductsDTO>> GetSimilarProductsAsync(int productId, string langCode)
        {
            using var context = new AppDbContext();
            var product = await GetAsync(x => x.Id == productId && !x.IsDeleted);
            if (product == null) return null;
            var similarProducts = await context.Products
                .Include(x=>x.PictureProducts)
                .Include(x=>x.Languages)
                .Where(x=>x.SubCategoryId == product.SubCategoryId && x.Id != productId)
                .Take(4)
                .Select(x=> new SimilarProductsDTO()
                {
                    PhotoUrl = x.PictureProducts.FirstOrDefault().PhotoUrl,
                    SeoUrl = x.Languages.FirstOrDefault(x=>x.LangCode == langCode).SeoUrl,
                    ProductName = x.Languages.FirstOrDefault(y=>y.LangCode == langCode).ProductName,
                }).ToListAsync();

            return similarProducts;
        }

        public async Task<List<BrandsProductsDTO>> GetBrandProductsAsync(int productId, string langCode)
        {
            using var context = new AppDbContext();
            var product = await GetAsync(x => x.Id == productId && !x.IsDeleted);
            if (product == null) return null;
            var brandsProducts = await context.Products
                .Include(x => x.PictureProducts)
                .Include(x => x.Languages)
                .Where(x => x.BrandId == product.BrandId && x.Id != productId)
                .Take(4)
                .Select(x => new BrandsProductsDTO()
                {
                    PhotoUrl = x.PictureProducts.FirstOrDefault().PhotoUrl,
                    SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                    ProductName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).ProductName
                }).ToListAsync();

            return brandsProducts;
        }

        public async Task<FilterProductDTO> GetFilteredProductsAsync(int categoryId, int subCategoryId, int page, int limit, string langCode)
        {
            using var context = new AppDbContext();
            var products = context.Products
                .Include(x => x.SubCategory)
                .ThenInclude(x=>x.Languages)
                .Include(x=>x.SubCategory)
                .ThenInclude(x=>x.Category)
                .ThenInclude(x=>x.Languages)
                .Include(x=>x.Languages)
                .Where(x=>!x.IsDeleted);
            
            if (categoryId > 0)
            {
                products = products.Where(x => x.SubCategory.CategoryId == categoryId);
            }

            if(subCategoryId > 0)
            {
                products = products.Where(x => x.SubCategoryId == subCategoryId);
            }

            var productsDTO = await products.Select(x => new ShopProductsDTO()
            {
                CategoryName = x.SubCategory.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                SubCategoryName = x.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
            }).ToListAsync();

            var pageDTO = productsDTO.CreatePage(page, limit);

            var categoryFilterDTOs = context.Categories
            .Include(x => x.Languages)
            .Include(x => x.SubCategories)
            .ThenInclude(x => x.Languages)
            .Where(x => !x.IsDeleted)
            .Select(x => new CategoryFilterDTO()
            {
                CategoryId = x.Id,
                CategoryName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).CategoryName,
                SubCategories = x.SubCategories
                    .Where(y => !y.IsDeleted)
                    .Select(y => new SubCategoryFilterDTO()
                    {
                        SubCategoryId = y.Id,
                        SubCategoryName = y.Languages.FirstOrDefault(z => z.LangCode == langCode).SubCategoryName,
                        ProductCount = products.Count(z=>z.SubCategoryId == y.Id)
                    }).ToList()
            }).ToList();

            foreach (var item in categoryFilterDTOs)
            {
                item.ProductCount = item.SubCategories.Sum(x => x.ProductCount);
            }
            var filterDTO = new FilterProductDTO()
            {
                PageDatas = pageDTO,
                Products = productsDTO,
                Categories = categoryFilterDTOs
            };


            return filterDTO;
        }

        public async Task<FilterCategoryProductDTO> GetFilteredCategoryProductsAsync(int categoryId, List<string> values, int page, int limit, string langCode)
        {
            if (categoryId == 0) return null;
            using var context = new AppDbContext();
            var products = context.Products
                .Include(x => x.SubCategory)
                .Include(x=>x.Languages)
                .ThenInclude(x=>x.Specifications)
                .Where(x => !x.IsDeleted && x.SubCategory.CategoryId == categoryId);
            if (!values.IsNullOrEmpty())
                products = products
                    .Where(x=> x.Languages
                    .Any(y => y.Specifications
                    .Any(z => values
                    .Contains(z.Value))));
            var productsDTO = await products.Select(x => new ShopProductsDTO()
            {
                CategoryName = x.SubCategory.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                SubCategoryName = x.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
            }).ToListAsync();

            var pageDTO = productsDTO.CreatePage(page, limit);

            var specifications = context.ProductSpecifications
                .Include(x => x.Language)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.SubCategory)
                .Where(x => !x.Language.Product.IsDeleted && x.Language.LangCode == langCode && x.Language.Product.SubCategory.CategoryId == categoryId);

            if (!values.IsNullOrEmpty())
                specifications = specifications.Where(x => values.Contains(x.Value));

            var specificationFilterList = new List<FilterSpecificationDTO>();

            foreach (var item in specifications)
            {
                var existKey = specificationFilterList.FirstOrDefault(x => x.Key == item.Key);
                if (existKey != null)
                {
                    var existValue = existKey.Values.FirstOrDefault(x=>x.Value == item.Value && x.Value == item.Value);
                    if(existValue != null)
                    {
                        existValue.ProductCount++;
                    }
                    else
                    {
                        existKey.Values.Add(new FilterValuesDTO()
                        {
                            ProductCount = 1,
                            Value = item.Value,
                        });
                    }
                }
                else
                {
                    specificationFilterList.Add(new FilterSpecificationDTO()
                    {
                        Key = item.Key,
                        Values = new List<FilterValuesDTO>()
                        {
                            new FilterValuesDTO()
                            {
                                ProductCount = 1,
                                Value = item.Value,
                            }
                        }
                    });
                }
            }


            var filterDTO = new FilterCategoryProductDTO()
            {
                PageDatas = pageDTO,
                Products = productsDTO,
                Specifications = specificationFilterList
            };

            return filterDTO;
        }

        public async Task<FilterCategoryProductDTO> GetFilteredSubCategoryProductsAsync(int categoryId, int subCategoryId, List<string> values, int page, int limit, string langCode)
        {
            if (categoryId == 0 || subCategoryId == 0) return null;
            using var context = new AppDbContext();
            var products = context.Products
                .Include(x => x.SubCategory)
                .Include(x => x.Languages)
                .ThenInclude(x => x.Specifications)
                .Where(x => !x.IsDeleted && x.SubCategory.CategoryId == categoryId && x.SubCategoryId == subCategoryId);
            if (!values.IsNullOrEmpty())
                products = products
                    .Where(x => x.Languages
                    .Any(y => y.Specifications
                    .Any(z => values
                    .Contains(z.Value))));
            var productsDTO = await products.Select(x => new ShopProductsDTO()
            {
                CategoryName = x.SubCategory.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                SubCategoryName = x.SubCategory.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
            }).ToListAsync();

            var pageDTO = productsDTO.CreatePage(page, limit);

            var specifications = context.ProductSpecifications
                .Include(x => x.Language)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.SubCategory)
                .Where(x => !x.Language.Product.IsDeleted && x.Language.LangCode == langCode && x.Language.Product.SubCategory.CategoryId == categoryId && x.Language.Product.SubCategoryId == subCategoryId);

            if (!values.IsNullOrEmpty())
                specifications = specifications.Where(x => values.Contains(x.Value));

            var specificationFilterList = new List<FilterSpecificationDTO>();

            foreach (var item in specifications)
            {
                var existKey = specificationFilterList.FirstOrDefault(x => x.Key == item.Key);
                if (existKey != null)
                {
                    var existValue = existKey.Values.FirstOrDefault(x => x.Value == item.Value && x.Value == item.Value);
                    if (existValue != null)
                    {
                        existValue.ProductCount++;
                    }
                    else
                    {
                        existKey.Values.Add(new FilterValuesDTO()
                        {
                            ProductCount = 1,
                            Value = item.Value,
                        });
                    }
                }
                else
                {
                    specificationFilterList.Add(new FilterSpecificationDTO()
                    {
                        Key = item.Key,
                        Values = new List<FilterValuesDTO>()
                        {
                            new FilterValuesDTO()
                            {
                                ProductCount = 1,
                                Value = item.Value,
                            }
                        }
                    });
                }
            }


            var filterDTO = new FilterCategoryProductDTO()
            {
                PageDatas = pageDTO,
                Products = productsDTO,
                Specifications = specificationFilterList
            };

            return filterDTO;
        }

        public async Task<List<SearchProductDTO>> SearchProducts(string productName, string langCode)
        {
            using var context = new AppDbContext();
            var productsDTO = await context.Products
                .Include(x => x.Languages)
                .Select(x => new SearchProductDTO()
                {
                    ProductId = x.Id,
                    ProductName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    SeoUrl = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,

                })
                .Where(x=>x.ProductName.Contains(productName))
                .ToListAsync();
            return productsDTO;
        }

        public async Task<List<ProductSliderDTO>> GetProductSliders(string langCode)
        {
            using var context = new AppDbContext();
            var productSliders = await context.Products
                .Include(x => x.Languages)
                .Include(x => x.PictureProducts)
                .Where(x => !x.IsDeleted && x.WattValue != null && x.ShootCount != null && x.Guarantee != null)
                .Select(x => new ProductSliderDTO()
                {
                    Guarantee = x.Guarantee,
                    WattValue = x.WattValue,
                    ShootCount = x.ShootCount,
                    SeoUrl = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SeoUrl,
                    ProductDescription = x.Languages.FirstOrDefault(y => y.LangCode == langCode).ProductDescription,
                    ProductName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).ProductName,
                    ProductId = x.Id,
                    PhotoUrl = x.PictureProducts.FirstOrDefault().PhotoUrl,
                }).ToListAsync();

            return productSliders;
        }

        private async static Task<IResult> UpdateHeadersAsync(List<GetHeaderLangDTO> productHeaders, AppDbContext context)
        {
            foreach (var headerDTO in productHeaders)
            {
                var header = await context.Headers
                    .FirstOrDefaultAsync(x => x.Id == headerDTO.HeaderId);
                if (header == null) return new ErrorResult(message: "Belə bir başlıq kodu mövcud deyil", statusCode: HttpStatusCode.BadRequest);
                header.WoltValue = headerDTO.WoltValue;
                header.PhotoUrl = headerDTO.PhotoUrl ?? header.PhotoUrl;
                context.Headers.Update(header);
                var languages = await context.HeaderLanguages
                    .Include(x => x.Specifications)
                    .Where(x => x.HeaderId == headerDTO.HeaderId).ToListAsync();
                if (headerDTO.Languages.IsNullOrEmpty()) continue;
                foreach (var item in headerDTO.Languages)
                {
                    var index = languages.FindIndex(x => x.LangCode == item.LangCode);
                    if (index == -1) return new ErrorResult(message: "Belə bir dil kodu mövcud deyil", statusCode: HttpStatusCode.BadRequest);
                    if (item.Specifications == null) continue;
                    if (!languages[index].Specifications.IsNullOrEmpty())
                        context.HeaderSpecifications.RemoveRange(languages[index].Specifications);
                    await context.HeaderSpecifications.AddRangeAsync(item.Specifications.Select(x => new HeaderSpecification()
                    {
                        LanguageId = languages[index].Id,
                        Key = x.Key,
                        Value = x.Value,
                    }));
                }
                context.HeaderLanguages.UpdateRange(languages);
            }
            return new SuccessResult(HttpStatusCode.OK);
        }
        
        private async static Task AddHeadersAsync(List<HeaderDTO> productHeaders, int productId, AppDbContext context)
        {
            var newHeaders = productHeaders.Select(x => new Header()
            {
                ProductId = productId,
                WoltValue = x.WoltValue,
                PhotoUrl = x.PhotoUrl,
            }).ToList();

            await context.Headers.AddRangeAsync(newHeaders);
            await context.SaveChangesAsync();

            var allLanguages = new List<HeaderLanguage>();
            for (int i = 0; i < newHeaders.Count; i++)
            {
                var specificationLanguages = productHeaders[i].Languages.Select(x => new HeaderLanguage()
                {
                    HeaderId = newHeaders[i].Id,
                    LangCode = x.LangCode,
                });
                allLanguages.AddRange(specificationLanguages);
            }
            await context.HeaderLanguages.AddRangeAsync(allLanguages);
            await context.SaveChangesAsync();

            for (int i = 0; i < productHeaders.Count; i++)
            {
                for (int j = 0; j < productHeaders[i].Languages.Count; j++)
                {
                    var languages = allLanguages.Where(x => x.HeaderId == newHeaders[i].Id).ToList();
                    var specifications = productHeaders[i].Languages[j].Specifications.Select(x => new HeaderSpecification()
                    {
                        LanguageId = languages[j].Id,
                        Key = x.Key,
                        Value = x.Value
                    });
                    await context.HeaderSpecifications.AddRangeAsync(specifications);
                }
            }
        }

    }
}
