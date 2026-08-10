using BusinessLogic.Extensions.SearchFilters;
using DataAccess.Data.Entities;
using System.Net;

namespace BusinessLogic.Extensions
{
    public static class GameQueryExtension
    {
        public static IQueryable<Game> ApplyFilters(
            this IQueryable<Game> query,
            GameParameters gameParams)
        {
            if (gameParams.MinPrice.HasValue)
                query = query.Where(g => g.Price * (1 - g.Discount / 100m) >= gameParams.MinPrice.Value);

            if (gameParams.MaxPrice.HasValue)
                query = query.Where(g => g.Price * (1 - g.Discount / 100m) <= gameParams.MaxPrice.Value);

            if (gameParams.OnSaleOnly == true)
                query = query.Where(g => g.Discount > 0);

            if (gameParams.TagIds != null && gameParams.TagIds.Count > 0)
            {
                query = query.Where(g => g.Tags.Any(t => gameParams.TagIds.Contains(t.Id)));
            }

            if (!string.IsNullOrWhiteSpace(gameParams.SearchTerm))
            {
                var trimmedTerm = gameParams.SearchTerm.Trim();
                if (trimmedTerm.Length < 3)
                    throw new HttpException("Search term length must be at least 3 characters", HttpStatusCode.BadRequest);

                var searchTerms = gameParams.SearchTerm.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                
                query = query.Where(g => searchTerms.Any(term => g.Title.Contains(term)));
            }

            return query;
        }
    }
}
