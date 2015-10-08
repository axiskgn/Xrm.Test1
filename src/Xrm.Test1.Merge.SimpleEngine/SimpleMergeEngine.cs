using System;
using System.Collections.Generic;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Repositories.Common;
using Xrm.Test1.DataModel.ResumeSource;
using Xrm.Test1.MergeCommon;
using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.Merge.SimpleEngine
{
    public class SimpleMergeEngine:IMergeEngine
    {
        private readonly IRootRepository _rootRepository;

        public SimpleMergeEngine(IRootRepository rootRepository)
        {
            if (rootRepository == null) throw new ArgumentNullException("rootRepository");
            _rootRepository = rootRepository;
            NewPersonCount = 0;
            NewResumeCount = 0;
        }

        public void AddResume(IResumeRaw resumeRaw, string sourceName)
        {
            var ra = _rootRepository.GetReadAcces();

            var resumeSourceList = ra.DictionaryRepository.GetAll<IResumeSource>();

            IResume resume = null;
            IPerson person = null;

            IResumeSourceInfo sourceInfo = null;
            var sourceType = resumeSourceList.FirstOrDefault(t => t.Name == sourceName);
            
            if (sourceType!=null)
            {
                var sourceInfoList = ra.ResumeSourceInfo.GetAll();
                sourceInfo =
                    sourceInfoList.FirstOrDefault(
                        t => t.Source.Id == sourceType.Id && t.ResumeSourceId == resumeRaw.InSourcesId);

                if (sourceInfo != null)
                {
                    var resumeList = ra.ResumeRepository.GetAll();

                    var resumeOkList = resumeList.Where(t => t.ResumeSourceInfo.Id == sourceInfo.Id).ToList();
                    if (resumeOkList.Any())
                    {
                        resume = resumeOkList.FirstOrDefault(t => t.EditDate == resumeRaw.EditDate);
                        if (resume != null)
                        {
                            person = resume.Person;
                        }
                        else
                        {
                            person = resumeOkList.First().Person;
                        }
                    }
                }
            }
            else
            {
                using (var wa = _rootRepository.GetWriteAcces())
                {
                    sourceType = wa.DictionaryRepository.Insert<IResumeSource>(sourceName);
                }
            }

            if (sourceInfo == null)
            {
                using (var wa = _rootRepository.GetWriteAcces())
                {
                    sourceInfo = wa.ResumeSourceInfo.Insert(resumeRaw.InSourcesId, sourceType, resumeRaw.Link);
                }               
            }

            if (person==null)
            {
                var personList = ra.PersonRepository.GetAll();
                person = personList.FirstOrDefault(t => t.Fio == resumeRaw.Fio && t.Birthday == resumeRaw.Birthday);
                if (person == null)
                {
                    using (var wa = _rootRepository.GetWriteAcces())
                    {
                        var sex =
                            ra.DictionaryRepository.GetAll<ISex>().FirstOrDefault(t => t.Name == resumeRaw.Sex.Name) ??
                            wa.DictionaryRepository.Insert<ISex>(resumeRaw.Sex.Name);

                        var maritialStatus =
                            ra.DictionaryRepository.GetAll<IMaritalStatus>()
                                .FirstOrDefault(t => t.Name == resumeRaw.MaritalStatus.Name) ??
                            wa.DictionaryRepository.Insert<IMaritalStatus>(resumeRaw.MaritalStatus.Name);

                        person = wa.PersonRepository.Insert(resumeRaw.Fio, resumeRaw.Birthday, sex,
                            new List<string> {resumeRaw.PhotoLink}, resumeRaw.IsSmoke, resumeRaw.IsDriver,
                            maritialStatus);

                        NewPersonCount++;
                    }
                }
            }

            if (resume == null)
            {
                using (var wa = _rootRepository.GetWriteAcces())
                {
                    var districtsList = ra.DictionaryRepository.GetAll<IDistrict>();
                    var districts = new List<IDistrict>();
                    foreach (var district in resumeRaw.Districts)
                    {
                        var needDistrict = districtsList.FirstOrDefault(t => t.Name == district.Name) ??
                                           wa.DictionaryRepository.Insert<IDistrict>(district.Name);
                        districts.Add(needDistrict);
                    }

                    var education =
                        ra.DictionaryRepository.GetAll<IEducation>()
                            .FirstOrDefault(t => t.Name == resumeRaw.Education.Name) ??
                        wa.DictionaryRepository.Insert<IEducation>(resumeRaw.Education.Name);

                    var expirienceLength = ra.DictionaryRepository.GetAll<IExperienceLength>()
                        .FirstOrDefault(t => t.Name == resumeRaw.ExperienceLength.Name) ??
                                           wa.DictionaryRepository.Insert<IExperienceLength>(
                                               resumeRaw.ExperienceLength.Name);

                    var rubric = ra.DictionaryRepository.GetAll<IRubric>()
                        .FirstOrDefault(t => t.Name == resumeRaw.Rubric.Name) ??
                                 wa.DictionaryRepository.Insert<IRubric>(
                                     resumeRaw.Rubric.Name);

                    var resumeSpecList = new List<IRubricSpeciality>();
                    var specList = ra.DictionaryRepository.GetAll<IRubricSpeciality>();
                    foreach (var speciality in resumeRaw.Specialities)
                    {
                        var needDistrict = specList.FirstOrDefault(t => t.Name == speciality.Name) ??
                                           wa.DictionaryRepository.Insert<IRubricSpeciality>(speciality.Name);
                        specList.Add(needDistrict);
                    }

                    var scheduleWork = ra.DictionaryRepository.GetAll<IScheduleWork>()
                        .FirstOrDefault(t => t.Name == resumeRaw.ScheduleWork.Name) ??
                                 wa.DictionaryRepository.Insert<IScheduleWork>(
                                     resumeRaw.ScheduleWork.Name);

                    var workType = ra.DictionaryRepository.GetAll<IWorkingType>()
                        .FirstOrDefault(t => t.Name == resumeRaw.WorkingType.Name) ??
                                   wa.DictionaryRepository.Insert<IWorkingType>(
                                       resumeRaw.WorkingType.Name);

                    ICity city = null;

                    if (!string.IsNullOrEmpty(resumeRaw.City.Name))
                    {
                        city = ra.DictionaryRepository.GetAll<ICity>()
                            .FirstOrDefault(t => t.Name == resumeRaw.City.Name) ??
                               wa.DictionaryRepository.Insert<ICity>(
                                   resumeRaw.City.Name);
                    }

                    resume = wa.ResumeRepository.Insert(person, resumeRaw.Header, resumeRaw.AddDate, resumeRaw.EditDate,
                        districts, resumeRaw.WantedSalaryRub, education, resumeRaw.EducationDescription,
                        resumeRaw.EducationSpecialty, expirienceLength, resumeRaw.Experience, sourceInfo,
                        resumeRaw.PersonalQualities, resumeRaw.PhotoLink, rubric, resumeSpecList, scheduleWork, workType, city);

                    NewResumeCount++;
                }
            }

            if (!string.IsNullOrEmpty(resume.PhotoLink) && !person.Photos.Contains(resume.PhotoLink))
            {
                using (var wa = _rootRepository.GetWriteAcces())
                {
                    wa.PersonRepository.AddPhoto(person, resume.PhotoLink);
                }
            }
        }

        public int NewPersonCount { get; private set; }
        public int NewResumeCount { get; private set; }
    }
}
