using System;
using System.Collections.Generic;
using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.RawData.E1.BaseJsonConverter
{
    internal class ResumeRaw : IResumeRaw
    {
        public string Fio { get; set; }
        public DateTime Birthday { get; set; }
        public IItemRaw Sex { get; set; }
        public bool IsSmoke { get; set; }
        public bool IsDriver { get; set; }
        public IItemRaw MaritalStatus { get; set; }
        public string Header { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime EditDate { get; set; }
        public IList<IItemRaw> Districts { get; set; }
        public int WantedSalaryRub { get; set; }
        public IItemRaw Education { get; set; }
        public string EducationDescription { get; set; }
        public string EducationSpecialty { get; set; }
        public IItemRaw ExperienceLength { get; set; }
        public string Experience { get; set; }
        public string PersonalQualities { get; set; }
        public string PhotoLink { get; set; }
        public IItemRaw Rubric { get; set; }
        public IList<IItemRaw> Specialities { get; set; }
        public IItemRaw ScheduleWork { get; set; }
        public IItemRaw WorkingType { get; set; }
        public string Link { get; set; }
        public int InSourcesId { get; set; }
        public IItemRaw City { get; set; }
    }
}
