using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Hearthstone_Deckbuilder.Interface.HearthstoneAPI.Controller
{
    internal abstract class ConsecutiveByIndividualDtoProfile<TBo, TDto>
        where TDto : HearthstoneAPIController
        where TBo : HearthstoneAPIControllerBO
    {
        protected void Configure()
        {
            Mapper.CreateMap<HearthstoneAPIControllerBO, HearthstoneAPIController>()
                .ForMember(c => c.CardAttackValue, f => f.MapFrom(src => src.Attack))
                .ForMember(c => c.CardCostValue, f => f.MapFrom(src => src.Cost))
                .ForMember(c => c.CardHealthValue, f => f.MapFrom(src => src.Health))
                .ForMember(c => c.CardIdValue, f => f.MapFrom(src => src.CardId))
                .ForMember(c => c.CardNameValue, f => f.MapFrom(src => src.Name))
                .ForMember(c => c.CardQualityValue, f => f.MapFrom(src => src.Quality))
                .ForMember(c => c.CardTypeValue, f => f.MapFrom(src => src.Type))
                .ForMember(c => c.CardSetValue, f => f.MapFrom(src => src.Set))
                .ForMember(c => c.ClassValue, f => f.MapFrom(src => src.Class));
        }
    }
}
