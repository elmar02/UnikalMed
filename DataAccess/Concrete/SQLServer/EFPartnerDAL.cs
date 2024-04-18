using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.PartnerEntities;
using Entities.DTOs.PartnerDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFPartnerDAL : EFRepositoryBase<Partner, AppDbContext>, IPartnerDAL
    {
        public async Task CreatePhotoAsync(string photoUrl)
        {
            var newPartner = new Partner()
            {
                PhotoUrl = photoUrl,
            };
            await AddAsync(newPartner);
        }

        public async Task<string> DeletePhotoAsync(int partnerId)
        {
            using var context = new AppDbContext();
            var partner = await context.Partners
                .FirstOrDefaultAsync(x => x.Id == partnerId);
            if (partner == null) return null;
            Remove(partner);
            return partner.PhotoUrl;
        }

        public async Task<List<PartnerDTO>> GetAllPhotoAsync()
        {
            using var context = new AppDbContext();
            var cities = await context.Partners.Select(x => new PartnerDTO()
            {
                PartnerId = x.Id,
                PhotoUrl = x.PhotoUrl,
            }).ToListAsync();
            return cities;
        }
    }
}
