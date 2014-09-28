using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.Repositories
{
    public interface IResumeWriteRepository
    {
        IResume Insert(IPerson person, string header, DateTime addDate, DateTime editDate,
            IList<IDistrict> districts, int wantedSalaryRub, IEducation education, string educationDescription,
            string educationSpecialty, IExperienceLength experienceLength, string experience,
            IResumeSourceInfo resumeSourceInfo, string personalQualities, string photoLink, IRubric rubric,
            IList<IRubricSpeciality> specialities, IScheduleWork scheduleWork, IWorkingType workingType, ICity city);
    }
}