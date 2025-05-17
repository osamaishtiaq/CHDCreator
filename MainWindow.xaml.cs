using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace CHDConverter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog
        {
            Filter = "GDI or CUE files (*.gdi;*.cue)|*.gdi;*.cue",
            Title = "Select GDI or CUE file"
        };

        if (dlg.ShowDialog() == true)
        {
            InputFilePath.Text = dlg.FileName;
            OutputFilePath.Text = Path.ChangeExtension(dlg.FileName, ".chd");
        }
    }

    private async void Convert_Click(object sender, RoutedEventArgs e)
    {
        string chdmanPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chdman/chdman.exe");

        if (!File.Exists(chdmanPath))
        {
            MessageBox.Show("chdman.exe not found in application directory.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        string input = InputFilePath.Text;
        string output = OutputFilePath.Text;

        if (string.IsNullOrWhiteSpace(input) || !File.Exists(input))
        {
            MessageBox.Show("Please select a valid .gdi or .cue file.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        StatusText.Text = "Converting, please wait...";
        StatusText.Foreground = System.Windows.Media.Brushes.Black;


        DisableControlsAndStartProgressBar();

        ConsoleOutput.Text = ""; // Clear previous output

        try
        {
            var result = await Task.Run(() =>
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = chdmanPath,
                        Arguments = $"createcd -i \"{input}\" -o \"{output}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };


                process.OutputDataReceived += (s, ea) =>
                {
                    if (ea.Data != null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ConsoleOutput.AppendText(ea.Data + Environment.NewLine);
                            ConsoleOutput.ScrollToEnd();
                        });
                    }
                };

                process.ErrorDataReceived += (s, ea) =>
                {
                    if (ea.Data != null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ConsoleOutput.AppendText(ea.Data + Environment.NewLine);
                            ConsoleOutput.ScrollToEnd();
                        });
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                return new { ExitCode = process.ExitCode };
            });

            if (result.ExitCode == 0)
            {
                StatusText.Text = $"✅ Success: CHD created at {output}";
                StatusText.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                StatusText.Text = $"❌ Failure";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Failed to run chdman: {ex.Message}";
            StatusText.Foreground = System.Windows.Media.Brushes.Red;
        }
        finally
        {
            EnableControlsAndStopProgressBar();
        }
    }


    private void EnableControlsAndStopProgressBar()
    {
        ConvertButton.IsEnabled = true;
        ProgressBar.IsIndeterminate = false;
        FileBrowseButton.IsEnabled = true;
    }

    private void DisableControlsAndStartProgressBar()
    {
        ConvertButton.IsEnabled = false;
        FileBrowseButton.IsEnabled = false;
        ProgressBar.IsIndeterminate = true;
    }
}