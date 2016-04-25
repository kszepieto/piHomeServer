using System.Collections.Generic;
using AutoMapper;

namespace piHome.WebHost.Infrastructure.Mapping
{
    public static class IMapperExtentions
    {
        public static List<D> MapList<S, D>(this IMapper mapper, List<S> source)
        {
            return mapper.Map<List<S>, List<D>>(source);
        } 
    }
}
