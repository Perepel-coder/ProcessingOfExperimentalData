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
using Autofac;

namespace ViewModels.ViewModels
{
    public class MainViewModel
    {
        private readonly IMediator mediatR;
        private readonly IContainer container;
        public MainPageControls PageControls { get; set; }
        public MainViewModel(IMediator mediatR, IContainer container)
        {
            this.PageControls = new MainPageControls();
            this.mediatR = mediatR;
            this.container = container;
        }

        private RelayCommand hamburgerMenuIsActive;
        private RelayCommand getInputDataFromFile;
        private RelayCommand getStatisticalAnalysis;
        private RelayCommand getCorrelationsOfFactors;
        private RelayCommand saveInputDataAs;
        private RelayCommand saveResultAs;

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

                      PageControls.NumberDegreesFreedom = getInputDataRESPONSE.InputData.Columns.Count - 1;
                      PageControls.ColumForStatisticalAnalysis = getInputDataRESPONSE.InputData.Columns.Count;
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
                  (getStatisticalAnalysis = new RelayCommand(async obj =>
                  {
                      if(PageControls.InitialDataForProcessing.Count == 0) { return; }
                      List<double> samples = new List<double>();
                      foreach(var el in PageControls.InitialDataForProcessing)
                      {
                          samples.Add(double.Parse((string)el.Value[PageControls.ColumForStatisticalAnalysis-1]));
                      }
                      var GetResultStatisticalAnalysisRESPONSE = await this.mediatR.Send
                      (new GetResultStatisticalAnalysisREQUEST(container: this.container, samples: samples, degreesOfFreedom: PageControls.NumberDegreesFreedom));
                      if(GetResultStatisticalAnalysisRESPONSE == null) { return; } PageControls.StatisticalAnalysisData.Clear();

                      foreach (DataRow row in GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Rows)
                      {
                          PageControls.StatisticalAnalysisData.Add(new MainPageControls.RowTable { Value = row.ItemArray });
                      }
                      #region Creat DataGrid from DataTable
                      var columns = (ICollection<DataGridColumn>)obj; columns.Clear();

                      BindingMode mode = BindingMode.TwoWay;
                      UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged;

                      for (int i = 0; i < GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Columns.Count; i++)
                      {
                          var column = new DataGridTextColumn()
                          {
                              IsReadOnly = false,
                              Header = GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Columns[i].ColumnName,
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
        public ICommand GetCorrelationsOfFactors
        {
            get
            {
                return getCorrelationsOfFactors ??
                  (getCorrelationsOfFactors = new RelayCommand(async obj =>
                  {
                      if (PageControls.InitialDataForProcessing.Count == 0) { return; }
                      List<double> firstFactor = new List<double>();
                      List<double> secondFactor = new List<double>();
                      foreach (var el in PageControls.InitialDataForProcessing)
                      {
                          firstFactor.Add(double.Parse((string)el.Value[PageControls.ColumForFactorsOne - 1]));
                          secondFactor.Add(double.Parse((string)el.Value[PageControls.ColumForFactorsTwo - 1]));
                      }
                      var GetResultStatisticalAnalysisRESPONSE = await this.mediatR.Send
                     (new GetResultStatisticalAnalysisREQUEST(container: this.container, firstFactor: firstFactor, secondFactor: secondFactor));
                      if (GetResultStatisticalAnalysisRESPONSE == null) { return; } PageControls.CorrelationsOfFactors.Clear();

                      foreach (DataRow row in GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Rows)
                      {
                          PageControls.CorrelationsOfFactors.Add(new MainPageControls.RowTable { Value = row.ItemArray });
                      }

                      #region Creat DataGrid from DataTable
                      var columns = (ICollection<DataGridColumn>)obj; columns.Clear();

                      BindingMode mode = BindingMode.TwoWay;
                      UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged;

                      for (int i = 0; i < GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Columns.Count; i++)
                      {
                          var column = new DataGridTextColumn()
                          {
                              IsReadOnly = false,
                              Header = GetResultStatisticalAnalysisRESPONSE.ReportStatisticalAnalysis.Columns[i].ColumnName,
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
        public ICommand SaveInputDataAs
        {
            get
            {
                return saveInputDataAs ??
                  (saveInputDataAs = new RelayCommand(obj =>
                  {
                      //PageControls.HamburgerMenuIsActive = true;
                  }));
            }
        }
        public ICommand SaveResultAs
        {
            get
            {
                return saveResultAs ??
                  (saveResultAs = new RelayCommand(obj =>
                  {
                      //PageControls.HamburgerMenuIsActive = true;
                  }));
            }
        }
        #endregion
    }
}