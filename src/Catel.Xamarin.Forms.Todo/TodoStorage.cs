using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Xamarin.Forms.Todo.Models;

namespace Catel.Xamarin.Forms.Todo
{
	public static class TodoStorage
	{
		#region Data
		#region Static
		private static readonly List<TodoItem> TodoItems = new List<TodoItem>
		{
			new TodoItem
			{
				Name = "Call Jenny",
				Description = "Call Jenny"
			},
			new TodoItem
			{
				Name = "Buy food",
				Description = "Buy food in a store"
			}
		};
		#endregion
		#endregion

		#region Public
		public static Task AddItemAsync(TodoItem todoItem)
		{
			TodoItems.Add(todoItem);
			return Task.CompletedTask;
		}

		public static Task<IEnumerable<TodoItem>> GetItemsAsync()
		{
			return Task.FromResult<IEnumerable<TodoItem>>(TodoItems);
		}

		public static Task SaveItemAsync(TodoItem todoItem) => Task.CompletedTask;
		#endregion
	}
}
