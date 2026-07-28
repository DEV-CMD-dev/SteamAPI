using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

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

        public async Task Create(string userId,CreateTagDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId).EnsureModerator();

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new HttpException("Tag name can not be empty", HttpStatusCode.BadRequest);

            var newTag = _mapper.Map<Tag>(dto);
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();
        }

        public async Task Patch(string userId, int id, PatchTagDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) && string.IsNullOrWhiteSpace(dto.Picture))
                throw new HttpException("Tag name and picture can not be empty", HttpStatusCode.BadRequest);

            await Update(userId,id, dto);
        }

        public async Task Put(string userId, int id, PutTagDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new HttpException("Tag name can not be empty", HttpStatusCode.BadRequest);

            await Update(userId,id, dto);
        }

        public async Task Delete(string userId, int id)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId).EnsureModerator();

            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        private async Task Update<TDto>(string userId,int id, TDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId).EnsureModerator();

            var existingTag = await _context.Tags.FindAsync(id);

            if (existingTag == null)
                throw new HttpException($"Tag with ID {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingTag);

            await _context.SaveChangesAsync();
        }
    }
}