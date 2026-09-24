using AutoMapper;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLogic.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public LibraryService(
           SteamDbContext context,
           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<LibraryGameDto>> GetUserGames(string userId)
        {
            var query = _context.UserGames
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .Select(u => new LibraryGameDto
                {
                    Id = u.GameId,
                    Title = u.Game.Title,
                    IconUrl = u.Game.IconUrl,
                    CoverImageVertical = u.Game.CoverImageVertical ?? string.Empty,
                    LastPlayDate = u.LastPlayDate,
                    PlayTimeMinutes = u.PlayTimeMinutes,
                    IsInstalled = u.IsInstalled
                })
                .OrderBy(t => t.Title);

            return await query.ToListAsync();
        }

        public async Task<FullLibraryGameDto> GetById(int id)
        {
            var game = await _context.UserGames
                .AsNoTracking()
                .Where(u => u.GameId == id)
                .Select(g => new FullLibraryGameDto
                {
                    Id = g.GameId,
                    IconUrl = g.Game.IconUrl,
                    CoverImageHorizontal = g.Game.CoverImageHorizontal,
                    CoverImageVertical = g.Game.CoverImageVertical,
                    LastPlayDate = g.LastPlayDate,
                    PlayTimeMinutes = g.PlayTimeMinutes,
                    IsInstalled = g.IsInstalled,
                    Title = g.Game.Title,
                    ReleaseDate = g.Game.ReleaseDate,
                    DeveloperId = g.Game.DeveloperId,
                    PurchasedAt = g.PurchasedAt
                })
                .FirstOrDefaultAsync();

            if (game == null)
                throw new HttpException($"Game with ID {id} not found", HttpStatusCode.NotFound);

            return game;
        }

    }
}
