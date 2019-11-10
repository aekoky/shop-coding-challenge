using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Pagination
{
    public class PageModel
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public long DataCount { get; set; }

        public PageModel(int page, int pageSize, long? count = default)
        {
            PageNumber = page;
            PageSize = pageSize;
            DataCount = count ?? 0;
        }

        public override string ToString()
        {
            return $"{nameof(PageNumber)} : {PageNumber}" +
                   $"{nameof(PageSize)} : {PageSize}" +
                   $"{nameof(DataCount)} : {DataCount}";
        }
    }
}
