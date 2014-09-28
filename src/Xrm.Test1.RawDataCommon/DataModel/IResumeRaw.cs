using System;
using System.Collections.Generic;

namespace Xrm.Test1.RawDataCommon.DataModel
{
    public interface IResumeRaw
    {
        string Fio { get; }
        DateTime Birthday { get; }
        IItemRaw Sex { get; }
        bool IsSmoke { get; } 
        bool IsDriver { get; }
        IItemRaw MaritalStatus { get; }

        string Header { get; }
        DateTime AddDate { get; }
        DateTime EditDate { get; }

        IList<IItemRaw> Districts { get; }
        int WantedSalaryRub { get; }

        IItemRaw Education { get; }

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
        IItemRaw ExperienceLength { get; }

        /// <summary>
        /// Описание опыта работы
        /// </summary>
        string Experience { get; }

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
        IItemRaw Rubric { get; }

        /// <summary>
        /// Специальности рубрики
        /// </summary>
        IList<IItemRaw> Specialities { get; }

        /// <summary>
        /// График работы
        /// </summary>
        IItemRaw ScheduleWork { get; }

        /// <summary>
        /// Тип работы (полная занятость/совмещение)
        /// </summary>
        IItemRaw WorkingType { get; }

        string Link { get; }

        int InSourcesId { get; }

        IItemRaw City { get; }

    }
}