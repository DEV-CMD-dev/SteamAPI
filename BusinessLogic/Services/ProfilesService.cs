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
                .OrderBy(t => t.UserId)
                .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<ProfileDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<ProfileDto> GetById(string userId)
        {
            var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

            if (profile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            return _mapper.Map<ProfileDto>(profile);
        }
        public async Task Patch(string userId, PatchProfileDto dto)
        {
            await Update(userId, dto);
        }

        public async Task Put(string userId, PutProfileDto dto)
        {
            await Update(userId, dto);
        }

        private async Task Update<TDto>(string userId, TDto dto)
        {
            var existingProfile = await _context.Profiles.FindAsync(userId);

            if (existingProfile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingProfile);

            await _context.SaveChangesAsync();
        }
    }
}
