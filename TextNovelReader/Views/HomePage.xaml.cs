using TextNovelReader.ViewModels;

namespace TextNovelReader.Views;

public partial class HomePage : ContentPage
{
	private readonly HomePageViewModel _viewModel; 
	public HomePage(HomePageViewModel viewModel)
	{
		_viewModel = viewModel; 
		InitializeComponent();
	}

	public HomePageViewModel ViewModel
	{
		get => _viewModel; 
	}
}