using System;
using System.Windows.Input;
using TimeTracker.PageModels.Base;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
	internal class ButtonModel : ExtendedBindableObject
	{
		public ButtonModel(string text, Action onClicked, bool isEnabled = true)
		{
			Text = text;
			Command = new Command(onClicked);
			IsEnabled = isEnabled;
		}

		private string text;

		public string Text
		{
			get => text;
			set => SetProperty(ref text, value);
		}

		private bool isEnabled;

		public bool IsEnabled
		{
			get => isEnabled;
			set => SetProperty(ref isEnabled, value);
		}

		private ICommand command;

		public ICommand Command
		{
			get => command;
			set => SetProperty(ref command, value);
		}
	}
}