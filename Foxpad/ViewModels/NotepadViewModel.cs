using Foxpad.Helpers;
using Foxpad.Services;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Foxpad.ViewModels
{
    public class NotepadViewModel:BaseViewModel
    {
        public NotepadViewModel()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveAsFileCommand = new RelayCommand(SaveAsFile);

            _fileService = new FileService();

            NameDocument = "Foxpad document.txt";
        }

        public string Text { get; set; }
        public string NameDocument { get; set; }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand SaveFileCommand { get; }
        public RelayCommand SaveAsFileCommand { get; }
        public long CountCharacters { get; set; }
        private StorageFile _saveFile;

        private FileService _fileService { get; set; }

        private async void OpenFile()
        {
            var file = await _fileService.OpenFilePicker();
            if (file is null) return;

            var text = await _fileService.ReadAllText(file);
            Text = this.Text = text;

            NameDocument = file.DisplayName + "." + file.FileType;
            Changed("NameDocument");

            Changed("Text");
        }

        private async void SaveFile()
        {
            if(_saveFile is null)
            {
                var docFolder = KnownFolders.DocumentsLibrary;
                var file = await docFolder.CreateFileAsync($"Foxpad document {DateTime.Now.Minute}-{DateTime.Now.Second}.txt");
                _saveFile = file;
            }
            await _fileService.WriteTextInFile(Text, _saveFile);

            NameDocument = _saveFile.DisplayName + "." + _saveFile.FileType;

            Changed("NameDocument");
        }

        private async void SaveAsFile()
        {
            var file = await _fileService.OpenSaveFilePicker();
            if (file is null) return;

            _saveFile = file;
            var f = _fileService.WriteTextInFile(Text, file);

            NameDocument = file.DisplayName + "." + file.FileType;

            Changed("NameDocument");
        }

        public void TextChanged(object sender, TextChangedEventArgs e)
        {
            CountCharacters = Text.Length;
            Changed("CountCharacters");
        }

    }
}
