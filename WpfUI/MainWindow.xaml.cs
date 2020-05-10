using Files;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CopyFilesParameters copyFilesParameters { get; set; } = new CopyFilesParameters();

        private IFolder Folder { get; set; }
        public MainWindow(IFolder folder)
        {
            InitializeComponent();
            DataContext = this;
            Folder = folder;
        }
        private async void CopyFiles(object sender, RoutedEventArgs e)
        {
            copyFilesParameters.searchOption = (System.IO.SearchOption)cmbDirectorySearchoptions.SelectedItem;
            await Task.Run(() => Folder.CopyFiles(copyFilesParameters, ShowInfo));
        }

        private void GetSourceFolder(object sender, RoutedEventArgs e)
        {
            copyFilesParameters.sourceFolder = GetFolderPath();

            SetInfoText();
        }

        private string GetFolderPath()
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return string.Empty;
        }

        private void GetInputFile(object sender, RoutedEventArgs e)
        {
            using var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                copyFilesParameters.extension = $"*{Path.GetExtension(dialog.FileName)}";
            }

            SetInfoText();
        }

        private void SetInfoText()
        {
            Info.Text = $"Copy {copyFilesParameters.extension} files  from {copyFilesParameters.sourceFolder} to {copyFilesParameters.targetFolder}";
            ButtonCopy.IsEnabled = true;

            ButtonCopy.IsEnabled = copyFilesParameters.CanCopy();
            if (copyFilesParameters.sourceFolder == copyFilesParameters.targetFolder)
            {
                ButtonCopy.IsEnabled = false;
                Info.Text = $"Source {copyFilesParameters.sourceFolder} cannot be the same as target {copyFilesParameters.targetFolder}";
            }
        }

        private void GetTargetFolder(object sender, RoutedEventArgs e)
        {
            copyFilesParameters.targetFolder = GetFolderPath();
            SetInfoText();
        }

        private void ShowInfo(string info)
        {
            this.Dispatcher.Invoke(() =>
             {
                 Info.Text = $"{  Info.Text}{Environment.NewLine}{info}";
             });
        }
    }
}

