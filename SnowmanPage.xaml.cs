using Microsoft.Maui.Layouts;

namespace Mobiilirakendused_TARgv24;

public partial class SnowmanPage : ContentPage
{
    public List<string> tegevused = new() 
    { 
        "Peida lumememm", "Näita lumememm", "Muuda värvi", "Sulata", "Tantsi", "Räägi"
    };
    bool tegevus_töötab = false;
    bool öö = false;

    Frame pea, keha, amber, lbl_frame;
    BoxView silm_v, silm_p, btn1, btn2, btn3, porgand, smile1, smile2, smile3, smile4, smile5;
    AbsoluteLayout abs;
    Button nupp;
    Label lbl, lbl_kiirus;
    Picker picker;
    Stepper stepper;
    Slider slider;
    Switch switch_päev;
    Random random = new Random();

    public SnowmanPage()
    {
        BackgroundImageSource = "winter_bg.jpg";

        lbl = new Label
        {
            Text = "Vali tegevus!",
            FontSize = 25,
            TextColor = Colors.White,
            FontFamily = "Anta-Regular",
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };
        lbl_frame = new Frame
        {
            Content = lbl,
            BackgroundColor = Color.FromRgba(30, 144, 255, 130),
            Padding = 5,
            CornerRadius = 20,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            Margin = 10
        };
            
        // Elemendid lumememme jaoks
        amber = new Frame { CornerRadius = 6, HasShadow = false };

        pea = new Frame 
        { 
            CornerRadius = 100,
            HasShadow = false
        };
        

        keha = new Frame 
        { 
            CornerRadius = 150,
            HasShadow = false,
        };

        // värvib lumememme originaal värvidesse
        näita_originaal_värvid();

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
            if (picker.SelectedIndex == 2 || picker.SelectedIndex == 3 || picker.SelectedIndex == 4 || picker.SelectedIndex == 5)
            {
                näita_lumememm();
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
            var elemendid = new View[]
            {
                pea, keha, amber, silm_v, silm_p, btn1, btn2, btn3, 
                porgand, smile1, smile2, smile3, smile4, smile5
            };
            foreach (var v in elemendid)
            {
                v.Opacity = e.NewValue;
            }
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

        // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/switch?view=net-maui-9.0
        switch_päev = new Switch
        {
            IsToggled = false,
            OnColor = Colors.LightYellow,
            Scale = 1.5,
            ThumbColor = Colors.LightSkyBlue
        };
        switch_päev.Toggled += async (sender, e) =>
        {
            if (e.Value) // kui lüliti on sees
            {
                öö = true;
                BackgroundImageSource = "winter_night.jpg";
                while (öö == true)
                {
                    BackgroundImageSource = "winter_night.jpg";

                    double algne_x = random.Next(0, (int)abs.Width);
                    double algne_y = -20;

                    _ = lumesadu(algne_x, algne_y); // helbed langevad paralleelselt

                    await Task.Delay(random.Next(100, 500)); // juhuslik paus lumehelveste loomisel
                }
            }
            else
            {
                öö = false;
                BackgroundImageSource = "winter_bg.jpg";
            }
        };

        // Nupp - tegevuse käivitamiseks
        nupp = new Button
        {
            Text = "Käivita",
            FontSize = 20,
            TextColor = Colors.White,
            FontFamily = "Anta-Regular",
            Margin = 10,
            CornerRadius = 50,
            Background = new LinearGradientBrush
            (
                new GradientStopCollection
                {
                    new GradientStop(Colors.LightBlue, 0.0f),
                    new GradientStop(Colors.Blue, 1.0f)
                },
                new Point(0, 0),
                new Point(0, 1)
            )
        };
        nupp.Clicked += Nupp_Clicked;

        // Paigutus AbsoluteLayout abil
        abs = new AbsoluteLayout()
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            Children =
            {
                lbl_frame, keha, pea, amber, picker, nupp, slider, stepper, lbl_kiirus, switch_päev,
                silm_v, silm_p, porgand, btn1, btn2, btn3,
                smile1, smile2, smile3, smile4, smile5
            }
        };

        AbsoluteLayout.SetLayoutBounds(pea, new Rect(0.5, 0.30, 150, 150));  // x, y, width, height
        AbsoluteLayout.SetLayoutFlags(pea, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(keha, new Rect(0.5, 0.60, 250, 250));
        AbsoluteLayout.SetLayoutFlags(keha, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(amber, new Rect(0.5, 0.13, 100, 110));
        AbsoluteLayout.SetLayoutFlags(amber, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(silm_v, new Rect(0.43, 0.30, 14, 14));
        AbsoluteLayout.SetLayoutFlags(silm_v, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(silm_p, new Rect(0.58, 0.30, 14, 14));
        AbsoluteLayout.SetLayoutFlags(silm_p, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(porgand, new Rect(0.42, 0.33, 60, 16));
        AbsoluteLayout.SetLayoutFlags(porgand, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile1, new Rect(0.40, 0.36, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile2, new Rect(0.44, 0.38, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile2, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile3, new Rect(0.5, 0.387, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile3, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile4, new Rect(0.56, 0.38, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile4, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(smile5, new Rect(0.60, 0.36, 10, 10));
        AbsoluteLayout.SetLayoutFlags(smile5, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn1, new Rect(0.5, 0.48, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn2, new Rect(0.5, 0.56, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn2, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(btn3, new Rect(0.5, 0.64, 16, 16));
        AbsoluteLayout.SetLayoutFlags(btn3, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(lbl_frame, new Rect(0.3, 0.01, 350, 100));
        AbsoluteLayout.SetLayoutFlags(lbl_frame, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(picker, new Rect(0.1, 0.82, 180, 50));
        AbsoluteLayout.SetLayoutFlags(picker, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(nupp, new Rect(0.97, 0.83, 150, 70));
        AbsoluteLayout.SetLayoutFlags(nupp, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(slider, new Rect(0.5, 0.91, 250, 50));
        AbsoluteLayout.SetLayoutFlags(slider, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(lbl_kiirus, new Rect(0.25, 0.98, 150, 50));
        AbsoluteLayout.SetLayoutFlags(lbl_kiirus, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.8, 0.98, 150, 50));
        AbsoluteLayout.SetLayoutFlags(stepper, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(switch_päev, new Rect(0.6, 0.20, 200, 100));
        AbsoluteLayout.SetLayoutFlags(switch_päev, AbsoluteLayoutFlags.PositionProportional);

        Content = abs;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        var tegevus = picker.SelectedItem as string;

        var elemendid = new View[]
        {
            pea, keha, amber, silm_v, silm_p, btn1, btn2, btn3, porgand, smile1, smile2, smile3, smile4, smile5
        };

        // Tegevuste käsitlemine
        switch (tegevus)
        {
            case "Peida lumememm":
                {
                    lbl.Text = "Lumememm on peidetud";
                    foreach (var v in elemendid)
                    {
                        v.IsVisible = false;
                    }
                    break;
                }

            case "Näita lumememm":
                {
                    lbl.Text = "Lumememm on nähtav";
                    näita_lumememm();
                    break;
                }

            case "Muuda värvi":
                {
                    lbl.Text = "Värv muutub juhuslikult";
                    bool ok = await DisplayAlert("Kinnitus", "Muuda värvi juhuslikult?", "Jah", "Ei");
                    if (ok == false)
                    {
                        // värvib lumememme originaal värvidesse
                        näita_originaal_värvid();
                        break;
                    }

                    Color random_värv() => Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
                    // massiiv juhuslike värvide jaoks
                    var värvid = new[] { random_värv(), random_värv(), random_värv(), random_värv() }; 
                    for (int i = 0; i < värvid.Length; i++)
                    {
                        värvid[i] = random_värv();
                    }


                    amber.Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                            new GradientStop(värvid[0], 1.0f),
                            new GradientStop(värvid[1], 0.0f)
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                    pea.Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                            new GradientStop(värvid[2], 1.0f),
                            new GradientStop(värvid[2], 0.5f),
                            new GradientStop(värvid[3], 0.0f)
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                    keha.Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                            new GradientStop(värvid[2], 1.0f),
                            new GradientStop(värvid[3], 0.0f)
                        },
                        new Point(0, 0),
                        new Point(0, 1)
                    );
                    break;
                }

            case "Sulata":
                {
                    if (tegevus_töötab)
                    {
                        break;
                    }
                    tegevus_töötab = true;
                    lbl.Text = "Lumememm on sulanud";

                    var kõik_animatsioonid = new List<Task>();
                    foreach (var v in elemendid)
                    {
                        // value, kiirus ms, easing
                        // Easing - кривая плавности анимации, CubicIn - плавный старт и ускорение в конце
                        kõik_animatsioonid.Add(v.FadeTo(0, (uint)stepper.Value, Easing.CubicIn));
                        kõik_animatsioonid.Add(v.ScaleTo(0.6, (uint)stepper.Value, Easing.CubicIn));
                    }
                    await Task.WhenAll(kõik_animatsioonid);

                    tegevus_töötab = false;
                    break;
                }

            case "Tantsi":
                {
                    if (tegevus_töötab)
                    {
                        break;
                    }
                     
                    tegevus_töötab = true;
                    lbl.Text = "Lumememm tantsib!";

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

                    tegevus_töötab = false;
                    break;
                }

            case "Räägi":
                {
                    if (tegevus_töötab)
                    {
                        break;
                    }
                    tegevus_töötab = true;
                    lbl.Text = "Lumememm räägib!";

                    IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

                    SpeechOptions options = new SpeechOptions()
                    {
                        Pitch = 1.5f,   // 0.0 - 2.0
                        Volume = 0.75f, // 0.0 - 1.0
                        Locale = locales.FirstOrDefault(l => l.Language == "et-EE")
                    };
                    await TextToSpeech.SpeakAsync("Ohoho, jõulud tulevad", options);

                    tegevus_töötab = false;
                    break;
                }
        }
    }
    // näitab lumememme elemendid ja taastab nende algseisundi
    private void näita_lumememm()
    {
        var elemendid = new View[]
        {
            pea, keha, amber, silm_v, silm_p, btn1, btn2, btn3, porgand, smile1, smile2, smile3, smile4, smile5
        };
        foreach (var v in elemendid)
        {
            v.IsVisible = true;
            v.Opacity = 1;
            v.Scale = 1;  // maksimaalne suurus
            v.TranslationX = 0; // keskele x
            v.TranslationY = 0; // keskele y
        }
    }

    // värvib lumememme originaal värvidesse
    private void näita_originaal_värvid()
    {
        keha.Background = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Colors.White, 1.0f),
                new GradientStop(Colors.LightBlue, 0.0f)
            },
            new Point(0, 0),
            new Point(0, 1)
        );
        pea.Background = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Colors.White, 1.0f),
                new GradientStop(Colors.White, 0.5f),
                new GradientStop(Colors.LightBlue, 0.0f)
            },
            new Point(0, 0),
            new Point(0, 1)
        );
        amber.Background = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Colors.Grey, 1.0f),
                new GradientStop(Colors.DarkGrey, 0.0f)
            },
            new Point(0, 0),
            new Point(0, 1)
        );
    }

    // loob lumehelbed ja teeb animatsiooni
    private async Task lumesadu(double x, double y)
    {
        var lumi = new Image
        {
            Source = "snowflake_white.png",
            HeightRequest = random.Next(2, 60),
            WidthRequest = random.Next(2, 60)
        };

        // algne lumehelbe paigutamine
        AbsoluteLayout.SetLayoutBounds(lumi, new Rect(x, y, lumi.WidthRequest, lumi.HeightRequest));
        AbsoluteLayout.SetLayoutFlags(lumi, AbsoluteLayoutFlags.None);

        abs.Children.Add(lumi);

        // juhuslik liikumine
        double targetX = x + random.Next(-50, 50);
        double targetY = abs.Height;
        uint duration = (uint)random.Next(3000, 7000);

        // animatsioon
        await lumi.TranslateTo(targetX - x, targetY - y, duration, Easing.Linear);

        abs.Children.Remove(lumi);
    }
}