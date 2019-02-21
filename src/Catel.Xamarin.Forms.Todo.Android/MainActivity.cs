using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Catel.Logging;
using Xamarin.Forms.Platform.Android;
using Log = Android.Util.Log;

namespace Catel.Xamarin.Forms.Todo.Droid
{
	[Activity(Label = "XamarinVM",
		Icon = "@mipmap/icon",
		Theme = "@style/MainTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsAppCompatActivity
	{
		#region Overrided
		protected override void OnCreate(Bundle savedInstanceState)
		{
			LogManager.AddListener(new LogCatListener());
			LogManager.AddListener(new ToastListener(this));

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
		}
		#endregion

		#region Nested types
		#region Type: LogCatListener
		private class LogCatListener : LogListenerBase
		{
			#region Data
			#region Consts
			private const string Tag = "Catel";
			#endregion
			#endregion

			#region Overrided
			protected override void Debug(ILog log, string message, object extraData, LogData logData, DateTime time)
			{
				Log.Debug(Tag, message);
			}

			protected override void Error(ILog log, string message, object extraData, LogData logData, DateTime time)
			{
				Log.Error(Tag, message);
			}

			protected override void Info(ILog log, string message, object extraData, LogData logData, DateTime time)
			{
				Log.Info(Tag, message);
			}

			protected override void Warning(ILog log, string message, object extraData, LogData logData, DateTime time)
			{
				Log.Warn(Tag, message);
			}
			#endregion
		}
		#endregion

		#region Type: ToastListener
		private class ToastListener : LogListenerBase
		{
			#region Data
			#region Fields
			private readonly Context _context;
			#endregion
			#endregion

			#region .ctor
			public ToastListener(Context context)
			{
				_context = context;
			}
			#endregion

			#region Overrided
			/// <summary>
			/// Called when a <see cref="F:Catel.Logging.LogEvent.Debug" /> message is written to the log.
			/// </summary>
			/// <param name="log">The log.</param>
			/// <param name="message">The message.</param>
			/// <param name="extraData">The additional data.</param>
			/// <param name="logData">The log data.</param>
			/// <param name="time">The time.</param>
			protected override void Debug(ILog log, string message, object extraData, LogData logData, DateTime time)
			{
				if (log.Name == "Catel.MVVM.Providers.LogicBase" && message.Contains("is being loaded") || message.Contains("is being unloaded"))
				{
					Toast.MakeText(_context, message, ToastLength.Short)
						 .Show();
				}
			}
			#endregion
		}
		#endregion
		#endregion
	}
}
