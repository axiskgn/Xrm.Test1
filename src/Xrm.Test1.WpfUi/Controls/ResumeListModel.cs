using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.WpfUi.Annotations;
using Xrm.Test1.WpfUi.Common;

namespace Xrm.Test1.WpfUi.Controls
{
    public class ResumeListModel:INotifyPropertyChanged
    {
        private IList<ResumeShortModel> _resumeList;
        private bool _isFilterShow;
        private UserControl _filterPanel;

        public ResumeListModel()
        {
            HeaderShortCmd = new RelayCommand(delegate(object o)
            {
                _resumeList = _resumeList.OrderBy(t => t.Head).ToList();
                OnPropertyChanged("ResumeList");
            });
            NameShortCmd = new RelayCommand(delegate(object o)
            {
                _resumeList = _resumeList.OrderBy(t => t.Name).ToList();
                OnPropertyChanged("ResumeList");
            });
            SalaryShortCmd = new RelayCommand(delegate(object o)
            {
                _resumeList = _resumeList.OrderBy(t => t.BaseResume.WantedSalaryRub).ToList();
                OnPropertyChanged("ResumeList");
            });

            FilterShowBtn = new RelayCommand(delegate(object o)
            {
                _isFilterShow = !_isFilterShow;
                OnPropertyChanged("FilterPanelVisibility");
            });
        }

        public void SetList(IList<IResume> resumeList)
        {
            _resumeList = resumeList.Select(t => new ResumeShortModel(t,OnSelectResume)).ToList();
            OnPropertyChanged("ResumeList");

        }

        private void OnSelectResume(IResume resume)
        {
            var handler = SelectedResume;
            if (handler != null)
            {
                handler(resume);
            }
        }

        public event Action<IResume> SelectedResume;

        public UserControl FilterPanel
        {
            get { return _filterPanel; }
            set
            {
                _filterPanel = value;
                OnPropertyChanged("FilterPanel");
            }
        }

        public ICommand HeaderShortCmd { get; private set; }
        public ICommand NameShortCmd { get; private set; }
        public ICommand SalaryShortCmd { get; private set; }

        public ICommand DownloadCmd { get; set; }

        public ICommand FilterShowBtn { get; private set; }

        public Visibility FilterPanelVisibility
        {
            get { return _isFilterShow ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IList<ResumeShortModel> ResumeList
        {
            get { return _resumeList; }
        }

        public class ResumeShortModel
        {
            public IResume BaseResume { get; private set; }
            private readonly Action<IResume> _selectAction;

            public ResumeShortModel(IResume baseResume, Action<IResume> selectAction)
            {
                if (baseResume == null) throw new ArgumentNullException("baseResume");
                BaseResume = baseResume;
                _selectAction = selectAction;
                SelectCommand = new RelayCommand(delegate(object o)
                {
                    _selectAction(BaseResume);
                });
            }

            public string Name{get { return BaseResume.Person.Fio; }}
            public string Head {get { return BaseResume.Header; }}
            public string Salary {get { return BaseResume.WantedSalaryRub.ToString(); }}
            public ICommand SelectCommand { get; private set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}