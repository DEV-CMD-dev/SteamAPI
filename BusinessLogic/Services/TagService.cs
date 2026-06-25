using AutoMapper;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        public TagService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<TagDto>> GetAll()
        {
            var tagsFromDb = await _context.Tags.ToListAsync();
            var tagDtos = _mapper.Map<IList<TagDto>>(tagsFromDb);
            return tagDtos;
        }
        public async Task<TagDto?> Get(int id)
        {
            var tagFromDb = await _context.Tags.FindAsync(id);
            if (tagFromDb == null)
            {
                return null;
            }
            var tagDto = _mapper.Map<TagDto>(tagFromDb);
            return tagDto;
        }
        public async Task<TagDto> Create(CreateTagDto model)
        {
            var tagEntity = _mapper.Map<Tag>(model);
            _context.Tags.Add(tagEntity);
            await _context.SaveChangesAsync();
            var tagDto = _mapper.Map<TagDto>(tagEntity);
            return tagDto;
        }
        public async Task Update(TagDto model)
        {
            var existingTag = await _context.Tags.FindAsync(model.Id);
            if (existingTag == null)
                throw new KeyNotFoundException($"Гра з Id {model.Id} не знайдена.");
            _mapper.Map(model, existingTag);
            await _context.SaveChangesAsync();

        }
        public async Task Delete(int id)
        {
            var tagFromDb = await _context.Tags.FindAsync(id);
            if (tagFromDb == null)
            {
                throw new Exception($"Game with ID {id} not found.");
            }
            _context.Tags.Remove(tagFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
