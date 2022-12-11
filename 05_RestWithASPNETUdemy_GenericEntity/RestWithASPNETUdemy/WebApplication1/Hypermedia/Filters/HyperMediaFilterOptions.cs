using System.Collections.Generic;
using WebApplication1.Hypermedia.Abstract;

namespace WebApplication1.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentReponseEnricherList { get; set; } = new List<IResponseEnricher>();

    }
}
