using MongoDB.Driver;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopChallenge.Pagination
{
    public static class PaginationExtension
    {
        public async static Task<Page<T>> GetPagedAsync<T>(this IMongoCollection<T> collection, PageModel page, FilterDefinition<T> filter = null)
        {
            if (filter is null)
                filter = Builders<T>.Filter.Empty;
            if (page is null)
                throw new ArgumentNullException(nameof(page));
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            long collectionCount = await collection.CountDocumentsAsync(filter).ConfigureAwait(false);
            page.DataCount = collectionCount;
            var skip = page.PageNumber * page.PageSize;
            var result = collection.Find(filter);
            var data = await result.Skip(skip).Limit(page.PageSize).ToListAsync<T>().ConfigureAwait(false);

            return new Page<T>(page, data);
        }
        public static Page<T> GetPage<T>(this IEnumerable<T> data, PageModel page)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));
            if (page is null)
                throw new ArgumentNullException(nameof(page));

            page.DataCount = data.Count();
            var skip = page.PageNumber * page.PageSize;
            data = data.Skip(skip).Take(page.PageSize);

            return new Page<T>(page, data);
        }


        public static Page<T> GetEmptyPage<T>()
        {
            return new Page<T>(default, default, Enumerable.Empty<T>());
        }
    }
}
