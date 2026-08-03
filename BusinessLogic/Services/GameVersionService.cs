using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLogic.Services
{
    public class GameVersionService : IGameVersionService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public GameVersionService(
           SteamDbContext context,
           IMapper mapper,
           IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<GameVersionDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.GameVersions
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<GameVersionDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<GameVersionDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<GameVersionDto> GetById(int id)
        {
            var gameVersion = await _context.GameVersions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (gameVersion == null)
                throw new HttpException($"Game version with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<GameVersionDto>(gameVersion);
        }

        public async Task Create(string userId, CreateGameVersionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Version))
                throw new HttpException("Game version name can not be empty", HttpStatusCode.BadRequest);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(dto.GameId);

            var newGameVersion = _mapper.Map<GameVersion>(dto);
            _context.GameVersions.Add(newGameVersion);
            await _context.SaveChangesAsync();
        }

        public async Task Patch(int id, string userId, PatchGameVersionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Version) && string.IsNullOrWhiteSpace(dto.PatchNotes))
                throw new HttpException("Game version name and patch notes can not be empty", HttpStatusCode.BadRequest);

            await Update(userId, id, dto);
        }

        public async Task Put(int id, string userId, PutGameVersionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Version) || string.IsNullOrEmpty(dto.PatchNotes))
                throw new HttpException("Game version name or patch notes can not be empty", HttpStatusCode.BadRequest);

            await Update(userId, id, dto);
        }

        public async Task Delete(string userId, int id)
        {
            var gameVersion = await _context.GameVersions.FindAsync(id);

            if (gameVersion == null)
                throw new HttpException($"Game version with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(gameVersion.GameId);

            _context.GameVersions.Remove(gameVersion);
            await _context.SaveChangesAsync();
        }

        private async Task Update<TDto>(string userId, int id, TDto dto)
        {
            var existingGameVersion = await _context.GameVersions.FindAsync(id);

            if (existingGameVersion == null)
                throw new HttpException($"Game version with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(existingGameVersion.GameId);

            _mapper.Map(dto, existingGameVersion);

            await _context.SaveChangesAsync();
        }
    }
}
