using AutoMapper;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        public AchievementService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<AchievementDto>> GetAll()
        {
            var achievementFromDb = await _context.Achievement.ToListAsync();
            var achievementDtos = _mapper.Map<IList<AchievementDto>>(achievementFromDb);
            return achievementDtos;
        }
        public async Task<AchievementDto?> Get(int id)
        {
            var achievementFromDb = await _context.Achievement.FindAsync(id);
            if (achievementFromDb == null)
            {
                return null;
            }
            var achievementDto = _mapper.Map<AchievementDto>(achievementFromDb);
            return achievementDto;
        }
        public async Task<AchievementDto> Create(CreateAchivementDto model)
        {
            var achievementEntity = _mapper.Map<Achievement>(model);
            _context.Achievement.Add(achievementEntity);
            await _context.SaveChangesAsync();
            var achievementDto = _mapper.Map<AchievementDto>(achievementEntity);
            return achievementDto;
        }
        public async Task Update(AchievementDto model)
        {
            var existingAchievement = await _context.Achievement.FindAsync(model.Id);
            if (existingAchievement == null)
                throw new KeyNotFoundException($"Ачівка з Id {model.Id} не знайдена.");
            _mapper.Map(model, existingAchievement);
            await _context.SaveChangesAsync();

        }
        public async Task Delete(int id)
        {
            var achievementFromDb = await _context.Achievement.FindAsync(id);
            if (achievementFromDb == null)
            {
                throw new Exception($"Game with ID {id} not found.");
            }
            _context.Achievement.Remove(achievementFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
