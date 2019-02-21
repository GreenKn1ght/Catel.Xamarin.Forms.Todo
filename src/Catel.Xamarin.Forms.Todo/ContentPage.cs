// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentPage.cs" company="Catel development team">
//   Copyright (c) 2008 - 2015 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Catel.MVVM;
using Catel.MVVM.Providers;
using Catel.MVVM.Views;

namespace Catel.Xamarin.Forms.Todo
{
	/// <summary>
	/// The content page.
	/// </summary>
	public class ContentPage : global::Xamarin.Forms.ContentPage, IView
	{
		#region Delegates and events
		/// <summary>
		/// Occurs when the back button is pressed.
		/// </summary>
		public event EventHandler<EventArgs> BackButtonPressed;

		/// <summary>
		/// Occurs when the data context has changed.
		/// </summary>
		public event EventHandler<DataContextChangedEventArgs> DataContextChanged;

		/// <summary>
		/// Occurs when the view is loaded.
		/// </summary>
		public event EventHandler<EventArgs> Loaded;

		/// <summary>
		/// Occurs when the view is unloaded.
		/// </summary>
		public event EventHandler<EventArgs> Unloaded;

		/// <summary>
		/// Occurs when the view model has changed.
		/// </summary>
		public event EventHandler<EventArgs> ViewModelChanged;

		/// <summary>
		/// Occurs when a property on the <see cref="ViewModel" /> has changed.
		/// </summary>
		public event EventHandler<PropertyChangedEventArgs> ViewModelPropertyChanged;
		#endregion

		#region Data
		#region Fields
		/// <summary>
		/// The old binding context reference.
		/// </summary>
		private object _oldbindingContext;
		/// <summary>
		/// The user control logic.
		/// </summary>
		private readonly UserControlLogic _userControlLogic;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentPage" /> class.
		/// </summary>
		public ContentPage()
			: this(null)
		{
			BindingContextChanged += OnBindingContextChanged;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentPage" /> class.
		/// </summary>
		/// <param name="viewModel">The view model</param>
		public ContentPage(IViewModel viewModel)
		{
			_userControlLogic = new UserControlLogic(this, null, viewModel);

			_userControlLogic.TargetViewPropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);

			_userControlLogic.ViewModelClosedAsync += OnViewModelClosedAsync;

			_userControlLogic.ViewModelChanged += (sender, args) =>
			{
				if (!ObjectHelper.AreEqual(BindingContext, _userControlLogic.ViewModel))
				{
					BindingContext = _userControlLogic.ViewModel;
				}
			};

			_userControlLogic.ViewModelPropertyChanged += (sender, e) =>
			{
				ViewModelPropertyChanged?.Invoke(this, e);
			};

			DataContextChanged += OnDataContextChanged;
		}
		#endregion

		#region Overridable
		/// <summary>
		/// Called when the <see cref="ViewModel" /> has changed.
		/// </summary>
		/// <remarks>
		/// This method does not implement any logic and saves a developer from subscribing/unsubscribing
		/// to the <see cref="ViewModelChanged" /> event inside the same user control.
		/// </remarks>
		protected virtual void OnViewModelChanged()
		{
		}

		/// <summary>
		/// Called when the <see cref="ViewModel" /> has been closed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
		protected virtual Task OnViewModelClosedAsync(object sender, ViewModelClosedEventArgs e) => Task.CompletedTask;
		#endregion

		#region IView members
		/// <summary>
		/// Gets or sets the data context.
		/// </summary>
		/// <value>
		/// The data context.
		/// </value>
		public object DataContext
		{
			get => BindingContext;
			set => BindingContext = value;
		}

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public object Tag
		{
			get;
			set;
		}
		#endregion

		#region IViewModelContainer members
		/// <summary>
		/// Gets the view model.
		/// </summary>
		public IViewModel ViewModel => DataContext as IViewModel;
		#endregion

		#region Overrided
		/// <summary>
		/// Occurs when the back button is pressed.
		/// </summary>
		/// <returns>
		/// To be added.
		/// </returns>
		/// <remarks>
		/// TODO: This implementation requires improvements.
		/// </remarks>
		protected sealed override bool OnBackButtonPressed()
		{
			/*
			BackButtonPressed?.Invoke(this);
			var popupLayout = Content as PopupLayout;
			//// TODO: Lookup for top most popup layout.
			return popupLayout != null && popupLayout.IsPopupActive || base.OnBackButtonPressed();
			*/

			BackButtonPressed?.Invoke(this, EventArgs.Empty);

			return base.OnBackButtonPressed();
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();

			if (Parent is null)
			{
				Unloaded?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				Loaded?.Invoke(this, EventArgs.Empty);
			}
		}
		#endregion

		#region Private
		private void OnBindingContextChanged(object o, EventArgs eventArgs)
		{
			DataContextChanged?.Invoke(this, new DataContextChangedEventArgs(_oldbindingContext, BindingContext));
			_oldbindingContext = BindingContext;
			RaiseViewModelChanged();
		}

		/// <summary>
		/// Occurs when the data context has changed.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="eventArgs">The data context changed event args.</param>
		private void OnDataContextChanged(object sender, DataContextChangedEventArgs eventArgs)
		{
			ViewModelChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises view model changed
		/// </summary>
		private void RaiseViewModelChanged()
		{
			OnViewModelChanged();
			ViewModelChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion
	}
}
