using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HeSaysGif.GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void GenerationButton_OnClick(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog window = new();
        string[]? result = window.ShowAsync(this).GetAwaiter().GetResult();

        if (result == null)
            return;

        string selectedImagePath = result[0];

        // runs the CLI program on the selected file
        global::Program.Main(selectedImagePath);

        GenerationResultTextBlock.Text = $"Created GIF: {selectedImagePath}";
        GenerationResultTextBlock.TextDecorations = new()
        {
            new TextDecoration()
            {
                Location = TextDecorationLocation.Underline,
                Stroke = Brushes.LightBlue
            }
        };
    }

    private void GenerationResultTextBlock_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        TextBlock block = (TextBlock) sender!;
        if (string.IsNullOrEmpty(block.Text))
            return;

        string path = block.Text[13..];
        
        try
        {
            if (Path.GetDirectoryName(path) is { } directoryPath)
                Process.Start(directoryPath);
        }
        catch
        {
            // no access to open directory. oh well
        }
    }
}