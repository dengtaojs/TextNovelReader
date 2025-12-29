using TextNovelReader.ViewModels;

namespace TextNovelReader.Views;

public partial class SettingsPage : ContentPage
{
	private readonly SettingsPageViewModel _viewModel;

	public SettingsPage(SettingsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
	}

	public SettingsPageViewModel ViewModel
	{
		get => _viewModel;
	}
}