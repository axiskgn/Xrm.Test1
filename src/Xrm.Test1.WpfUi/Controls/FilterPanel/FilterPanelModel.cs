using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Repositories.Common;
using Xrm.Test1.WpfUi.Annotations;
using Xrm.Test1.WpfUi.Common;

namespace Xrm.Test1.WpfUi.Controls.FilterPanel
{
    public class FilterPanelModel:INotifyPropertyChanged
    {
        private readonly IReadAcces _ra;

        public FilterPanelModel([NotNull] IReadAcces ra)
        {
            if (ra == null) throw new ArgumentNullException("ra");
            _ra = ra;
            LoadData();
            ClearFilterBtn = new RelayCommand(delegate(object o)
            {
                IsCity = false;
                IsEducation = false;
                IsHeader = false;
                IsScheduleWork = false;
                IsWorkingType = false;
            });
        }

        public void LoadData()
        {
            CityList = _ra.DictionaryRepository.GetAll<ICity>().OrderBy(t=>t.Name).ToList();
            EducationList = _ra.DictionaryRepository.GetAll<IEducation>().OrderBy(t => t.Name).ToList();
            WorkingTypeList = _ra.DictionaryRepository.GetAll<IWorkingType>().OrderBy(t => t.Name).ToList();
            ScheduleWorkList = _ra.DictionaryRepository.GetAll<IScheduleWork>().OrderBy(t => t.Name).ToList();
        }
        //IScheduleWork

        public ICommand ClearFilterBtn { get; set; }

        #region ScheduleWork

        private bool _isScheduleWork;
        private IList<IScheduleWork> _scheduleWorkList;
        private object _scheduleWorkFilter;

        public bool IsScheduleWork
        {
            get { return _isScheduleWork; }
            set
            {
                if (value.Equals(_isScheduleWork)) return;
                _isScheduleWork = value;
                OnPropertyChanged("IsScheduleWork");
            }
        }

        public IList<IScheduleWork> ScheduleWorkList
        {
            get { return _scheduleWorkList; }
            set
            {
                if (Equals(value, _scheduleWorkList)) return;
                _scheduleWorkList = value;
                OnPropertyChanged("ScheduleWorkList");
            }
        }

        public object ScheduleWorkFilter
        {
            get { return _scheduleWorkFilter; }
            set
            {
                if (Equals(value, _scheduleWorkFilter)) return;
                _scheduleWorkFilter = value;
                OnPropertyChanged("ScheduleWorkFilter");
            }
        }

        #endregion

        #region WorkingType

        private bool _isworkingType;
        private IList<IWorkingType> _workingTypeList;
        private object _workingTypeFilter;

        public bool IsWorkingType
        {
            get { return _isworkingType; }
            set
            {
                if (value.Equals(_isworkingType)) return;
                _isworkingType = value;
                OnPropertyChanged("IsWorkingType");
            }
        }

        public IList<IWorkingType> WorkingTypeList
        {
            get { return _workingTypeList; }
            set
            {
                if (Equals(value, _workingTypeList)) return;
                _workingTypeList = value;
                OnPropertyChanged("WorkingTypeList");
            }
        }

        public object WorkingTypeFilter
        {
            get { return _workingTypeFilter; }
            set
            {
                if (Equals(value, _workingTypeFilter)) return;
                _workingTypeFilter = value;
                OnPropertyChanged("WorkingTypeFilter");
            }
        }

        #endregion

        #region Education

        private bool _isEducation;
        private IList<IEducation> _educationList;
        private object _educationFilter;

        public bool IsEducation
        {
            get { return _isEducation; }
            set
            {
                if (value.Equals(_isEducation)) return;
                _isEducation = value;
                OnPropertyChanged("IsEducation");
            }
        }

        public IList<IEducation> EducationList
        {
            get { return _educationList; }
            set
            {
                if (Equals(value, _educationList)) return;
                _educationList = value;
                OnPropertyChanged("EducationList");
            }
        }

        public object EducationFilter
        {
            get { return _educationFilter; }
            set
            {
                if (Equals(value, _educationFilter)) return;
                _educationFilter = value;
                OnPropertyChanged("EducationFilter");
            }
        }

        #endregion

        #region City

        private bool _isCity;
        private IList<ICity> _cityList;
        private object _cityFilter;

        public bool IsCity
        {
            get { return _isCity; }
            set
            {
                if (value.Equals(_isCity)) return;
                _isCity = value;
                OnPropertyChanged("IsCity");
            }
        }

        public IList<ICity> CityList
        {
            get { return _cityList; }
            set
            {
                if (Equals(value, _cityList)) return;
                _cityList = value;
                OnPropertyChanged("CityList");
            }
        }

        public object CityFilter
        {
            get { return _cityFilter; }
            set
            {
                if (Equals(value, _cityFilter)) return;
                _cityFilter = value;
                OnPropertyChanged("CityFilter");
            }
        }

        #endregion
        
        #region Header

        private bool _isHeader;
        private string _headerFiler;

        public bool IsHeader
        {
            get { return _isHeader; }
            set
            {
                if (value.Equals(_isHeader)) return;
                _isHeader = value;
                OnPropertyChanged("IsHeader");
            }
        }

        public string HeaderFiler
        {
            get { return _headerFiler; }
            set
            {
                if (value == _headerFiler) return;
                _headerFiler = value;
                OnPropertyChanged("HeaderFiler");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
