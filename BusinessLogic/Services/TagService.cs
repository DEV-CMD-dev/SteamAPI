using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public async Task<IEnumerable<TagDto>> GetAll()
        {
            var tags = await _context.Tags.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto> GetById(int id)
        {
            var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (tag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<TagDto>(tag);
        }

        public async Task Create(CreateTagDto dto)
        {
            var newTag = _mapper.Map<Tag>(dto);
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateTagDto dto)
        {
            var existingTag = await _context.Tags.FindAsync(id);

            if (existingTag == null)
                throw new HttpException($"Tag with Id {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingTag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}