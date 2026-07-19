using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
            var tags = await _context.Profiles.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ProfileDto>>(tags);
        }

        public async Task<ProfileDto> GetById(int id)
        {
            var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (profile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task Create(ProfileDto dto)
        {
            var newProfile = _mapper.Map<Profile>(dto);
            _context.Profiles.Add(newProfile);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, ProfileDto dto)
        {
            var existingProfile = await _context.Profiles.FindAsync(id);

            if (existingProfile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingProfile);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
                throw new HttpException($"Profile with ID {id} not found", HttpStatusCode.NotFound);

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}