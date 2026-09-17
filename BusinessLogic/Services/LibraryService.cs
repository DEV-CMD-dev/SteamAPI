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
                    IconUrl = u.Game.IconUrl
                })
                .OrderBy(t => t.Title);

            return await query.ToListAsync();
        }
    }
}
