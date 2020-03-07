using Foxpad.Helpers;
using Foxpad.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Foxpad.ViewModels
{
    public class WelcomeViewModel:BaseViewModel
    {

        private FileService _fileService;
        private Frame _frame;

        public WelcomeViewModel()
        {
            _fileService = new FileService();
            _frame = Window.Current.Content as Frame;

        }

        public RelayCommand CreateDocumentCommand { get; }
        public RelayCommand OpenDocumentCommand { get; }


        private void CreateDocument()
        {
            _frame.Navigate(typeof(NotepadViewModel));
        }

        public async void OpenDocument()
        {
            var file = await _fileService.OpenFilePicker();

            _frame.Navigate(typeof(NotepadViewModel), file);
        }
    }
}
