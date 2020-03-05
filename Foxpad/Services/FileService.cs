using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Foxpad.Services
{
    public class FileService
    {
        public async Task<StorageFile> OpenFilePicker()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".rtf");

            return await picker.PickSingleFileAsync();
        }

        public async Task<string> ReadAllText(StorageFile file)
        {
            if (file is null) return null;

            var text = await FileIO.ReadTextAsync(file);
            return text;
        }

        public async Task<StorageFile> OpenSaveFilePicker()
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("FoxpadDoc", new List<string>() { ".txt", ".rtf" });
            savePicker.SuggestedFileName = "Foxpad document";
            var file = await savePicker.PickSaveFileAsync();
            return file;
        }

        public async Task<bool> WriteTextInFile(string text, StorageFile file)
        {
            if (text is null || file is null) return false;

            try
            {
                await FileIO.WriteTextAsync(file, text);
                return true;

            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
