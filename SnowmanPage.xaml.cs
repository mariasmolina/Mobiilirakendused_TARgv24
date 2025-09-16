namespace Mobiilirakendused_TARgv24;

public partial class SnowmanPage : ContentPage
{
    public List<string> tekstid = new List<string>() { "SISSE", "VÄLJA", "ÖÖ", "PÄEV", "JALAKÄIA" };
    Frame pea, keha, amber;
	AbsoluteLayout abs;
	Stepper st;
	Slider sl;
    public SnowmanPage()
	{
		amber = new Frame
		{
			BackgroundColor = Colors.Orange,
			WidthRequest = 30,
			HeightRequest = 30,
			CornerRadius = 15,
			HasShadow = false
		};
		pea = new Frame
		{

		};
		keha = new Frame
		{

		};
    }
}