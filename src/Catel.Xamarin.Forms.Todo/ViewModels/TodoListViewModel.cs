using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Catel.Collections;
using Catel.MVVM;
using Catel.Services;
using Catel.Xamarin.Forms.Todo.Models;
using JetBrains.Annotations;

namespace Catel.Xamarin.Forms.Todo.ViewModels
{
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
	public class TodoListViewModel : ViewModelBase
	{
		#region Data
		#region Fields
		private readonly FastObservableCollection<TodoItem> _todoItems;
		#endregion
		#endregion

		#region .ctor
		public TodoListViewModel(INavigationService navigationService)
		{
			_todoItems = new FastObservableCollection<TodoItem>();
			TapOnItemCommand = new Command<TodoItem>(item =>
			{
				navigationService.Navigate<TodoItemViewModel>(item);
			});
			CreateTodoItemCommand = new Command(() => navigationService.Navigate<CreateTodoItemViewModel>());
		}
		#endregion

		#region Properties
		public ICommand CreateTodoItemCommand
		{
			get;
		}

		public ICommand TapOnItemCommand
		{
			get;
		}

		/// <summary>Gets the title of the view model.</summary>
		/// <value>The title.</value>
		public override string Title => "Todo";

		public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;
		#endregion

		#region Overrided
		protected override async Task InitializeAsync()
		{
			await base.InitializeAsync();
			var items = await TodoStorage.GetItemsAsync();
			using (_todoItems.SuspendChangeNotifications())
			{
				_todoItems.ReplaceRange(items);
			}
		}
		#endregion
	}
}
