using System;
using System.Windows.Input;
using TimeTracker.PageModels.Base;
using Xamarin.Forms;

namespace TimeTracker.ViewModels.Buttons
{
	internal class ButtonModel : ExtendedBindableObject
	{
		public ButtonModel(string text, Action onClicked, bool isEnabled = true)
		{
			Text = text;
			Command = new Command(onClicked);
			IsEnabled = isEnabled;
		}

		string _text;

		public string Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		bool _isEnabled;

		public bool IsEnabled
		{
			get => _isEnabled;
			set => SetProperty(ref _isEnabled, value);
		}

		ICommand _command;

		public ICommand Command
		{
			get => _command;
			set => SetProperty(ref _command, value);
		}
	}
}