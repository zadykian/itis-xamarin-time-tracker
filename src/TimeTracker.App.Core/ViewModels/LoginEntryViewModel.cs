using TimeTracker.App.Core.PageModels.Base;

namespace TimeTracker.App.Core.ViewModels
{
	internal class LoginEntryViewModel : ExtendedBindableObject
	{
		public LoginEntryViewModel(string placeholder, bool isPassword)
		{
			Placeholder = placeholder;
			IsPassword = isPassword;
		}

		private string placeholder;

		public string Placeholder
		{
			get => placeholder;
			set => SetProperty(ref placeholder, value);
		}

		private string text;

		public string Text
		{
			get => text;
			set => SetProperty(ref text, value);
		}

		private bool isPassword;

		public bool IsPassword
		{
			get => isPassword;
			set => SetProperty(ref isPassword, value);
		}
	}
}