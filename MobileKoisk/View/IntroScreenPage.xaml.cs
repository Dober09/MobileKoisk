using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class IntroScreenPage : ContentPage
{
	public IntroScreenPage()
	{
		InitializeComponent();

		BindingContext = new IntroScreenViewModel();

	}
}