using System;
using Catel.Logging;
using Catel.Reflection;
using Catel.Xamarin.Forms.Todo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Catel.Xamarin.Forms.Todo
{
	public partial class App
	{
		#region .ctor
		static App()
		{
			LogManager.AddDebugListener();
		}

		public App()
		{
			InitializeComponent();
			var loadedAssemblies = AppDomain.CurrentDomain.GetLoadedAssemblies();
			MainPage = new NavigationPage(new TodoListView());
		}
		#endregion
	}
}
