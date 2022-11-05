using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace ViewModels.Models
{
    public class MainPageControls : ReactiveObject
    {
        private bool hamburgerMenuIsActive = false;
        private List<RowTable> initialDataForProcessing = new List<RowTable>();
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
    }
}
