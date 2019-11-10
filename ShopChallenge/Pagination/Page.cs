using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Pagination
{
    public class Page<T>
    {
        public int PageNumber { get; }
        public long DataCount { get; }
        public IEnumerable<T> Data { get; }
        public int PageSize { get; }

        public Page(int? pageNumber, long? dataCount, IEnumerable<T> data)
        {
            PageNumber = pageNumber ?? default;
            DataCount = dataCount ?? default;
            Data = data;
            PageSize = Data?.Count() ?? 0;
        }

        public Page(PageModel page, IEnumerable<T> data) : this(page?.PageNumber, page?.DataCount, data)
        {

        }
        public override string ToString()
        {
            return $"{nameof(PageNumber)}: {PageNumber}" +
                   $"{nameof(DataCount)}: {DataCount}" +
                   $"{nameof(PageSize)}: {PageSize}";
        }
    }
}
