using MediatR;
using QueryCQRS.Queries;
using System;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.IO;
using System.Windows.Input;
using ViewModels.Models;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using Windows.UI.Xaml;
using System.Data;

namespace ViewModels.ViewModels
{
    public class MainViewModel
    {
        private readonly IMediator mediatR;
        public MainPageControls PageControls { get; set; }
        public MainViewModel(IMediator mediatR)
        {
            this.PageControls = new MainPageControls();
            this.mediatR = mediatR;
        }

        private RelayCommand hamburgerMenuIsActive;
        private RelayCommand getInputDataFromFile;
        private RelayCommand getStatisticalAnalysis;

        #region Commands
        public ICommand HamburgerMenuIsActive
        {
            get
            {
                return hamburgerMenuIsActive ??
                  (hamburgerMenuIsActive = new RelayCommand(obj =>
                  {
                      PageControls.HamburgerMenuIsActive = true;
                  }));
            }
        }
        public ICommand GetInputDataFromFile
        {
            get
            {
                return getInputDataFromFile ??
                  (getInputDataFromFile = new RelayCommand(async obj =>
                  {
                      var openPicker = new FileOpenPicker();
                      openPicker.ViewMode = PickerViewMode.Thumbnail;
                      openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                      openPicker.FileTypeFilter.Add(".xls");
                      openPicker.FileTypeFilter.Add(".xlsx");
                      var file = await openPicker.PickSingleFileAsync();
                      if(file == null) { return; }
                      PageControls.InitialDataForProcessing.Clear();
                      Stream stream = await file.OpenStreamForReadAsync();
                      var getInputDataRESPONSE = await this.mediatR.Send(new GetInputDataREQUEST(fileStream: stream));
                      stream.Close();
                      foreach (DataRow row in getInputDataRESPONSE.InputData.Rows)
                      {
                          PageControls.InitialDataForProcessing.Add(new MainPageControls.RowTable { Value = row.ItemArray });
                      }
                      #region Creat DataGrid from DataTable
                      var columns = (ICollection<DataGridColumn>)obj; columns.Clear();

                      BindingMode mode = BindingMode.TwoWay;
                      UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged;

                      for (int i = 0; i < getInputDataRESPONSE.InputData.Columns.Count; i++)
                      {
                          var column = new DataGridTextColumn()
                          {
                              IsReadOnly=false,
                              Header = getInputDataRESPONSE.InputData.Columns[i].ColumnName,
                              Binding = new Binding()
                              {
                                  Path = new PropertyPath($"Value[{i}]"),
                                  Mode = mode,
                                  UpdateSourceTrigger = trigger
                              }
                          };
                          columns.Add(column);
                      }
                      #endregion
                  }));
            }
        }
        public ICommand GetStatisticalAnalysis
        {
            get
            {
                return getStatisticalAnalysis ??
                  (getStatisticalAnalysis = new RelayCommand(obj =>
                  {
                      
                  }));
            }
        }
        #endregion
    }
}