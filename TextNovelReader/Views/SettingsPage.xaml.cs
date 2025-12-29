using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class SettingsPage : ContentPage
{
	private readonly ReaderViewModel _viewModel;

	public SettingsPage(ReaderViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
	}

	public ReaderViewModel ViewModel => _viewModel;
}