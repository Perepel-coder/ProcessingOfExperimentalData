using ReactiveUI;
using System.Collections.Generic;

namespace ViewModels.Models
{
    public class MainPageControls : ReactiveObject
    {
        private bool hamburgerMenuIsActive = false;
        private int columForStatisticalAnalysis;
        private int columForFactorsOne = 1;
        private int columForFactorsTwo = 2;
        private int numberDegreesFreedom;
        private List<RowTable> initialDataForProcessing = new List<RowTable>();
        private List<RowTable> statisticalAnalysisData = new List<RowTable>();
        private List<RowTable> correlationsOfFactors = new List<RowTable>();

        public class RowTable : ReactiveObject
        {
            private object[] value;
            public object[] Value
            {
                get { return this.value; }
                set { this.RaiseAndSetIfChanged(ref this.value, value); }
            }
        }
        public bool HamburgerMenuIsActive
        {
            get { return hamburgerMenuIsActive; }
            set { this.RaiseAndSetIfChanged(ref hamburgerMenuIsActive, !hamburgerMenuIsActive); }
        }

        public List<RowTable> InitialDataForProcessing
        {
            get { return initialDataForProcessing; }
            set { this.RaiseAndSetIfChanged(ref initialDataForProcessing, value); }
        }
        public List<RowTable> StatisticalAnalysisData
        {
            get { return statisticalAnalysisData; }
            set { this.RaiseAndSetIfChanged(ref statisticalAnalysisData, value); }
        }
        public List<RowTable> CorrelationsOfFactors
        {
            get { return correlationsOfFactors; }
            set { this.RaiseAndSetIfChanged(ref correlationsOfFactors, value); }
        }
        public int ColumForStatisticalAnalysis
        {
            get { return columForStatisticalAnalysis; }
            set { 
                this.RaiseAndSetIfChanged(ref columForStatisticalAnalysis, value); 
            }
        }
        public int ColumForFactorsOne
        {
            get { return columForFactorsOne; }
            set { this.RaiseAndSetIfChanged(ref columForFactorsOne, value); }
        }
        public int ColumForFactorsTwo
        {
            get { return columForFactorsTwo; }
            set { this.RaiseAndSetIfChanged(ref columForFactorsTwo, value); }
        }
        public int NumberDegreesFreedom
        {
            get { return numberDegreesFreedom; }
            set { this.RaiseAndSetIfChanged(ref numberDegreesFreedom, value); }
        }
    }
}
