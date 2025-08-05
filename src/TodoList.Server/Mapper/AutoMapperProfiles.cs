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
            CreateMap<Todo, TodoResponse>();
            CreateMap<CreateTodoRequest, Todo>();
            CreateMap<UpdateTodoRequest, Todo>();
        }
    }
}
