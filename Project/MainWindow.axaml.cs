using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
namespace AvaloniaUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        inputCommand.KeyUp += TextBox_KeyEnter;
    }

    private async void TextBox_KeyEnter(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            TextBox textBox = (sender as TextBox)!;
            
            var result = await TerminalExec.ExecuteAsync(textBox.Text!);
            if (result.ExitCode == 0)
            {
                output.Inlines!.Clear();
                var inline = new Run(result.StandardOutput) { Foreground = Brushes.Green };
                output.Inlines.Add(inline);
            }
            else
            {
                output.Inlines!.Clear();
                var inline1 = new Run(result.StandardError) { Foreground = Brushes.Red };
                var inline2 = new Run($"\nExit code: {result.ExitCode}") { Foreground = Brushes.Orange };
                output.Inlines.Add(inline1);
                output.Inlines.Add(inline2);
            }
        }
    }
}