using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.ServiceEntities;
using Entities.DTOs.ServiceDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFServiceDAL : EFRepositoryBase<Service, AppDbContext>, IServiceDAL
    {
        public async Task<bool> CreateServiceAsync(CreateServiceDTO serviceDTO)
        {
            using var context = new AppDbContext();
            var newService = new Service()
            {
                PhotoUrl = serviceDTO.PhotoUrl,
            };
            await AddAsync(newService);

            var languages = await context.ServiceLanguages.ToListAsync();
            foreach (var language in serviceDTO.Languages)
            {
                if(languages.Any(x=>x.ServiceName == language.ServiceName && x.LangCode == language.LangCode))
                    return false;
                await context.ServiceLanguages.AddAsync(new ServiceLanguage()
                {
                    LangCode = language.LangCode,
                    ServiceName = language.ServiceName,
                    ServiceId = newService.Id,
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public string DeleteService(int serviceId)
        {
            using var context = new AppDbContext();
            var service = context.Services
                .FirstOrDefault(x=>x.Id == serviceId);
            if (service == null) return null;
            Remove(service); 
            return service.PhotoUrl;
        }

        public async Task<List<GetServiceListDTO>> GetAllServiceAsync(string langCode)
        {
            using var context = new AppDbContext();
            var services = await context.Services
                .Include(x => x.Languages)
                .Select(x => new GetServiceListDTO()
                {
                    PhotoUrl = x.PhotoUrl,
                    ServiceName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).ServiceName,
                    ServiceId = x.Id,
                }).ToListAsync();

            return services;
        }

        public async Task<string> UpdateServiceAsync(UpdateServiceDTO serviceDTO)
        {
            using var context = new AppDbContext();
            var service = await context.Services
                .Include(x=>x.Languages)
                .FirstOrDefaultAsync(x=>x.Id == serviceDTO.ServiceId);
            if(service == null) return null;
            foreach (var languageDTO in serviceDTO.Languages)
            {
                var index = service.Languages.FindIndex(x => x.LangCode == languageDTO.LangCode);
                if (index == -1) continue;
                service.Languages[index].ServiceName = languageDTO.ServiceName;
            }
            service.PhotoUrl = serviceDTO.PhotoUrl ?? service.PhotoUrl;
            context.Services.Update(service);
            context.ServiceLanguages.UpdateRange(service.Languages);
            await context.SaveChangesAsync();
            return serviceDTO.PhotoUrl ?? "";
        }
    }
}
