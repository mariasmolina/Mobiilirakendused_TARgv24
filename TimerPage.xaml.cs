using System.Threading.Tasks;

namespace Mobiilirakendused_TARgv24;

public partial class TimerPage : ContentPage
{
	public TimerPage()
	{
		InitializeComponent();
	}

	bool on_off = true;
	private void Klik_pealdise_peal(object sender, TappedEventArgs e)
	{
		if (on_off)
		{
			on_off = false;
		}
		else
		{
			on_off = true;
			N�ita_aeg();
		}
	}
	private async void N�ita_aeg()
	{
		while (on_off)
		{
			label.Text = DateTime.Now.ToString("HH:mm:ss");
			await Task.Delay(1000);
		}
    }
}