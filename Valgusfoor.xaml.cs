using System;
using System.Security.Cryptography.X509Certificates;

namespace Mobiilirakendused_TARgv24;

public partial class Valgusfoor : ContentPage
{
    public List<string> tekstid = new List<string>() { "SISSE", "VÄLJA", "ÖÖ", "PÄEV", "JALAKÄIA" };
    public Color punane, kollane, roheline, hall;
    bool foor_on = false;
    bool päev = false;
    bool öö = false;
    bool jalakäia = false;

    ScrollView sv;
    HorizontalStackLayout hsl_nupud_rida1, hsl_nupud_rida2;
    VerticalStackLayout vsl_valgusfoor, vsl;
    Frame frame, lbl_frame, punane_foor, kollane_foor, roheline_foor;
    Label pealkiri_lbl, kollane_lbl;

    public Valgusfoor()
    {
        //InitializeComponent();
        Title = "Valgusfoor";
        var foor_suurus = DeviceDisplay.MainDisplayInfo.Width / 8;
        punane = Color.FromRgb(255, 0, 0);
        kollane = Color.FromRgb(255, 255, 0);
        roheline = Color.FromRgb(0, 255, 0);
        hall = Color.FromRgb(128, 128, 128);

        pealkiri_lbl = new Label
        {
            Text = "Lülita foor sisse!",
            FontSize = 40,
            TextColor = Colors.White,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontFamily = "Anta-Regular"
        };

        // Pealkirja raam
        lbl_frame = new Frame
        {
            Content = pealkiri_lbl,
            BackgroundColor = Color.FromRgba(0, 0, 139, 130),
            Padding = 5,
            CornerRadius = 20,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            Margin = 10
        };

        // Jalakäia režiimi taimeri jaoks
        kollane_lbl = new Label
        {
            Text = "",
            FontSize = 70,
            FontFamily = "Doto-VariableFont",
            TextColor = Color.FromRgba(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        // Valgusfoor
        // Punane   
        punane_foor = new Frame
        {
            BackgroundColor = hall,
            WidthRequest = foor_suurus,
            HeightRequest = foor_suurus,
            CornerRadius = (float)foor_suurus / 2,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center
        };
        var tapRed = new TapGestureRecognizer();
        tapRed.Tapped += click_punane;
        punane_foor.GestureRecognizers.Add(tapRed);

        // Kollane
        kollane_foor = new Frame
        {
            BackgroundColor = hall,
            WidthRequest = foor_suurus,
            HeightRequest = foor_suurus,
            CornerRadius = (float)foor_suurus / 2,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Content = kollane_lbl
        };
        var tapYellow = new TapGestureRecognizer();
        tapYellow.Tapped += click_kollane;
        kollane_foor.GestureRecognizers.Add(tapYellow);

        // Roheline
        roheline_foor = new Frame
        {
            BackgroundColor = hall,
            WidthRequest = foor_suurus,
            HeightRequest = foor_suurus,
            CornerRadius = (float)foor_suurus / 2,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center
        };
        var tapGreen = new TapGestureRecognizer();
        tapGreen.Tapped += click_roheline;
        roheline_foor.GestureRecognizers.Add(tapGreen);

        vsl_valgusfoor = new VerticalStackLayout
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center,
            Children = { punane_foor, kollane_foor, roheline_foor }
        };

        // Raam
        frame = new Frame
        {
            BackgroundColor = Color.FromRgb(56, 56, 56),
            Content = vsl_valgusfoor,
            WidthRequest = foor_suurus * 1.5,
            HeightRequest = (3 * foor_suurus) + 100,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            CornerRadius = 50,
            Margin = 10
        };

        // Nupud
        hsl_nupud_rida1 = new HorizontalStackLayout { BackgroundColor = Color.FromRgba(0, 0, 0, 0), HorizontalOptions = LayoutOptions.Center };
        hsl_nupud_rida2 = new HorizontalStackLayout { BackgroundColor = Color.FromRgba(0, 0, 0, 0), HorizontalOptions = LayoutOptions.Center };
        for (int i = 0; i < tekstid.Count; i++)
        {
            Button nupp = new Button
            {
                Text = tekstid[i],
                FontSize = 20,
                BackgroundColor = Color.FromRgb(0, 120, 215),
                TextColor = Colors.White,
                CornerRadius = 100,
                FontFamily = "Anta-Regular",
                Margin = 10,
                ZIndex = i
            };
            if (i == 0)
            {
                nupp.BackgroundColor = Color.FromRgb(27, 179, 100);
            }
            else if (i == 1)
            {
                nupp.BackgroundColor = Color.FromRgb(214, 26, 26);
            }
            
            if (i < 2)
            {
                hsl_nupud_rida1.Add(nupp);
            }
            else
            {
                hsl_nupud_rida2.Add(nupp);
            }
            nupp.Clicked += nupp_clicked;
        }

        // Taustapilt
        BackgroundImageSource = "background_night.jpg";

        vsl = new VerticalStackLayout
        {
            Children = { lbl_frame, frame, hsl_nupud_rida1 },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        sv = new ScrollView { Content = vsl, HorizontalOptions = LayoutOptions.Center };
        Content = sv;
    }

    //Funktsioonid
    private async void nupp_clicked(object? sender, EventArgs e)
    {
        Button nupp = (Button)sender;
        if (nupp.ZIndex == 0)
        {
            lülita_sisse(sender, e);
        }
        else if (nupp.ZIndex == 1)
        {
            lülita_välja(sender, e);
        }
        else if (nupp.ZIndex == 2 && foor_on == true)
        {
            öö_reziim(sender, e);
        }
        else if (nupp.ZIndex == 3 && foor_on == true)
        {
            automaat_reziim(sender, e);
        }
        else if (nupp.ZIndex == 4 && foor_on == true)
        {
            jalakäia_reziim(sender, e);
        }
    }
    // Lülitab foori sisse
    private void lülita_sisse(object? sender, EventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = false;
        foor_on = true;

        if (!vsl.Children.Contains(hsl_nupud_rida2))
        {
            vsl.Children.Add(hsl_nupud_rida2);
        }

        pealkiri_lbl.Text = "Vali valgus!";
        pealkiri_lbl.FontSize = 40;
        BackgroundImageSource = "background.jpg";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);
        punane_foor.BackgroundColor = punane;
        kollane_foor.BackgroundColor = kollane;
        roheline_foor.BackgroundColor = roheline;
    }
    // Lülitab foori välja
    private void lülita_välja(object? sender, EventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = false;
        foor_on = false;

        if (vsl.Children.Contains(hsl_nupud_rida2))
        {
            vsl.Children.Remove(hsl_nupud_rida2);
        }

        pealkiri_lbl.Text = "Lülita foor sisse!";
        pealkiri_lbl.FontSize = 28;
        BackgroundImageSource = "background_night.jpg";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);
        punane_foor.BackgroundColor = hall;
        kollane_foor.BackgroundColor = hall;
        roheline_foor.BackgroundColor = hall;
    }
    // Punase foori klikk
    private async void click_punane(object? sender, TappedEventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = false;

        if (foor_on == true)
        {
            punane_foor.BackgroundColor = punane;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = hall;
            pealkiri_lbl.Text = "Seisa!";
            pealkiri_lbl.FontSize = 40;
            lbl_frame.BackgroundColor = Color.FromRgba(255, 0, 0, 130);

            await Task.WhenAll(
                punane_foor.ScaleTo(1.2, 150),
                punane_foor.FadeTo(0.5, 150)
            );
            await Task.WhenAll(
                punane_foor.ScaleTo(1.0, 150),
                punane_foor.FadeTo(1.0, 150)
            );
        }
    }
    // Kollase foori klikk
    private async void click_kollane(object? sender, TappedEventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = false;

        if (foor_on == true)
        {
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = kollane;
            roheline_foor.BackgroundColor = hall;
            pealkiri_lbl.Text = "Valmista";
            pealkiri_lbl.FontSize = 40;
            lbl_frame.BackgroundColor = Color.FromRgba(216, 216, 0, 130);

            await Task.WhenAll(
                kollane_foor.ScaleTo(1.2, 150),
                kollane_foor.FadeTo(0.5, 150)
            );
            await Task.WhenAll(
                kollane_foor.ScaleTo(1.0, 150),
                kollane_foor.FadeTo(1.0, 150)
            );
        }
    }
    // Rohelise foori klikk
    private async void click_roheline(object? sender, TappedEventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = false;

        if (foor_on == true)
        {
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = roheline;
            pealkiri_lbl.Text = "Sõida!";
            pealkiri_lbl.FontSize = 40;
            lbl_frame.BackgroundColor = Color.FromRgba(0, 255, 0, 130);

            await Task.WhenAll(
                roheline_foor.ScaleTo(1.2, 150),
                roheline_foor.FadeTo(0.5, 150)
            );
            await Task.WhenAll(
                roheline_foor.ScaleTo(1.0, 150),
                roheline_foor.FadeTo(1.0, 150)
            );
        }
    }
    // Öö režiim
    private async void öö_reziim(object? sender, EventArgs e)
    {
        päev = false;
        jalakäia = false;
        öö = true;

        BackgroundImageSource = "background_night.jpg";
        pealkiri_lbl.FontSize = 40;
        pealkiri_lbl.Text = "Öörežiim";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);

        while (öö == true)
        {
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(1000);
            if (öö != true)
            {
                break;
            }
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = kollane;
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(1000);
            if (öö != true)
            {
                break;
            }
        }
    }
    // Automaatrežiim
    private async void automaat_reziim(object? sender, EventArgs e)
    {
        öö = false;
        jalakäia = false;
        päev = true;

        kollane_lbl.Text = "";
        pealkiri_lbl.FontSize = 40;
        pealkiri_lbl.Text = "Päevarežiim";
        BackgroundImageSource = "background.jpg";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);

        while (päev == true)
        {
            punane_foor.BackgroundColor = punane;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(4000);
            if (päev != true)
            {
                break;
            }
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = kollane;
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(2000);
            if (päev != true)
            {
                break;
            }
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = roheline;
            await Task.Delay(4000);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = roheline;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = roheline;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = hall;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
            roheline_foor.BackgroundColor = roheline;
            await Task.Delay(500);
            if (päev != true)
            {
                break;
            }
        }
    }
    // Oma reziim - jalakäia režiim koos taimeriga
    private async void jalakäia_reziim(object? sender, EventArgs e)
    {
        päev = false;
        öö = false;
        jalakäia = true;

        BackgroundImageSource = "background.jpg";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);

        while (jalakäia == true)
        {
            pealkiri_lbl.Text = "Seisa!";
            lbl_frame.BackgroundColor = Color.FromRgba(255, 0, 0, 130);
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = hall;
            punane_foor.Content = new Image { Source = "red_man.png", Aspect = Aspect.AspectFit };
            for (int i = 15; i >= 1; i--)
            {
                kollane_lbl.TextColor = punane;
                kollane_lbl.Text = i.ToString();
                await Task.Delay(1000);
                if (jalakäia != true)
                {
                    break;
                }
            }
            if (jalakäia != true)
            {
                break;
            }
            punane_foor.Content = null;

            pealkiri_lbl.Text = "Mine!";
            lbl_frame.BackgroundColor = Color.FromRgba(0, 255, 0, 130);
            punane_foor.BackgroundColor = hall;
            kollane_foor.BackgroundColor = hall;
            roheline_foor.BackgroundColor = hall;
            roheline_foor.Content = new Image { Source = "green_man.png", Aspect = Aspect.AspectFit };
            for (int i = 8; i >= 1; i--)
            {
                kollane_lbl.TextColor = roheline;
                kollane_lbl.Text = i.ToString();
                await Task.Delay(1000);
                if (jalakäia != true)
                {
                    break;
                }
            }
            if (jalakäia != true)
            {
                break;
            }
            roheline_foor.Content = null;
        }
        roheline_foor.Content = null;
        punane_foor.Content = null;
        kollane_lbl.Text = "";
        lbl_frame.BackgroundColor = Color.FromRgba(0, 0, 139, 130);
    }
}