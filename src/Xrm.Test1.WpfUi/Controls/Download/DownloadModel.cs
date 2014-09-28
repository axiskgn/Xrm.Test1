using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Xrm.Test1.WpfUi.Annotations;

namespace Xrm.Test1.WpfUi.Controls.Download
{
    public class DownloadModel:INotifyPropertyChanged
    {
        private int _progress;
        private int _newPersonCount;
        private int _newResumeCount;
        private TimeSpan _downloadTime;
        private TimeSpan _mergeTime;
        private TimeSpan _allTime;

        public int Progress
        {
            get { return _progress; }
            set
            {
                if (value == _progress) return;
                _progress = value;
                OnPropertyChanged("Progress");
                OnPropertyChanged("CloseBtnVisibility");
            }
        }

        public ICommand CloseCmd { get; set; }

        public Visibility CloseBtnVisibility
        {
            get { return Progress >= 100 ? Visibility.Visible : Visibility.Hidden; }
        }

        public int NewPersonCount
        {
            get { return _newPersonCount; }
            set
            {
                if (value == _newPersonCount) return;
                _newPersonCount = value;
                OnPropertyChanged("NewPersonCount");
            }
        }

        public int NewResumeCount
        {
            get { return _newResumeCount; }
            set
            {
                if (value == _newResumeCount) return;
                _newResumeCount = value;
                OnPropertyChanged("NewResumeCount");
            }
        }

        public TimeSpan DownloadTime
        {
            get { return _downloadTime; }
            set
            {
                if (value.Equals(_downloadTime)) return;
                _downloadTime = value;
                OnPropertyChanged("DownloadTime");
            }
        }

        public TimeSpan MergeTime
        {
            get { return _mergeTime; }
            set
            {
                if (value.Equals(_mergeTime)) return;
                _mergeTime = value;
                OnPropertyChanged("MergeTime");
            }
        }

        public TimeSpan AllTime
        {
            get { return _allTime; }
            set
            {
                if (value.Equals(_allTime)) return;
                _allTime = value;
                OnPropertyChanged("AllTime");
            }
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