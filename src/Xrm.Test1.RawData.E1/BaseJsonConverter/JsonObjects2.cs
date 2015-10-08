using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xrm.Test1.RawData.E1.BaseJsonConverter2
{
    public class Query
    {
        public List<int> state { get; set; }
        public int visibility_type { get; set; }
    }

    public class Resultset
    {
        public int count { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    public class Metadata
    {
        public Query query { get; set; }
        public Resultset resultset { get; set; }
    }

    public class Education
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class WorkingType
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Schedule
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Currency
    {
        public int id { get; set; }
        public string title { get; set; }
        public string alias { get; set; }
    }

    public class ExperienceLength
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Citizenship
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class CitiesReference
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<object> districts { get; set; }
        public List<object> subways { get; set; }
    }

    public class Speciality
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Rubric
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<Speciality> specialities { get; set; }
    }

    public class Subway
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class District
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string title { get; set; }
        public string locative { get; set; }
    }

    public class Contact
    {
        //Фамилия
        public string lastname { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public string patronymic { get; set; }
        public string street { get; set; }
        public string building { get; set; }
        public string room { get; set; }
        public Subway subway { get; set; }
        public District district { get; set; }
        public object address_description { get; set; }
        public object address { get; set; }
        public City city { get; set; }
        public object icq { get; set; }
        public string skype { get; set; }
        public string url { get; set; }
        public object facebook { get; set; }
        public object moi_krug { get; set; }
        public object linkedin { get; set; }
        public object twitter { get; set; }
        public object vk { get; set; }
        public bool use_messages { get; set; }
    }

    public class Photo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string size { get; set; }
    }

    public class WorkTimeTotal
    {
        public int year { get; set; }
        public int month { get; set; }
    }

    public class CreatedAt
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class LastLog
    {
        public int reason_id { get; set; }
        public object reason { get; set; }
        public int user_id { get; set; }
        public CreatedAt created_at { get; set; }
    }

    public class Resume
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public int? wanted_salary { get; set; }
        public int? wanted_salary_rub { get; set; }
        public int age { get; set; }
        public string header { get; set; }
        public string personal_qualities { get; set; }
        public string institution { get; set; }
        public string education_specialty { get; set; }
        public string education_description { get; set; }
        public string experience { get; set; }
        public string url { get; set; }
        public string skills { get; set; }
        public string birthday { get; set; }
        public string add_date { get; set; }
        public string mod_date { get; set; }
        public bool removal { get; set; }
        public bool is_driver { get; set; }
        public bool is_journey { get; set; }
        public bool is_smoke { get; set; }
        public bool has_child { get; set; }
        public string sex { get; set; }
        public string marital_status { get; set; }
        public bool surname_hide { get; set; }
        public bool can_accept_replies { get; set; }
        public bool hide_birthday { get; set; }
        public string visibility_type { get; set; }
        public Education education { get; set; }
        public WorkingType working_type { get; set; }
        public Schedule schedule { get; set; }
        public Currency currency { get; set; }
        public ExperienceLength experience_length { get; set; }
        public Citizenship citizenship { get; set; }
        public List<CitiesReference> cities_references { get; set; }
        public List<object> driver_licenses { get; set; }
        public List<object> languages { get; set; }
        public List<object> secondary_educations { get; set; }
        public List<object> institutions { get; set; }
        public List<object> jobs { get; set; }
        public List<Rubric> rubrics { get; set; }
        public List<object> recommendations { get; set; }
        public int state { get; set; }
        public int validate_state { get; set; }
        public int entity { get; set; }
        public Contact contact { get; set; }
        public Photo photo { get; set; }
        public string salary { get; set; }
        public string info_short { get; set; }
        public string info { get; set; }
        public int views { get; set; }
        public object access_status { get; set; }
        public object access_due_date { get; set; }
        public object apiece_count { get; set; }
        public bool is_upped { get; set; }
        public bool is_limit_exceeded { get; set; }
        public bool is_deleted { get; set; }
        public bool is_archived { get; set; }
        public bool legacy { get; set; }
        public int priority { get; set; }
        public object attachment { get; set; }
        public WorkTimeTotal work_time_total { get; set; }
        public List<object> subways { get; set; }
        public List<District> districts { get; set; }
        public List<object> tags { get; set; }
        public string imported_via { get; set; }
        public LastLog last_log { get; set; }
        public bool favorite { get; set; }
        public bool hided { get; set; }
    }

    public class RootObject
    {
        public Metadata metadata { get; set; }
        public List<Resume> resumes { get; set; }
    }
}
