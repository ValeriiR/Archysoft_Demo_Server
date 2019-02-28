using System.Collections.Generic;

namespace D1.Model.Common
{
    public class SearchResult<T> 
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Total { get; set; }
    }
}
