using AutoMapper;
using TodoList.Data.Entities;
using TodoList.Shared.Todo;
using TodoList.Shared.User;

namespace TodoList.Server.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            TodoMapping();
            UsersMapping();
        }

        public void TodoMapping()
        {
            CreateMap<Todo, TodoResponse>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User!.Email));
            CreateMap<CreateTodoRequest, Todo>();
            CreateMap<UpdateTodoRequest, Todo>();
        }

        private void UsersMapping()
        {
            CreateMap<User, UserResponse>();
        }

    }
}
