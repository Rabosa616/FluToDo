using FluToDo.Api.Core.Models;
using System.Collections.Generic;

namespace FluToDo.Api.Core.Interfaces
{
	public interface ITodoRepository
	{
		void Add(TodoItem item);
		IEnumerable<TodoItem> GetAll();
		TodoItem Find(string key);
		TodoItem Remove(string key);
		void Update(TodoItem item);
	}
}