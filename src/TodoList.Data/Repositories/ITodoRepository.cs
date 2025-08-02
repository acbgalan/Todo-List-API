using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Entities;

namespace TodoList.Data.Repositories
{
    public interface ITodoRepository:IRepositoryAsync<Todo>
    {
    }
}
