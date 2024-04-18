using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.AdvertEntities;
using Entities.DTOs.AdvertDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFAdvertDAL : EFRepositoryBase<Advert, AppDbContext>, IAdvertDAL
    {
        public async Task CreatePhotoAsync(CreateAdvertDTO advertDTO)
        {

            using var context = new AppDbContext();
            var newAdvert = new Advert()
            {
                Link = advertDTO.Link,
                IsHidden = advertDTO.IsHidden,
            };
            await AddAsync(newAdvert);
            var languages = new List<AdvertLanguage>();
            foreach (var item in advertDTO.Languages)
            {
                languages.Add(new AdvertLanguage()
                {
                    LangCode = item.LangCode,
                    AdvertId = newAdvert.Id,
                    PhotoUrl = item.PhotoUrl,
                });
            }
            await context.AdvertLanguages.AddRangeAsync(languages);
            await context.SaveChangesAsync();
        }

        public async Task<List<string>> DeletePhotoAsync(int advertId)
        {
            using var context = new AppDbContext();
            var advert = await context.Adverts
                .Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == advertId);
            if (advert == null) return null;
            Remove(advert);
            return advert.Languages.Select(x => x.PhotoUrl).ToList();
        }

        public async Task<List<AdvertDTO>> GetAllPhotoAsync(string langCode)
        {
            using var context = new AppDbContext();
            var photos = await context.Adverts
                .Include(x => x.Languages)
                .Select(x => new AdvertDTO()
                {
                    AdvertId = x.Id,
                    IsHidden = x.IsHidden,
                    Link = x.Link,
                    PhotoUrl = x.Languages.FirstOrDefault(x=>x.LangCode == langCode).PhotoUrl
                }).ToListAsync();

            return photos;
        }

        public async Task<List<UnhiddenAdvertDTO>> GetAllUnhiddenPhotoAsync(string langCode)
        {
            using var context = new AppDbContext();
            var photos = await context.Adverts
                .Include(x => x.Languages)
                .Where(x=>!x.IsHidden)
                .Select(x => new UnhiddenAdvertDTO()
                {
                    Link = x.Link,
                    PhotoUrl = x.Languages.FirstOrDefault(y => y.LangCode == langCode).PhotoUrl
                }).ToListAsync();

            return photos;
        }

    }
}
