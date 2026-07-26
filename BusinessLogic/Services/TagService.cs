using AutoMapper;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Helpers;
using BusinessLogic.Configurations;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;
        
        public TagService(
            SteamDbContext context,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<PaginatedList<TagDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Tags
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<TagDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
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

        public async Task Patch(int id, PatchTagDto dto)
        {
            await Update(id, dto);
        }

        public async Task Put(int id, PutTagDto dto)
        {
            await Update(id, dto);
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        private async Task Update<TDto>(int id, TDto dto)
        {
            var existingTag = await _context.Tags.FindAsync(id);

            if (existingTag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingTag);

            await _context.SaveChangesAsync();
        }
    }
}