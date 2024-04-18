using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.ServiceEntities;
using Entities.Concrete.StaffEntities;
using Entities.DTOs.ServiceDTOs;
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
    public class EFStaffDAL : EFRepositoryBase<Staff, AppDbContext>, IStaffDAL
    {
        public async Task<bool> CreateStaffAsync(CreateStaffDTO staffDTO)
        {
            using var context = new AppDbContext();
            var newStaff = new Staff()
            {
                ProfilePhotoUrl = staffDTO.PhotoUrl
            };
            await AddAsync(newStaff);

            var languages = await context.StaffLanguages.ToListAsync();
            foreach (var language in staffDTO.Languages)
            {
                await context.StaffLanguages.AddAsync(new StaffLanguage()
                {
                    LangCode = language.LangCode,
                    SurName = language.SurName,
                    Name = language.Name,
                    StaffId = newStaff.Id,
                    Profession = language.Profession,
                    Content = language.Content,
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public string DeleteStaff(int staffId)
        {
            using var context = new AppDbContext();
            var staff = context.Staffs
                .FirstOrDefault(x => x.Id == staffId);
            if (staff == null) return null;
            Remove(staff);
            return staff.ProfilePhotoUrl;
        }

        public async Task<List<GetStaffListDTO>> GetAllStaffAsync(string langCode)
        {
            using var context = new AppDbContext();
            var staffs = await context.Staffs
                .Include(x => x.Languages)
                .Select(x => new GetStaffListDTO()
                {
                    PhotoUrl = x.ProfilePhotoUrl,
                    Content = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Profession = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Profession,
                    Name = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Name,
                    SurName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SurName,
                    StaffId = x.Id
                }).ToListAsync();

            return staffs;
        }

        public async Task<string> UpdateStaffAsync(UpdateStaffDTO staffDTO)
        {
            using var context = new AppDbContext();
            var staff = await context.Staffs
                .Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == staffDTO.StaffId);
            if (staff == null) return null;
            foreach (var languageDTO in staffDTO.Languages)
            {
                var index = staff.Languages.FindIndex(x => x.LangCode == languageDTO.LangCode);
                if (index == -1) continue;
                staff.Languages[index].Name = languageDTO.Name ?? staff.Languages[index].Name;
                staff.Languages[index].SurName = languageDTO.SurName ?? staff.Languages[index].SurName;
                staff.Languages[index].Content = languageDTO.Content ?? staff.Languages[index].Content;
                staff.Languages[index].Profession = languageDTO.Profession ?? staff.Languages[index].Profession;
            }
            staff.ProfilePhotoUrl = staffDTO.ProfilePhotoUrl ?? staff.ProfilePhotoUrl;
            context.Staffs.Update(staff);
            context.StaffLanguages.UpdateRange(staff.Languages);
            await context.SaveChangesAsync();
            return staffDTO.ProfilePhotoUrl ?? "";
        }
    }
}
