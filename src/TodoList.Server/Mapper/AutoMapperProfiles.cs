using AutoMapper;
using TodoList.Data.Entities;
using TodoList.Shared.Todo;

namespace TodoList.Server.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            TodoMapping();
        }
        public void TodoMapping()
        {
            CreateMap<Todo, TodoResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Email));
            CreateMap<CreateTodoRequest, Todo>();
            CreateMap<UpdateTodoRequest, Todo>();
        }
    }
}
