using System.Threading.Tasks;

namespace Mobiilirakendused_TARgv24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TekstPage(), new FigurePage() };
	public List<string> tekstid = new List<string>() { "Tee lahti leht Tekst'ga", "Tee lahti Figure leht" };
	ScrollView sv;
	VerticalStackLayout vsl;

	public StartPage()
	{
		//InitializeComponent();
		Title = "Avaleht";
		vsl = new VerticalStackLayout { BackgroundColor = Color.FromRgb(240, 240, 240) };
		for (int i = 0; i < lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = tekstid[i],
				FontSize = 20,
				BackgroundColor = Color.FromRgb(0, 120, 215),
				TextColor = Colors.White,
				CornerRadius = 20,
				FontFamily = "Asimovian-Regular",
				Margin = 10,
				ZIndex = i
            };
			vsl.Add(nupp);
			nupp.Clicked += Nupp_Clicked;
        }
		sv = new ScrollView { Content = vsl };
		Content = sv;
    }

	private async void Nupp_Clicked (object? sender, EventArgs e)
	{
		Button nupp = (Button)sender;
		await Navigation.PushAsync(lehed[nupp.ZIndex]);
	}
}