using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Catel.Xamarin.Forms.Todo.Behaviors
{
	public class EventToCommandBehavior : BehaviorBase<View>
	{
		#region Data
		#region Static
		public static readonly BindableProperty EventNameProperty =
			BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), propertyChanged: OnEventNameChanged);
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));
		public static readonly BindableProperty ConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));
		#endregion

		#region Fields
		private Delegate _eventHandler;
		#endregion
		#endregion

		#region Properties
		public ICommand Command
		{
			get => (ICommand) GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		public object CommandParameter
		{
			get => GetValue(CommandParameterProperty);
			set => SetValue(CommandParameterProperty, value);
		}

		public IValueConverter Converter
		{
			get => (IValueConverter) GetValue(ConverterProperty);
			set => SetValue(ConverterProperty, value);
		}

		public string EventName
		{
			get => (string) GetValue(EventNameProperty);
			set => SetValue(EventNameProperty, value);
		}
		#endregion

		#region Overrided
		/// <param name="bindable">The bindable object to which the behavior was attached.</param>
		/// <summary>
		/// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method
		/// on this object.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);
			if (EventName != null)
			{
				RegisterEvent(EventName);
			}
		}

		/// <param name="bindable">The bindable object from which the behavior was detached.</param>
		/// <summary>
		/// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the
		/// superclass.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnDetachingFrom(View bindable)
		{
			if (EventName != null)
			{
				UnregisterEvent(EventName);
			}

			base.OnDetachingFrom(bindable);
		}
		#endregion

		#region Private
		private EventInfo GetEventInfo(string name)
		{
			var eventInfo = AssociatedObject.GetType()
											.GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException();
			}

			return eventInfo;
		}

		private void OnEvent(object sender, object eventArgs)
		{
			if (Command == null)
			{
				return;
			}

			object resolvedParameter;
			if (CommandParameter != null)
			{
				resolvedParameter = CommandParameter;
			}
			else if (Converter != null)
			{
				resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
			}
			else
			{
				resolvedParameter = eventArgs;
			}

			if (Command.CanExecute(resolvedParameter))
			{
				Command.Execute(resolvedParameter);
			}
		}

		private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var behavior = (EventToCommandBehavior) bindable;
			if (behavior.AssociatedObject == null)
			{
				return;
			}

			if (oldValue != null)
			{
				behavior.UnregisterEvent((string) oldValue);
			}

			if (newValue != null)
			{
				behavior.RegisterEvent((string) newValue);
			}
		}

		private void RegisterEvent(string name)
		{
			var eventInfo = GetEventInfo(name);
			var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo()
														   .GetDeclaredMethod(nameof(OnEvent));
			_eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
			eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
		}

		private void UnregisterEvent(string name)
		{
			if (_eventHandler == null)
			{
				return;
			}

			GetEventInfo(name)
				.RemoveEventHandler(AssociatedObject, _eventHandler);
			_eventHandler = null;
		}
		#endregion
	}
}
