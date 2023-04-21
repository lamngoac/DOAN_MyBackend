using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.WebAdmin.Extensions.Pager
{
    public interface IPagedList : IList, ICollection, IEnumerable
    {
        bool HasNextPage { get; }

        bool HasPreviousPage { get; }

        bool IsFirstPage { get; }

        bool IsLastPage { get; }

        int PageCount { get; }

        int PageIndex { get; }

        int PageNumber { get; }

        int PageSize { get; }

        int TotalItemCount { get; }
    }
}