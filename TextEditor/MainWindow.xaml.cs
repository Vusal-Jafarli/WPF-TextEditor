using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection.Metadata;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool auto_save = false;
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedText = text.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
                Clipboard.SetText(selectedText);
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
                text.Paste();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Select_all(object sender, RoutedEventArgs e)
        {
            text.SelectAll();
            Clipboard.SetText(text.SelectedText);
            SolidColorBrush selectionColor = (SolidColorBrush)text.SelectionBrush;
            text.SelectionBrush = selectionColor;
        }

        private void cut_Click(object sender, RoutedEventArgs e)
        {
            if (text.SelectionLength > 0)
                text.Text = text.Text.Remove(text.SelectionStart, text.SelectionLength);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string textToSave = text.Text;

                File.WriteAllText(filePath, textToSave);
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text File|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = System.IO.File.ReadAllText(filePath);

                text.Text = fileContent;
                file_open_txt.Text = openFileDialog.FileName;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_box.IsChecked == false)
                auto_save = false;

            if (auto_save)
                SaveTextToFile();
        }

        private void SaveTextToFile()
        {
            string? autoSaveFilePath = null;
            if (file_open_txt.Text != "")
            {
                autoSaveFilePath = file_open_txt.Text;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    autoSaveFilePath = saveFileDialog.FileName;
                    file_open_txt.Text = saveFileDialog.FileName;

                }
            }
            File.WriteAllText(autoSaveFilePath, text.Text);
        }
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (check_box.IsChecked == true)
            {
                SaveTextToFile();
                auto_save = true;
            }
            else
                auto_save = false;
        }
    }
}
//ByVC