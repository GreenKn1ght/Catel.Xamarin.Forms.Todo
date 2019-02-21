using System.Threading.Tasks;
using System.Windows.Input;
using Catel.MVVM;
using Catel.Services;
using Catel.Xamarin.Forms.Todo.Models;
using JetBrains.Annotations;

namespace Catel.Xamarin.Forms.Todo.ViewModels
{
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public class EditTodoItemViewModel : ViewModelBase
	{
		#region Data
		#region Fields
		[NotNull]
		private readonly INavigationService _navigationService;
		#endregion
		#endregion

		#region .ctor
		public EditTodoItemViewModel([NotNull] TodoItem todoItem, [NotNull] INavigationService navigationService)
		{
			_navigationService = navigationService;
			TodoItem = todoItem;
			SaveCommand = new TaskCommand(async () =>
			{
				if (await this.SaveAndCloseViewModelAsync())
				{
					_navigationService.GoBack();
				}
			});
		}
		#endregion

		#region Properties
		[ViewModelToModel]
		public string Description
		{
			get;
			set;
		}

		[ViewModelToModel]
		public string Name
		{
			get;
			set;
		}

		public ICommand SaveCommand
		{
			get;
		}

		[Model]
		protected TodoItem TodoItem
		{
			get;
			private set;
		}

		/// <summary>Gets the title of the view model.</summary>
		/// <value>The title.</value>
		public override string Title => "Edit";
		#endregion

		#region Overridable
		protected virtual async Task<bool> SaveItemAsync()
		{
			await TodoStorage.SaveItemAsync(TodoItem);
			return true;
		}
		#endregion

		#region Overrided
		/// <summary>Saves the data.</summary>
		/// <returns>
		/// <c>true</c> if successful; otherwise <c>false</c>.
		/// </returns>
		protected sealed override async Task<bool> SaveAsync() => await base.SaveAsync() && await SaveItemAsync();
		#endregion
	}
}
