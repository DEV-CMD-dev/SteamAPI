using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services
{
    public class GamesService : IGamesService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        public GamesService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<GameDto>> GetAll()
        {
            var gamesFromDb = await _context.Game.ToListAsync();
            var gameDtos = _mapper.Map<IList<GameDto>>(gamesFromDb);
            return gameDtos;
        }
    }
}
