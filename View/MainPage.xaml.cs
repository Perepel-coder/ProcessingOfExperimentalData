using Autofac;
using System;
using ViewModels.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

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
            this.DataContext = App.Container.Resolve<MainViewModel>(new NamedParameter("container", App.Container));
        } 
    }
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int num;
            if (int.TryParse((string)value, out num))
            {
                return num;
            }
            return 0;
        }
    }
}
