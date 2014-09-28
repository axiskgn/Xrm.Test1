using System;
using System.Collections.Generic;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.ResumeSource;

namespace Xrm.Test1.DataModel.Entity
{
    public interface IResume
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Информация о человеке
        /// </summary>
        IPerson Person { get; }

        /// <summary>
        /// Должность
        /// </summary>
        string Header { get; } 

        /// <summary>
        /// Дата+время добавления резюме на сервер
        /// </summary>
        DateTime AddDate { get; }

        /// <summary>
        /// Дата+время последнего изменения резюме на сервере
        /// </summary>
        DateTime EditDate { get; }

        /// <summary>
        /// Районы поиска
        /// </summary>
        IList<IDistrict> Districts { get; }

        /// <summary>
        /// Искомая зарплата в рублях
        /// </summary>
        int WantedSalaryRub { get; }

        /// <summary>
        /// Образование
        /// </summary>
        IEducation Education { get; }

        /// <summary>
        /// Учебное заведение
        /// </summary>
        string EducationDescription { get; }

        /// <summary>
        /// Специальность образования
        /// </summary>
        string EducationSpecialty { get; }

        /// <summary>
        /// Размер опыта
        /// </summary>
        IExperienceLength ExperienceLength { get; }

        /// <summary>
        /// Описание опыта работы
        /// </summary>
        string Experience { get; }

        /// <summary>
        /// Информация о источнике резюме
        /// </summary>
        IResumeSourceInfo ResumeSourceInfo { get; }

        /// <summary>
        /// Личные качества
        /// </summary>
        string PersonalQualities { get; }

        /// <summary>
        /// Ссылка на фото
        /// </summary>
        string PhotoLink { get; }

        /// <summary>
        /// Рубрика в которой размещается резюме
        /// </summary>
        IRubric Rubric { get; }

        /// <summary>
        /// Специальности рубрики
        /// </summary>
        IList<IRubricSpeciality> Specialities { get; }

        /// <summary>
        /// График работы
        /// </summary>
        IScheduleWork ScheduleWork { get; }

        /// <summary>
        /// Тип работы (полная занятость/совмещение)
        /// </summary>
        IWorkingType WorkingType { get; }

        /// <summary>
        /// Город
        /// </summary>
        ICity City { get; }

    }
}