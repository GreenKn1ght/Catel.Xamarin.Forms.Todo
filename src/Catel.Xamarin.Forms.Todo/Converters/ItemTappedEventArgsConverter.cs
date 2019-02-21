using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Catel.Xamarin.Forms.Todo.Converters
{
	public class ItemTappedEventArgsConverter : IMarkupExtension, IValueConverter
	{
		#region IMarkupExtension members
		/// <param name="serviceProvider">The service that provides the value.</param>
		/// <summary>Returns the object created from the markup extension.</summary>
		/// <returns>The object</returns>
		/// <remarks>To be added.</remarks>
		public object ProvideValue(IServiceProvider serviceProvider) => this;
		#endregion

		#region IValueConverter members
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using
		/// <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((ItemTappedEventArgs) value).Item;

		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using
		/// <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
		#endregion
	}
}
