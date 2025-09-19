using System.Threading.Tasks;

namespace Mobiilirakendused_TARgv24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TekstPage(), new FigurePage(), new TimerPage(), new Valgusfoor(), new DateTimePage(), new SnowmanPage() };
	public List<string> tekstid = new List<string>() { "Tekst'ga leht", "Figure leht", "Timer", "Valgusfoor", "Kuupäevad ja kellaajad", "Lumememm" };
	ScrollView sv;
	VerticalStackLayout vsl;
	Image img, img2;

	public StartPage()
	{
		Title = "Avaleht";
		BackgroundImageSource = "bg1.jpg";
		vsl = new VerticalStackLayout {  };
		img = new Image
		{
			Source = "image.png",
			HorizontalOptions = LayoutOptions.Center
		};
		vsl.Add(img);
		for (int i = 0; i < lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = tekstid[i],
				FontSize = 20,
				TextColor = Colors.White,
				Padding = 10,
				CornerRadius = 20,
				FontFamily = "Texturina-VariableFont",
				Margin = 10,
				WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 4,
				ZIndex = i,
				Shadow = new Shadow { Opacity = 0.35f, Offset = new Point(4, 4), Radius = 8 }
			};

			nupp.Background = new LinearGradientBrush(
				new GradientStopCollection
				{
					new GradientStop(Color.FromRgba(139, 0, 0, 230), 1.0f),
					new GradientStop(Color.FromRgba(255, 0, 0, 180), 0.7f),
					new GradientStop(Color.FromRgba(255, 0, 0, 0), 0.0f)
				},
				new Point(0, 0),
				new Point(0, 1)
			);

			vsl.Add(nupp);
			nupp.Clicked += Nupp_Clicked;
		}
		img2 = new Image
		{
			Source = "image.png",
			HorizontalOptions = LayoutOptions.Center
		};
		vsl.Add(img2);
		sv = new ScrollView { Content = vsl };
		Content = sv;
	}

	private async void Nupp_Clicked (object? sender, EventArgs e)
	{
		Button nupp = (Button)sender;
        await Navigation.PushAsync(lehed[nupp.ZIndex]);
	}
}