using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TimeTracker.PageModels.Base
{
	internal class ExtendedBindableObject : BindableObject
	{
		/// <summary>
		/// Simplifes the process of updating a Bindable Property and calling INotifyPropertyChanged
		/// </summary>
		protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return;
			}

			storage = value;
			OnPropertyChanged(propertyName);
		}
	}
}