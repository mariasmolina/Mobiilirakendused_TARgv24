using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using static System.Net.Mime.MediaTypeNames;

namespace Mobiilirakendused_TARgv24;

public partial class SnowmanPage : ContentPage
{
    public List<string> tegevused = new() 
    { 
        "Peida lumememm", "Näita lumememm", "Muuda värvi", "Sulata", "Tantsi", "Räägi"
    };
    bool busy = false;

    Frame pea, keha, amber;
    BoxView silm_v, silm_p, btn1, btn2, btn3, porgand, smile1, smile2, smile3, smile4, smile5;
    AbsoluteLayout abs;
    Button nupp;
    Label lbl, lbl_kiirus;
    Picker picker;
    Stepper stepper;
    Slider slider;
    Random random = new Random();

    public SnowmanPage()
    {
        BackgroundImageSource = "winter_bg.jpg";

        lbl = new Label
        {
            Text = "",
            FontSize = 25,
            TextColor = Colors.Red,
            FontFamily = "Anta-Regular"
        };

        // Elemendid lumememme jaoks
        amber = new Frame
        {
            BackgroundColor = Colors.Grey,
            CornerRadius = 6
        };

        pea = new Frame 
        { 
            CornerRadius = 100,
            HasShadow = false,
            Background = new LinearGradientBrush
            (
                new GradientStopCollection
                {
                    new GradientStop(Colors.White, 1.0f),
                    new GradientStop(Colors.White, 0.5f),
                    new GradientStop(Colors.LightBlue, 0.0f)
                },
                new Point(0, 0),
                new Point(0, 1)
            )
        };
        

        keha = new Frame 
        { 
            CornerRadius = 150,
            HasShadow = false,
            Background = new LinearGradientBrush
            (
                new GradientStopCollection
                {
                    new GradientStop(Colors.White, 1.0f),
                    new GradientStop(Colors.LightBlue, 0.0f)
                },
                new Point(0, 0),
                new Point(0, 1)
            )
        };

        silm_v = new BoxView { Color = Colors.Black, CornerRadius = 7 };
        silm_p = new BoxView { Color = Colors.Black, CornerRadius = 7 };

        btn1 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        btn2 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        btn3 = new BoxView { Color = Colors.Black, CornerRadius = 8 };

        smile1 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        smile2 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        smile3 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        smile4 = new BoxView { Color = Colors.Black, CornerRadius = 8 };
        smile5 = new BoxView { Color = Colors.Black, CornerRadius = 8 };

        porgand = new BoxView { Color = Colors.Orange, CornerRadius = 10 };

        // Picker - tegevuste valimiseks
        picker = new Picker
        {
            Title = "Vali tegevus",
            FontSize = 20,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontFamily = "Anta-Regular",
            ItemsSource = tegevused
        };
        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedIndex != -1)
            {
                lbl.Text = $"Valitud: {picker.Items[picker.SelectedIndex]}";
            }
        };

        // Slider - lumememme läbipaistvuse reguleerimiseks
        slider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 50,
            ThumbImageSource = "snowflake.png",
            BackgroundColor = Color.FromRgba(200, 200, 100, 0),
            MinimumTrackColor = Colors.Green,
            MaximumTrackColor = Colors.Blue
        };
        slider.ValueChanged += (s, e) =>
        {
            pea.Opacity = e.NewValue;
            keha.Opacity = e.NewValue;
            amber.Opacity = e.NewValue;
            silm_p.Opacity = e.NewValue;
            silm_v.Opacity = e.NewValue;
            btn1.Opacity = e.NewValue;
            btn2.Opacity = e.NewValue;
            btn3.Opacity = e.NewValue;
        };

        // Stepper - tegevuse kiiruse reguleerimiseks
        stepper = new Stepper
        {
            Minimum = 100, // 100-2000 ms kiirus
            Maximum = 2000,
            Increment = 100,
            Value = 500,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        };
        lbl_kiirus = new Label
        {
            Text = $"Kiirus: {stepper.Value}",
            FontSize = 20,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            TextColor = Colors.DarkBlue,
            Margin = new Thickness(0, 10, 0, 0),
            FontFamily = "Anta-Regular"
        };
        stepper.ValueChanged += (s, e) =>
        {
            stepper.Value = e.NewValue;
            lbl_kiirus.Text = $"Kiirus: {e.NewValue}";
        };
       

        // Nupp - tegevuse käivitamiseks
        nupp = new Button
        {
            Text = "Käivita",
            FontSize = 20,
            BackgroundColor = Colors.LightBlue,
            TextColor = Colors.Black,
            FontFamily = "Anta-Regular",
            Margin = 10,
            CornerRadius = 50
        };
        nupp.Clicked += Nupp_Clicked;

        // Paigutus AbsoluteLayout abil
        abs = new AbsoluteLayout()
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            Children =
            {
                lbl, keha, pea, amber, picker, nupp, slider, stepper, lbl_kiirus,
                silm_v, silm_p, porgand, btn1, btn2, btn3,
                smile1, smile2, smile3, smile4, smile5
            }
        };

        AbsoluteLayout.SetLayoutBounds(pea, new Rect(0.5, 0.35, 150, 150));  // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(pea, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(keha, new Rect(0.5, 0.65, 250, 250));
        AbsoluteLayout.SetLayoutFlags(keha, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(amber, new Rect(0.5, 0.18, 100, 110));
        AbsoluteLayout.SetLayoutFlags(amber, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(silm_v, new Rect(0.43, 0.35, 14, 14));
        AbsoluteLayout.SetLayoutFlags(silm_v, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(silm_p, new Rect(0.58, 0.35, 14, 14));
        AbsoluteLayout.SetLayoutFlags(silm_p, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(porgand, new Rect(0.42, 0.38, 60, 16));
        AbsoluteLayout.SetLayoutFlags(porgand, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile1, new Rect(0.40, 0.41, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile2, new Rect(0.44, 0.43, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile2, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile3, new Rect(0.5, 0.437, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile3, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile4, new Rect(0.56, 0.43, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile4, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile5, new Rect(0.60, 0.41, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile5, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn1, new Rect(0.5, 0.53, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn2, new Rect(0.5, 0.61, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn2, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn3, new Rect(0.5, 0.69, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn3, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(lbl, new Rect(0.5, 0.03, 300, 50));
        AbsoluteLayout.SetLayoutFlags(lbl, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(picker, new Rect(0.1, 0.85, 180, 50));
        AbsoluteLayout.SetLayoutFlags(picker, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(nupp, new Rect(0.97, 0.86, 150, 70));
        AbsoluteLayout.SetLayoutFlags(nupp, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(slider, new Rect(0.5, 0.95, 250, 50));
        AbsoluteLayout.SetLayoutFlags(slider, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(lbl_kiirus, new Rect(0.25, 1, 150, 50));
        AbsoluteLayout.SetLayoutFlags(lbl_kiirus, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.8, 1, 150, 50));
        AbsoluteLayout.SetLayoutFlags(stepper, AbsoluteLayoutFlags.PositionProportional);

        Content = abs;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        var tegevus = picker.SelectedItem as string;

        var elemendid = new View[]
        {
            pea, keha, amber, silm_v, silm_p, btn1, btn2, btn3, porgand, smile1, smile2, smile3, smile4, smile5
        };

        if (string.IsNullOrWhiteSpace(tegevus))
        {
            await DisplayAlert("Hoiatus", "Vali tegevus Pickerist.", "OK");
            return;
        }

        lbl.Text = $"Valitud: {tegevus}";

        // Tegevuste käsitlemine
        switch (tegevus)
        {
            case "Peida lumememm":
                foreach (var v in elemendid)
                {
                    v.IsVisible = false;
                }
                break;

            case "Näita lumememm":
                foreach (var v in elemendid)
                {
                    v.IsVisible = true;
                    v.Opacity = 1;
                    v.Scale = 1;  // maksimaalne suurus
                    v.TranslationX = 0; // keskele x
                    v.TranslationY = 0; // keskele y
                }
                break;

            case "Muuda värvi":
                {
                    bool ok = await DisplayAlert("Kinnitus", "Muuda värvi juhuslikult?", "Jah", "Ei");
                    if (ok == false)
                    {
                        break;
                    }

                    Color random_värv() => Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
                    var värv1 = random_värv();
                    var värv2 = random_värv();
                    var värv3 = random_värv();
                    var värv4 = random_värv();
                    var värv5 = random_värv();

                    amber.BackgroundColor = värv1;
                    pea.Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                    new GradientStop(värv2, 1.0f),
                    new GradientStop(värv2, 0.5f),
                    new GradientStop(värv3, 0.0f)
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                    keha.Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                    new GradientStop(värv4, 1.0f),
                    new GradientStop(värv5, 0.0f)
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                    break;
                }

            case "Sulata":
                {
                    if (busy)
                    {
                        break;
                    }
                    busy = true;


                    var kõik_animatsioonid = new List<Task>();
                    foreach (var v in elemendid)
                    {
                        // value, kiirus ms, easing
                        // Easing - кривая плавности анимации, CubicIn - плавный старт и ускорение в конце
                        kõik_animatsioonid.Add(v.FadeTo(0, (uint)stepper.Value, Easing.CubicIn));
                        kõik_animatsioonid.Add(v.ScaleTo(0.6, (uint)stepper.Value, Easing.CubicIn));
                    }
                    await Task.WhenAll(kõik_animatsioonid);

                    busy = false;
                    break;
                }

            case "Tantsi":
                {
                    if (busy)
                    {
                        break;
                    }
                    busy = true;

                    // liigub paremale, vasakule ja hüppab 3 korda - TranslateTo
                    for (int i = 0; i < 3; i++)
                    {
                        // liigub vasakule
                        var vasakule = new List<Task>();
                        foreach (var v in elemendid)
                        {
                            // x, y, kiirus ms, easing
                            // SinInOut - мягкий старт, и мягкая остановка
                            vasakule.Add(v.TranslateTo(-30, 0, (uint)stepper.Value, Easing.SinInOut));
                        }
                        await Task.WhenAll(vasakule);

                        // hüppab
                        var hüppa = new List<Task>();
                        foreach (var v in elemendid)
                        {
                            hüppa.Add(v.TranslateTo(-30, -30, (uint)stepper.Value, Easing.SinInOut));
                        }
                        await Task.WhenAll(hüppa);

                        // liigub paremale
                        var paremale = new List<Task>();
                        foreach (var v in elemendid)
                        {
                            paremale.Add(v.TranslateTo(30, 0, (uint)stepper.Value, Easing.SinInOut));
                        }
                        await Task.WhenAll(paremale);

                        // hüppab
                        foreach (var v in elemendid)
                        {
                            hüppa.Add(v.TranslateTo(30, -30, (uint)stepper.Value, Easing.SinInOut));
                        }
                        await Task.WhenAll(hüppa);
                    }

                    // tagasi keskele
                    var keskele = new List<Task>();
                    foreach (var v in elemendid)
                    {
                        keskele.Add(v.TranslateTo(0, 0, (uint)stepper.Value, Easing.SinInOut)); 
                    }
                    await Task.WhenAll(keskele);

                    busy = false;
                    break;
                }

            case "Räägi":
                IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

                SpeechOptions options = new SpeechOptions()
                {
                    Pitch = 1.5f,   // 0.0 - 2.0
                    Volume = 0.75f, // 0.0 - 1.0
                    Locale = locales.FirstOrDefault(l => l.Language == "et-EE")
                };
                await TextToSpeech.SpeakAsync("Ohoho, jõulud tulevad", options);
                break;

        }
    }
}