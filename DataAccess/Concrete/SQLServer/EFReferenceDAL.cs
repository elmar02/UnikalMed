using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.ReferenceEntities;
using Entities.Concrete.StaffEntities;
using Entities.DTOs.ReferenceDTOs;
using Entities.DTOs.StaffDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFReferenceDAL : EFRepositoryBase<Reference, AppDbContext> ,IReferenceDAL
    {
        public async Task<bool> CreateReferenceAsync(CreateReferenceDTO referenceDTO)
        {
            using var context = new AppDbContext();
            var newReference = new Reference()
            {
                PhotoUrl = referenceDTO.PhotoUrl,
            };
            await AddAsync(newReference);

            var languages = await context.ReferenceLanguages.ToListAsync();
            foreach (var language in referenceDTO.Languages)
            {
                await context.ReferenceLanguages.AddAsync(new ReferenceLanguage()
                {
                    LangCode = language.LangCode,
                    SurName = language.SurName,
                    Name = language.Name,
                    ReferenceId = newReference.Id,
                    Address = language.Address,
                    Content = language.Content,
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public string DeleteReference(int referenceId)
        {
            using var context = new AppDbContext();
            var staff = context.References
                .FirstOrDefault(x => x.Id == referenceId);
            if (staff == null) return null;
            Remove(staff);
            return staff.PhotoUrl;
        }

        public async Task<List<GetReferenceListDTO>> GetAllReferenceAsync(string langCode)
        {
            using var context = new AppDbContext();
            var staffs = await context.References
                .Include(x => x.Languages)
                .Select(x => new GetReferenceListDTO()
                {
                    PhotoUrl = x.PhotoUrl,
                    Content = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Address = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Address,
                    Name = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Name,
                    SurName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SurName,
                    ReferenceId = x.Id
                }).ToListAsync();

            return staffs;
        }

        public async Task<string> UpdateReferenceAsync(UpdateReferenceDTO referenceDTO)
        {
            using var context = new AppDbContext();
            var reference = await context.References
                .Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == referenceDTO.ReferenceId);
            if (reference == null) return null;
            foreach (var languageDTO in referenceDTO.Languages)
            {
                var index = reference.Languages.FindIndex(x => x.LangCode == languageDTO.LangCode);
                if (index == -1) continue;
                reference.Languages[index].Name = languageDTO.Name ?? reference.Languages[index].Name;
                reference.Languages[index].SurName = languageDTO.SurName ?? reference.Languages[index].SurName;
                reference.Languages[index].Content = languageDTO.Content ?? reference.Languages[index].Content;
                reference.Languages[index].Address = languageDTO.Address ?? reference.Languages[index].Address;
            }
            reference.PhotoUrl = referenceDTO.ProfilePhotoUrl ?? reference.PhotoUrl;
            context.References.Update(reference);
            context.ReferenceLanguages.UpdateRange(reference.Languages);
            await context.SaveChangesAsync();
            return referenceDTO.ProfilePhotoUrl ?? "";
        }
    }
    }
