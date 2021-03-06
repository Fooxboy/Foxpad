﻿using Foxpad.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Foxpad.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NotepadView : Page
    {

        public NotepadViewModel ViewModel { get; set; }

        public NotepadView()
        {
            this.InitializeComponent();
            ViewModel = new NotepadViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var file = (StorageFile)e.Parameter;

            await ViewModel.SetStorageFile(file);
        }
    }
}
