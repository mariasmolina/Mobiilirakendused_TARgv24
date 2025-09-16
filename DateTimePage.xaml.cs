using Microsoft.Maui.Layouts;

namespace Mobiilirakendused_TARgv24;

public partial class DateTimePage : ContentPage
{
    Label mis_on_valitud;
    DatePicker datePicker;
    TimePicker timePicker;
    Picker picker;
    Slider slider;
    Stepper stepper;
    AbsoluteLayout al;
    public DateTimePage()
    {
        mis_on_valitud = new Label
        {
            Text = "Siin kuvatakse valitud kuup�ev v�i kellaaeg",
            FontSize = 25,
            TextColor = Colors.Red,
            FontFamily = "Anta-Regular"
        };
        datePicker = new DatePicker
        {
            FontSize = 20,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black,
            FontFamily = "Anta-Regular",
            MinimumDate = DateTime.Now.AddDays(-7), // minimum on 7 p�eva tagasi
            //new DateTime(1900, 1, 1),
            MaximumDate = new DateTime(2100, 12, 31),
            Date = DateTime.Now,
            Format = "D" // "dd/MM/yyyy"
        };
        datePicker.DateSelected += Kuup�eva_valimine;

        timePicker = new TimePicker
        {
            FontSize = 20,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black,
            FontFamily = "Anta-Regular",
            Time = new TimeSpan(12, 0, 0),// keskp�ev
            Format = "t"
        };
        timePicker.PropertyChanged += (s, e) => 
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                mis_on_valitud.Text = $"Valitud kellaaeg: {timePicker.Time}";
            }
        };

        picker = new Picker
        {
            Title = "Vali �ks",
            FontSize = 20,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Colors.Black,
            FontFamily = "Anta-Regular",
            ItemsSource = new List<string> { "�ks", "Kaks", "Kolm", "Neli", "Viis" }
        };
        //picker.Items.Add("Kuus");
        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedIndex != -1)
            {
                mis_on_valitud.Text = $"Valitud: {picker.Items[picker.SelectedIndex]}";
            }
        };

        slider = new Slider
        {
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            ThumbColor = Colors.Red,
            MinimumTrackColor = Colors.Green,
            MaximumTrackColor = Colors.Blue
        };
        slider.ValueChanged += (s, e) =>
        {
            mis_on_valitud.FontSize = e.NewValue;
            mis_on_valitud.Rotation = e.NewValue;
        };

        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 100,
            Increment = 1,
            Value = 20,
            BackgroundColor = Color.FromRgb(200, 200, 100),
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += (s, e) =>
        {
            mis_on_valitud.Text = $"Stepper value: {e.NewValue}";
        };

        al = new AbsoluteLayout
        {
            Children = { mis_on_valitud, datePicker, timePicker, picker, slider, stepper }
        };
        //AbsoluteLayout.SetLayoutBounds(mis_on_valitud, new Rect(0.5, 0.2, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        //AbsoluteLayout.SetLayoutFlags(mis_on_valitud, AbsoluteLayoutFlags.All);
        //AbsoluteLayout.SetLayoutBounds(datePicker, new Rect(0.5, 0.5, 0.9, 0.2));
        //AbsoluteLayout.SetLayoutFlags(datePicker, AbsoluteLayoutFlags.All);
        //AbsoluteLayout.SetLayoutBounds(timePicker, new Rect(0.5, 0.8, 0.9, 0.2));  // X, Y, Width, Height
        //AbsoluteLayout.SetLayoutFlags(timePicker, AbsoluteLayoutFlags.All); // k�ik paika
        //AbsoluteLayout.SetLayoutBounds(picker, new Rect(0.5, 0.6, 0.9, 0.2));
        //AbsoluteLayout.SetLayoutFlags(picker, AbsoluteLayoutFlags.All);
        //AbsoluteLayout.SetLayoutBounds(slider, new Rect(0.5, 0.8, 0.9, 0.2));
        //AbsoluteLayout.SetLayoutFlags(slider, AbsoluteLayoutFlags.All);
        //AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.5, 1.0, 0.9, 0.2));
        //AbsoluteLayout.SetLayoutFlags(stepper, AbsoluteLayoutFlags.All);

        var elementid = new View[]
        {
             mis_on_valitud, datePicker, timePicker, picker, slider, stepper
        };
        for (int i = 0; i < elementid.Length; i++)
        {
            AbsoluteLayout.SetLayoutBounds(elementid[i], new Rect(0.5, i * 0.16, 0.9, 0.15));
            AbsoluteLayout.SetLayoutFlags(elementid[i], AbsoluteLayoutFlags.All);
        }
        Content = al;
    }

    private void Kuup�eva_valimine(object? sender, DateChangedEventArgs e)
    {
        mis_on_valitud.Text = $"Valitud kuup�ev: {e.NewDate:D}";
    }
}