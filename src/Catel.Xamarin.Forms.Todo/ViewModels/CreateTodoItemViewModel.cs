using System.Threading.Tasks;
using Catel.Services;
using Catel.Xamarin.Forms.Todo.Models;
using JetBrains.Annotations;

namespace Catel.Xamarin.Forms.Todo.ViewModels
{
	[UsedImplicitly(ImplicitUseTargetFlags.Members)]
	public class CreateTodoItemViewModel : EditTodoItemViewModel
	{
		#region .ctor
		public CreateTodoItemViewModel(INavigationService navigationService)
			: base(new TodoItem(), navigationService)
		{
		}
		#endregion

		#region Properties
		/// <summary>Gets the title of the view model.</summary>
		/// <value>The title.</value>
		public override string Title => "Create";
		#endregion

		#region Overrided
		protected override async Task<bool> SaveItemAsync()
		{
			await TodoStorage.AddItemAsync(TodoItem);
			return true;
		}
		#endregion
	}
}
