using Autofac;
using ViewModels.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data;
using Windows.UI.Xaml;
using System.Collections;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ProcessingOfExperimentalData.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Container.Resolve<MainViewModel>();
        }
    }
}
