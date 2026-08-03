using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public ProfileService(SteamDbContext context, IMapper mapper, IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<PaginatedList<ProfileDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Profiles
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<ProfileDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
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
