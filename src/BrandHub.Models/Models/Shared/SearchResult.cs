using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Shared
{
    public class SearchResult<T>
    {
        public SearchResult()
        {
            Result = new List<T>();
        }
        public List<T> Result { get; set; }
        public int Total { get; set; }
    }
}
