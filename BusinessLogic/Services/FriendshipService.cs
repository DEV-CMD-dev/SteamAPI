using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public FriendshipService(SteamDbContext context, IMapper mapper, IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<ProfileDto>> GetFriends(string userId, int pageNumber, int pageSize)
        {
            var query = _context.Friendships
                .AsNoTracking()
                .Where(f => (f.UserId == userId || f.FriendId == userId) && f.Status == DataAccess.Enums.FriendshipStatus.Accepted)
                .Select(f => f.UserId == userId ? f.Friend.Profile : f.User.Profile)
                .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<ProfileDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }
        public async Task<PaginatedList<ProfileDto>> GetIncomingRequests(string userId, int pageNumber, int pageSize)
        {
            var query = _context.Friendships
                 .AsNoTracking()
                 .Where(f => f.FriendId == userId && f.Status == DataAccess.Enums.FriendshipStatus.Pending)
                 .Select(f => f.User.Profile)
                 .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<ProfileDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

     
        public async Task SendFriendRequest(string userId, string userName)
        {
            var friend = await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
            if (friend == null)
                throw new HttpException("User not found", System.Net.HttpStatusCode.NotFound);
            if (userId == friend.Id)
                throw new HttpException("You cannot send a friend request to yourself", System.Net.HttpStatusCode.BadRequest);
            var existingFriendship = await _context.Friendships
                .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friend.Id) || (f.UserId == friend.Id && f.FriendId == userId));
            if (existingFriendship != null)
            {
                if (existingFriendship.Status == DataAccess.Enums.FriendshipStatus.Pending)
                    throw new HttpException("A friend request is already pending", System.Net.HttpStatusCode.BadRequest);
                else if (existingFriendship.Status == DataAccess.Enums.FriendshipStatus.Accepted)
                    throw new HttpException("You are already friends", System.Net.HttpStatusCode.BadRequest);
            }
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friend.Id,
                Status = DataAccess.Enums.FriendshipStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptFriendRequest(string userId, string friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId && f.Status == DataAccess.Enums.FriendshipStatus.Pending);
            if (friendship == null)
                throw new HttpException("Friend request not found", System.Net.HttpStatusCode.NotFound);
            friendship.Status = DataAccess.Enums.FriendshipStatus.Accepted;
            await _context.SaveChangesAsync();
        }

        public async Task DeclineFriendRequest(string userId, string friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId && f.Status == DataAccess.Enums.FriendshipStatus.Pending);
            if (friendship == null)
                throw new HttpException("Friend request not found", System.Net.HttpStatusCode.NotFound);
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriend(string userId, string friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
            if (friendship == null)
                throw new HttpException("Friendship not found", System.Net.HttpStatusCode.NotFound);
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }
    }
}
