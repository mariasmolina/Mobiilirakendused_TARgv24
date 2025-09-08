namespace Mobiilirakendused_TARgv24;

public partial class TekstPage : ContentPage
{
	Label lblTekst;
	Editor editorTekst;
	HorizontalStackLayout hsl;
	public TekstPage()
	{
		lblTekst = new Label
		{
			Text = "Tekst: ",
			FontSize = 20,
			TextColor = Colors.Black,
			FontFamily = "Asimovian-Regular"
        };
		editorTekst = new Editor
		{
			FontSize = 20,
			TextColor = Colors.Black,
			FontFamily = "Asimovian-Regular",
			AutoSize = EditorAutoSizeOption.TextChanges,
			BackgroundColor = Color.FromRgb(255, 255, 255),
			Placeholder = "Sisesta tuleb tekst",
			PlaceholderColor = Color.FromRgb(150, 150, 150),
			FontAttributes = FontAttributes.Italic,
        };
		editorTekst.TextChanged += EditorTekst_TextChanged;
        hsl = new HorizontalStackLayout
		{
			Margin = 10,
			BackgroundColor = Color.FromRgb(220, 220, 220),
            Children = { lblTekst, editorTekst },
			HorizontalOptions = LayoutOptions.Center
		};
		Content = hsl;
    }
	private void EditorTekst_TextChanged(object? sender, TextChangedEventArgs e)
	{
		lblTekst.Text = editorTekst.Text;
    }
}