using AutoMapper;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Enums;
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
            var tags = await _context.Tags.ToListAsync();
            var tagsDtos = _mapper.Map<IList<TagDto>>(tags);

            return tagsDtos;
        }

        public async Task<TagDto> Get(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new Exception($"Tag with ID {id} not found.");

            var tagDto = _mapper.Map<TagDto>(tag);

            return tagDto;
        }

        public async Task Create(CreateTagDto dto)
        {
            var newTag = _mapper.Map<Tag>(dto);

            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TagDto dto)
        {
            var existingTag = await _context.Tags.FindAsync(dto.Id);

            if (existingTag == null)
                throw new Exception($"Tag with Id {dto.Id} not found.");

            _mapper.Map(dto, existingTag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new Exception($"Game with ID {id} not found.");

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}
