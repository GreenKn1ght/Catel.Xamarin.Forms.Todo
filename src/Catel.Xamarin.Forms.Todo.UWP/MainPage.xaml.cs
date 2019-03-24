namespace Catel.Xamarin.Forms.Todo.UWP
{
	public sealed partial class MainPage
	{
		#region .ctor
		public MainPage()
		{
			InitializeComponent();

			LoadApplication(new Todo.App());
		}
		#endregion
	}
}
