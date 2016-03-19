using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
