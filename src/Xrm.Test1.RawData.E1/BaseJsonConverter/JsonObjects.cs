using System.Collections.Generic;

namespace Xrm.Test1.RawData.E1.BaseJsonConverter
{
    public class Resultset
    {
        public int count { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    public class Metadata
    {
        public Resultset resultset { get; set; }
    }

    public class ExperienceLength
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // Валюта
    public class Currency
    {
        //Идентификатор
        public int id { get; set; }
        //Валюта
        public string title { get; set; }
    }

    public class Schedule
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // тип работы (полная занятость)
    public class WorkingType
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // телефон
    public class Phone
    {
        public string phone { get; set; }
        public object comment { get; set; }
    }

    // город
    public class City
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // район
    public class District
    {
        //ид
        public int id { get; set; }
        // идентификатор
        public string title { get; set; }
    }

    public class Contact
    {
        public int icq { get; set; }

        // Скайп
        public string skype { get; set; }

        // почта
        public string email { get; set; }
        public object url { get; set; }
        public object street { get; set; }
        public object building { get; set; }
        public object room { get; set; }
        public object subway { get; set; }
        public string name { get; set; }

        //Имя
        public string firstname { get; set; }

        //Фамилия
        public string lastname { get; set; }

        //Отчество
        public string patronymic { get; set; }

        //Телефоны
        public List<Phone> phones { get; set; }

        // Город
        public City city { get; set; }

        // Район
        public District district { get; set; }
        public string facebook { get; set; }
        public string moi_krug { get; set; }
        public string linkedin { get; set; }
        public string twitter { get; set; }
        public string vk { get; set; }
        public object address_description { get; set; }

        // место жительства
        public string address { get; set; }
    }

    // Образование
    public class Education
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // район
    public class District2
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    // специальности
    public class Speciality
    {
        // идентификатор
        public int id { get; set; }
        // название
        public string title { get; set; }
    }

    // рубрики
    public class Rubric
    {
        // идентификатор
        public int id { get; set; }
        // название
        public string title { get; set; }
        // специальности
        public List<Speciality> specialities { get; set; }
    }

    // фото
    public class Photo
    {
        //ур
        public string url { get; set; }

        // размер
        public string size { get; set; }
    }

    public class Resume
    {
        /// <summary>
        /// Идентификатор на е1
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Описание опыта работы строкой
        /// </summary>
        public string experience { get; set; }

        /// <summary>
        /// Заголовок (искомые должности)
        /// </summary>
        public string header { get; set; }

        /// <summary>
        /// Специальность образования
        /// </summary>
        public string education_specialty { get; set; }

        /// <summary>
        ///  Описание работы енам
        /// </summary>
        public ExperienceLength experience_length { get; set; }

        /// <summary>
        ///  Институт
        /// </summary>
        public string institution { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public Currency currency { get; set; }

        /// <summary>
        ///  семейное положение
        /// </summary>
        public string marital_status { get; set; }

        /// <summary>
        ///  расписание работы
        /// </summary>
        public Schedule schedule { get; set; }

        public int wanted_salary { get; set; }

        /// <summary>
        /// +пол
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        ///  Умения
        /// </summary>
        public string personal_qualities { get; set; }

        /// <summary>
        ///  Где когда учился (строкой
        /// </summary>
        public string education_description { get; set; }

        /// <summary>
        ///  тип искомой работы
        /// </summary>
        public WorkingType working_type { get; set; }

        /// <summary>
        ///  дата добавления
        /// </summary>
        public string add_date { get; set; }

        /// <summary>
        ///  Контакты -
        /// </summary>
        public Contact contact { get; set; }

        /// <summary>
        ///  Образование
        /// </summary>
        public Education education { get; set; }

        /// <summary>
        ///  возраст
        /// </summary>
        public int age { get; set; }

        /// <summary>
        ///  дата рождения
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        ///  прячет ли ДР
        /// </summary>
        public string hide_birthday { get; set; }

        /// <summary>
        ///  урл для просмотра
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int owner_id { get; set; }

        /// <summary>
        ///  курит?
        /// </summary>
        public bool is_smoke { get; set; }

        /// <summary>
        ///  права ?
        /// </summary>
        public bool is_driver { get; set; }

        /// <summary>
        ///  Районы где ищет работу
        /// </summary>
        public List<District2> districts { get; set; }

        public bool removal { get; set; }

        public List<object> jobs { get; set; }

        public List<object> languages { get; set; }

        public List<object> driver_licenses { get; set; }

        /// <summary>
        ///  список институтов
        /// </summary>
        public List<object> institutions { get; set; }

        public List<object> recommendations { get; set; }

        /// <summary>
        ///  вторичное образование
        /// </summary>
        public List<object> secondary_educations { get; set; }

        /// <summary>
        ///  что то про детей
        /// </summary>
        public string has_child { get; set; }

        public List<object> citizenship { get; set; }

        public string portfolio_link { get; set; }

        public string surname_hide { get; set; }

        // ??
        public bool favorite { get; set; }

        /// <summary>
        ///  спрятано ли резюме
        /// </summary>
        public bool hided { get; set; }

        public string wanted_salary_rub { get; set; }

        /// <summary>
        ///  Рубрики
        /// </summary>
        public List<Rubric> rubrics { get; set; }

        /// <summary>
        ///  краткая инфа
        /// </summary>
        public string info_short { get; set; }

        /// <summary>
        ///  Чуть более полная инфа
        /// </summary>
        public string info { get; set; }

        /// <summary>
        /// авторизованность на сайте
        /// </summary>
        public string access_status { get; set; }

        /// <summary>
        /// возможность отправлять сообщения
        /// </summary>
        public bool receives_messages { get; set; }

        /// <summary>
        ///  Дата время модификации
        /// </summary>
        public string mod_date { get; set; }

        public bool is_upped { get; set; }

        /// <summary>
        ///  количество просмотров
        /// </summary>
        public int views { get; set; }

        /// <summary>
        /// зарплата 
        /// </summary>
        public string salary { get; set; }

        /// <summary>
        /// *фото
        /// </summary>
        public Photo photo { get; set; }
    }

    public class RootObject
    {
        public Metadata metadata { get; set; }
        public List<Resume> resumes { get; set; }
    }
}
