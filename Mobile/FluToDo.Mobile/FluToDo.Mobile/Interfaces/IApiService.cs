using FluToDo.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluToDo.Mobile.Interfaces
{
    public interface IApiService
    {
        Task<IEnumerable<ToDoItem>> GetItems();
        Task UpdateItem(ToDoItem item);
        Task DeleteItem(string itemKey);
    }

}
