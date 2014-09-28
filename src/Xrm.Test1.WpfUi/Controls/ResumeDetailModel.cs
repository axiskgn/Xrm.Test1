using System;
using System.ComponentModel;
using System.Windows.Input;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.WpfUi.Annotations;
using Xrm.Test1.WpfUi.Common;

namespace Xrm.Test1.WpfUi.Controls
{
    public class ResumeDetailModel:INotifyPropertyChanged
    {
        private IResume _resume;

        public ResumeDetailModel()
        {
            CloseCmd = new RelayCommand(o => OnBack());
        }

        public void SetResume(IResume resume)
        {
            _resume = resume;
            OnPropertyChanged("Fio");
            OnPropertyChanged("SexAndAge");
            OnPropertyChanged("Link");

            OnPropertyChanged("ResumeInfo");
            OnPropertyChanged("Head");
            OnPropertyChanged("Salary");

            OnPropertyChanged("WorkType");
            OnPropertyChanged("Experience");

            OnPropertyChanged("EducationType");
            OnPropertyChanged("EducationOrg");
            OnPropertyChanged("EducationSpec");

            OnPropertyChanged("Disctricts");
            OnPropertyChanged("City");

            OnPropertyChanged("PersonalQualities");

            OnPropertyChanged("MaritalStatus");
            OnPropertyChanged("Smoke");
            OnPropertyChanged("Driver");

            OnPropertyChanged("Rubric");
            OnPropertyChanged("RubricSpecs");
        }

        public ICommand CloseCmd { get; private set; }

        public string Fio
        {
            get
            {
                return _resume.Person.Fio;
            }
        }

        public string SexAndAge
        {
            get
            {
                //Увы костыль  :(
                var sex = _resume.Person.Sex.Name;
                if (sex == "female") sex = "Ж";
                if (sex == "male") sex = "М";

                if (_resume.Person.Birthday.Year < 1920 || _resume.Person.Birthday.Year>2010)
                {
                    return string.Format("{0} возраст не указан)",
                        sex);
                }

                return string.Format("{0} Возраст - {1} ({2})",
                    sex,
                    ((DateTime.Now - _resume.Person.Birthday).Days/365),
                    _resume.Person.Birthday.ToShortDateString());
            }
        }

        public string Link
        {
            get
            {
                return "http://rabota.e1.ru/"+_resume.ResumeSourceInfo.Link;
            }
        }

        public string ResumeInfo
        {
            get
            {
                return string.Format("Источник {0}, № резюме {1}, добавлено {2}, обновлено {3}",
                    _resume.ResumeSourceInfo.Source.Name, 
                    _resume.ResumeSourceInfo.ResumeSourceId, 
                    _resume.AddDate,
                    _resume.EditDate);
            }
        }

        public string Head
        {
            get { return _resume.Header; }
        }

        public string Salary
        {
            get { return string.Format("{0} руб.", _resume.WantedSalaryRub); }
        }

        public string WorkType
        {
            get { return string.Format("{0}/{1}", _resume.WorkingType.Name, _resume.ScheduleWork.Name); }
        }

        public string ExperienceLong
        {
            get { return string.Format("Опыт работы {0}", _resume.ExperienceLength.Name); }
        }

        public string Experience
        {
            get { return _resume.Experience; }
        }

        public string EducationType
        {
            get { return string.Format("Образование: {0}", _resume.Education.Name); }
        }

        public string EducationOrg
        {
            get { return _resume.EducationDescription; }
        }

        public string EducationSpec
        {
            get { return _resume.EducationSpecialty; }
        }

        public string Disctricts
        {
            get
            {
                var list = "";
                foreach (var district in _resume.Districts)
                {
                    if (!string.IsNullOrEmpty(list))
                    {
                        list += ", ";
                    }
                    list += district.Name;
                }
                return string.Format("Районы: {0}", list);
            }
        }

        public string City
        {
            get { return string.Format("Город: {0}", _resume.City.Name); }
        }

        public string PersonalQualities
        {
            get { return _resume.PersonalQualities; }
        }

        public string MaritalStatus
        {
            get
            {
                // тут нужен костыль
                return _resume.Person.MaritalStatus.Name;
            }
        }

        public string Smoke
        {
            get { return _resume.Person.IsSmoke?"Курю":"Не курю"; }
        }

        public string Driver
        {
            get { return _resume.Person.IsDriver?"Права есть":"Прав нет"; }
        }

        public string Rubric
        {
            get { return _resume.Rubric.Name; }
        }

        public string RubricSpecs
        {
            get
            {
                var result = "";

                foreach (var rubricSpeciality in _resume.Specialities)
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += ", ";
                    }

                    result += rubricSpeciality.Name;
                }

                return result;
            }
        }

        #region 

        public event Action Back;

        protected virtual void OnBack()
        {
            Action handler = Back;
            if (handler != null) handler();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}