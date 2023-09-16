using AutoMapper;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;

namespace MyToDoApi.Extensions
{
    public class AutoMapperProfile : MapperConfigurationExpression
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
