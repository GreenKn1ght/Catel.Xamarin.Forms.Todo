using System;
using Xamarin.Forms;

namespace Catel.Xamarin.Forms.Todo.Behaviors
{
	public class BehaviorBase<T> : Behavior<T> where T : BindableObject
	{
		#region Properties
		public T AssociatedObject
		{
			get;
			private set;
		}
		#endregion

		#region Overrided
		/// <param name="bindable">The bindable object to which the behavior was attached.</param>
		/// <summary>
		/// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method
		/// on this object.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAttachedTo(T bindable)
		{
			base.OnAttachedTo(bindable);
			AssociatedObject = bindable;

			if (bindable.BindingContext != null)
			{
				BindingContext = bindable.BindingContext;
			}

			bindable.BindingContextChanged += OnBindingContextChanged;
		}

		/// <summary>Override this method to execute an action when the BindingContext changes.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			BindingContext = AssociatedObject.BindingContext;
		}

		/// <param name="bindable">The bindable object from which the behavior was detached.</param>
		/// <summary>
		/// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the
		/// superclass.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnDetachingFrom(T bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.BindingContextChanged -= OnBindingContextChanged;
			AssociatedObject = null;
		}
		#endregion

		#region Private
		private void OnBindingContextChanged(object sender, EventArgs e)
		{
			OnBindingContextChanged();
		}
		#endregion
	}
}
