using KaylaaShop.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items,int count , int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,int pageIndex, int pageSize)
        {
            var src_count = source.Count();

            int count=0;
            List<T> items = new List<T>(); 
            if (src_count == 1)
            {
                count = source.Count();
                items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                 count = await source.CountAsync();
                 items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            }

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
