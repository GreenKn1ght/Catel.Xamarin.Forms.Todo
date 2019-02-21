using System.Windows.Input;
using Catel.MVVM;
using Catel.Services;
using Catel.Xamarin.Forms.Todo.Models;
using JetBrains.Annotations;

namespace Catel.Xamarin.Forms.Todo.ViewModels
{
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public class TodoItemViewModel : ViewModelBase
	{
		#region Data
		#region Fields
		[NotNull]
		private readonly INavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public TodoItemViewModel([NotNull] TodoItem todoItem, [NotNull] INavigationService navigationService)
		{
			_navigationService = navigationService;
			TodoItem = todoItem;
			EditTodoItemCommand = new Command(() => navigationService.Navigate<EditTodoItemViewModel>(todoItem));
		}
		#endregion

		#region Properties
		[ViewModelToModel]
		public string Description
		{
			get;
			private set;
		}

		public ICommand EditTodoItemCommand
		{
			get;
		}

		[ViewModelToModel]
		public string Name
		{
			get;
			private set;
		}

		[Model]
		private TodoItem TodoItem
		{
			get;
			set;
		}

		/// <summary>Gets the title of the view model.</summary>
		/// <value>The title.</value>
		public override string Title => Name;
		#endregion
	}
}
