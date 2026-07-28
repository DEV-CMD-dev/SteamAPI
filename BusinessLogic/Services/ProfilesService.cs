using System.Net;
using AutoMapper;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;

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
        public async Task Patch(int id, string userId, PatchProfileDto dto)
        {
            await Update(id, userId, dto);
        }

        public async Task Put(int id, string userId, PutProfileDto dto)
        {
            await Update(id, userId, dto);
        }

        private async Task Update<TDto>(int id, string userId, TDto dto)
        {
            var existingProfile = await _context.Profiles.FindAsync(id);

            if (existingProfile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            if (existingProfile.UserId != userId)
                throw new HttpException("You are not authorized to update this profile", HttpStatusCode.Forbidden);

            _mapper.Map(dto, existingProfile);

            await _context.SaveChangesAsync();
        }
    }
}
