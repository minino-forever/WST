using System.Collections.Generic;

namespace WST.Admin.Models.ViewModels
{
    public class ListDto<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentItem { get; set; }
    }
}