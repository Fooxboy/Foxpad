using Foxpad.Helpers;
using Foxpad.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Foxpad.ViewModels
{
    public class NotepadViewModel:BaseViewModel
    {
        NotepadViewModel()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveAsFileCommand = new RelayCommand(SaveAsFile);

            _fileService = new FileService();
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

            Changed("Text");
        }

        private async void SaveFile()
        {
            if(_saveFile is null)
            {
                var docFolder = KnownFolders.DocumentsLibrary;
                var file = await docFolder.CreateFileAsync($"Foxpad document {DateTime.Now.Minute}:{DateTime.Now.Second}");
                _saveFile = file;
            }
            await _fileService.WriteTextInFile(Text, _saveFile);
        }

        private async void SaveAsFile()
        {
            var file = await _fileService.OpenSaveFilePicker();
            if (file is null) return;

            _saveFile = file;
            var f = _fileService.WriteTextInFile(Text, file);
        }

    }
}
