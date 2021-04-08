using Xamarin.Forms;

namespace TimeTracker.Views
{
	public partial class StatementEarningsView
	{
		public static readonly BindableProperty EarningsProperty = BindableProperty.Create(
			nameof(Earnings), typeof(double), typeof(StatementEarningsView),
			propertyChanged: OnEarningsPropertyChanged);

		public double Earnings
		{
			get => (double) GetValue(EarningsProperty);
			set => SetValue(EarningsProperty, value);
		}

		private static void OnEarningsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(newValue is double earnings))
			{
				return;
			}

			var formattedString = earnings.ToString("C");

			if (!(bindable is StatementEarningsView view))
			{
				return;
			}

			view.DollarsLabel.Text = formattedString.Substring(1, formattedString.IndexOf('.') - 1);
			view.CentsLabel.Text = formattedString.Substring(formattedString.IndexOf('.'));
		}

		public StatementEarningsView() => InitializeComponent();
	}
}