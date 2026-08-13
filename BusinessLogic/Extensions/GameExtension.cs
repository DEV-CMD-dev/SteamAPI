using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Extensions
{
    public static class GameExtension
    {
        public static async Task SetTagsAsync(this Game game ,SteamDbContext context, IEnumerable<int> tagIds)
        {
            var ids = tagIds.ToList();

            var tags = await context.Tags
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            if (tags.Count != ids.Count)
                throw new HttpException("One or more provided Tag IDs do not exist", HttpStatusCode.BadRequest);

            game.Tags ??= new List<Tag>();
            game.Tags.Clear();

            foreach (var tag in tags)
            {
                game.Tags.Add(tag);
            }
                
        }
    }
}
