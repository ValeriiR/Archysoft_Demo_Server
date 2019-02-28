

namespace D1.Model.Common
{
    public class BaseFilter
    {
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string GetTrimSearch()
        {
            return Search.Trim();
        }
    }
}
