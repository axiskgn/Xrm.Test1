using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.ConsoleTest.DataModel.ObjectFactory
{
    public class ResumeFactory : IResumeFactory
    {
        private class Resume : IResume
        {
            public Resume(int id, IPerson person, string header, DateTime addDate, DateTime editDate, IList<IDistrict> districts, int wantedSalaryRub, IEducation education, string educationDescription, string educationSpecialty, IExperienceLength experienceLength, string experience, IResumeSourceInfo resumeSourceInfo, string personalQualities, string photoLink, IRubric rubric, IList<IRubricSpeciality> specialities, IScheduleWork scheduleWork, IWorkingType workingType, ICity city)
            {
                Id = id;
                Person = person;
                Header = header;
                AddDate = addDate;
                EditDate = editDate;
                Districts = districts;
                WantedSalaryRub = wantedSalaryRub;
                Education = education;
                EducationDescription = educationDescription;
                EducationSpecialty = educationSpecialty;
                ExperienceLength = experienceLength;
                Experience = experience;
                ResumeSourceInfo = resumeSourceInfo;
                PersonalQualities = personalQualities;
                PhotoLink = photoLink;
                Rubric = rubric;
                Specialities = specialities;
                ScheduleWork = scheduleWork;
                WorkingType = workingType;
                City = city;
            }

            public int Id { get; private set; }
            public IPerson Person { get; private set; }
            public string Header { get; private set; }
            public DateTime AddDate { get; private set; }
            public DateTime EditDate { get; private set; }
            public IList<IDistrict> Districts { get; private set; }
            public int WantedSalaryRub { get; private set; }
            public IEducation Education { get; private set; }
            public string EducationDescription { get; private set; }
            public string EducationSpecialty { get; private set; }
            public IExperienceLength ExperienceLength { get; private set; }
            public string Experience { get; private set; }
            public IResumeSourceInfo ResumeSourceInfo { get; private set; }
            public string PersonalQualities { get; private set; }
            public string PhotoLink { get; private set; }
            public IRubric Rubric { get; private set; }
            public IList<IRubricSpeciality> Specialities { get; private set; }
            public IScheduleWork ScheduleWork { get; private set; }
            public IWorkingType WorkingType { get; private set; }
            public ICity City { get; private set; }
        }

        public IResume CreateEntity(int id, IPerson person, string header, DateTime addDate, DateTime editDate, IList<IDistrict> districts,
            int wantedSalaryRub, IEducation education, string educationDescription, string educationSpecialty,
            IExperienceLength experienceLength, string experience, IResumeSourceInfo resumeSourceInfo, string personalQualities,
            string photoLink, IRubric rubric, IList<IRubricSpeciality> specialities, IScheduleWork scheduleWork, IWorkingType workingType, ICity city)
        {
            return new Resume(id, person, header, addDate, editDate, districts,
                wantedSalaryRub, education, educationDescription, educationSpecialty,
                experienceLength, experience, resumeSourceInfo, personalQualities,
                photoLink, rubric, specialities, scheduleWork, workingType, city);
        }
    }
}