using System.Net;
using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Profile = DataAccess.Data.Entities.Profile;

namespace BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;

        public ProfileService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfileDto>> GetAll()
        {
            var profiles = await _context.Profiles.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
        }

        public async Task<ProfileDto> GetById(int id)
        {
            var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (profile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<ProfileDto>(profile);
        }
        public async Task Patch(int id, PatchProfileDto dto)
        {
            await Update(id, dto);
        }

        public async Task Put(int id, PutProfileDto dto)
        {
            await Update(id, dto);
        }

        private async Task Update<TDto>(int id, TDto dto)
        {
            var existingProfile = await _context.Profiles.FindAsync(id);

            if (existingProfile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingProfile);

            await _context.SaveChangesAsync();
        }
    }
}
