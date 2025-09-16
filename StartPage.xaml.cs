using System.Threading.Tasks;

namespace Mobiilirakendused_TARgv24;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TekstPage(), new FigurePage(), new TimerPage(), new Valgusfoor(), new DateTimePage(), new StepperSliderPage() };
	public List<string> tekstid = new List<string>() { "Tekst'ga leht", "Figure leht", "Timer", "Valgusfoor", "Kuupäevad ja kellaajad", "StepperSlider leht" };
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
				TextColor = Colors.White,
				Padding = 10,
                CornerRadius = 20,
				FontFamily = "Asimovian-Regular",
				Margin = 10,
				ZIndex = i,
                Shadow = new Shadow { Opacity = 0.35f, Offset = new Point(4, 4), Radius = 8 }
            };

            nupp.Background = new LinearGradientBrush(
				new GradientStopCollection
				{
					new GradientStop(Colors.OrangeRed, 0.0f),
					new GradientStop(Colors.Cyan, 1.0f)
				},
				new Point(0, 0),
				new Point(0, 1)
			);

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