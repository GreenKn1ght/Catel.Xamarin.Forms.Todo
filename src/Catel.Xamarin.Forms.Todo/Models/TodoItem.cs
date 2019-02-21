using Catel.Data;

namespace Catel.Xamarin.Forms.Todo.Models
{
	public class TodoItem : ModelBase
	{
		#region Properties
		public string Description
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
		#endregion
	}
}
