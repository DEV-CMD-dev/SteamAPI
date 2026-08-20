using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IFriendshipService
    {
        Task<PaginatedList<ProfileDto>> GetFriends(string userId, int pageNumber, int pageSize);
        Task<PaginatedList<ProfileDto>> GetIncomingRequests(string userId, int pageNumber, int pageSize);
        Task SendFriendRequest(string userId, string friendId);
        Task AcceptFriendRequest(string userId, string friendId);
        Task DeclineFriendRequest(string userId, string friendId);
        Task RemoveFriend(string userId, string friendId);
    }
}
