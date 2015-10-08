using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Xrm.Test1.RawData.E1.Common;
using Xrm.Test1.RawDataCommon.DataModel;

namespace Xrm.Test1.RawData.E1.BaseJsonConverter
{
    public class BaseConverter : IJsonConverter
    {
        public IList<IResumeRaw> Convert(Stream stream)
        {
            try
            {
                var tmp = new DataContractJsonSerializer(typeof(BaseJsonConverter2.RootObject));
                var tmpObj = tmp.ReadObject(stream)as BaseJsonConverter2.RootObject;

                if (tmpObj != null)
                {
                    var result = new List<IResumeRaw>();
                    foreach (var obj in tmpObj.resumes)
                    {
                        result.Add(ConvertResume(obj));
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                // тут надо логировать, делаю плохо
            }

            return new List<IResumeRaw>();
        }

        private IResumeRaw ConvertResume(BaseJsonConverter2.Resume data)
        {
            //&quot
            var result = new ResumeRaw();

            result.Fio = string.Format("{0} {1} {2}", data.contact.lastname, data.contact.firstname,
                data.contact.patronymic).Trim();

            result.Birthday = string.IsNullOrEmpty(data.birthday)
                ? DateTime.MinValue
                : DateTime.Parse(data.birthday).Date;

            result.Sex = new ItemRaw {Name = data.sex};
            result.IsSmoke = data.is_smoke;
            result.IsDriver = data.is_driver;
            result.MaritalStatus = new ItemRaw{Name = data.marital_status};
            result.Header = data.header;
            result.AddDate = DateTime.Parse(data.add_date);
            result.EditDate = DateTime.Parse(data.mod_date);
            result.Districts =
                data.districts.Select(t => new ItemRaw {Id = t.id, Name = t.title}).Cast<IItemRaw>().ToList();

            int sum;

            if (data.wanted_salary_rub != null)
            {
                sum = data.wanted_salary_rub.Value;
            }
            else
            {
                sum = 0;
            }


            result.WantedSalaryRub = sum;
            result.Education = data.education != null
                ? new ItemRaw {Id = data.education.id, Name = data.education.title}
                : new ItemRaw { Name = "Не указан" };
            result.EducationDescription = data.education_description;
            result.EducationSpecialty = data.education_specialty;
            result.ExperienceLength = data.experience_length != null
                ? new ItemRaw {Id = data.experience_length.id, Name = data.experience_length.title}
                : new ItemRaw { Name = "Не указан" };
            result.Experience = data.experience.Replace("&quot;", "\"").Replace("&quot", "\"");
            result.PersonalQualities = data.personal_qualities;
            result.PhotoLink = data.photo!=null? data.photo.url:string.Empty;
            result.Rubric = new ItemRaw {Id = data.rubrics.First().id, Name = data.rubrics.First().title};
            result.Specialities =
                data.rubrics.First()
                    .specialities.Select(t => new ItemRaw {Id = t.id, Name = t.title})
                    .Cast<IItemRaw>()
                    .ToList();
            result.ScheduleWork = data.schedule != null
                ? new ItemRaw {Id = data.schedule.id, Name = data.schedule.title}
                : new ItemRaw {Name="Не указан"};
            result.WorkingType = data.working_type != null
                ? new ItemRaw {Id = data.working_type.id, Name = data.working_type.title}
                : new ItemRaw { Name = "Не указан" };
            result.Link = "http://rabota.e1.ru/"+data.url;
            result.InSourcesId = data.id;

            result.City = new ItemRaw { Id = data.contact.city.id, Name = data.contact.city.title };

            return result;
        }
    }
}
