using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    public class ResumeReadRepository : IResumeReadRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IDictionaryReadRepository _dictionaryRepository;
        private readonly IPersonReadRepository _personRepository;
        private readonly IResumeSourceInfoReadRepository _resumeSourceRepository;
        private readonly IResumeFactory _resumeFactory;

        public ResumeReadRepository(OleDbConnection connection, IDictionaryReadRepository dictionaryRepository, IPersonReadRepository personRepository, IResumeSourceInfoReadRepository resumeSourceRepository, IResumeFactory resumeFactory)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dictionaryRepository == null) throw new ArgumentNullException("dictionaryRepository");
            if (personRepository == null) throw new ArgumentNullException("personRepository");
            if (resumeSourceRepository == null) throw new ArgumentNullException("resumeSourceRepository");
            if (resumeFactory == null) throw new ArgumentNullException("resumeFactory");
            _connection = connection;
            _dictionaryRepository = dictionaryRepository;
            _personRepository = personRepository;
            _resumeSourceRepository = resumeSourceRepository;
            _resumeFactory = resumeFactory;
        }

        public IList<IResume> GetAll()
        {
            var persons = _personRepository.GetAll();

            #region ResumeDistrict

            var districtList = _dictionaryRepository.GetAll<IDistrict>();

            var districtResumeList = new Dictionary<int, List<IDistrict>>();
            //CREATE TABLE ResumeDistrict(id COUNTER, idResume int, district int)
            using (var cmd0 = new OleDbCommand("SELECT idResume, district FROM ResumeDistrict", _connection))
            {
                using (var dr0 = cmd0.ExecuteReader())
                {
                    while (dr0 != null && dr0.Read())
                    {
                        var idResume = (int) dr0["idResume"];
                        var district = (int) dr0["district"];
                        if (!districtResumeList.ContainsKey(idResume))
                        {
                            districtResumeList.Add(idResume, new List<IDistrict>());
                        }
                        districtResumeList[idResume].Add(districtList.First(t => t.Id == district));
                    }
                }
            }

            #endregion
            
            #region Specialities

            var specialitiesList = _dictionaryRepository.GetAll<IRubricSpeciality>();

            var specialitiesResumeList = new Dictionary<int, List<IRubricSpeciality>>();
            //CREATE TABLE Specialities(id COUNTER, idResume int, specialities int)
            using (var cmd1 = new OleDbCommand("SELECT idResume, specialities FROM Specialities", _connection))
            {
                using (var dr1 = cmd1.ExecuteReader())
                {
                    while (dr1 != null && dr1.Read())
                    {
                        var idResume = (int) dr1["idResume"];
                        var specialities = (int) dr1["specialities"];
                        if (!specialitiesResumeList.ContainsKey(idResume))
                        {
                            specialitiesResumeList.Add(idResume, new List<IRubricSpeciality>());
                        }
                        specialitiesResumeList[idResume].Add(specialitiesList.First(t => t.Id == specialities));
                    }
                }
            }

            #endregion

            var resumeSources = _resumeSourceRepository.GetAll();

            var educationList = _dictionaryRepository.GetAll<IEducation>();
            var experienceLengthList = _dictionaryRepository.GetAll<IExperienceLength>();
            var rubricList = _dictionaryRepository.GetAll<IRubric>();
            var scheduleWorkList = _dictionaryRepository.GetAll<IScheduleWork>();
            var workingTypeList = _dictionaryRepository.GetAll<IWorkingType>();
            var cityList = _dictionaryRepository.GetAll<ICity>();

            var result = new List<IResume>();
            // CREATE TABLE Resume(id COUNTER, header string, addDate datetime, editDate datetime, wantedSalaryRub int,
//education int, educationDescription string, educationSpecialty string, experienceLength int, resumeSourceInfo int,
//experience string, personalQualities string, photoLink string, rubric int, scheduleWork int, workingType int)",
            using (var cmd = new OleDbCommand("SELECT * FROM Resume", _connection))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        var id = (int) dr["id"];
                        var personId = (int) dr["personId"];
                        var header = (string) dr["header"];
                        var addDate = (DateTime) dr["addDate"];
                        var editDate = (DateTime) dr["editDate"];
                        var wantedSalaryRub = (int) dr["wantedSalaryRub"];
                        var education = (int) dr["education"];
                        var educationDescription = (string) dr["educationDescription"];
                        var educationSpecialty = (string) dr["educationSpecialty"];
                        var experienceLength = (int) dr["experienceLength"];
                        var resumeSourceInfo = (int) dr["resumeSourceInfo"];
                        var experience = (string) dr["experience"];
                        var personalQualities = (string) dr["personalQualities"];
                        var photoLink = (string) dr["photoLink"];
                        var rubric = (int) dr["rubric"];
                        var scheduleWork = (int) dr["scheduleWork"];
                        var workingType = (int) dr["workingType"];
                        var city = (int) dr["city"];

                        var obj = _resumeFactory.CreateEntity(id,
                            persons.First(t => t.Id == personId),
                            header,
                            addDate,
                            editDate,
                            districtResumeList.ContainsKey(id) ? districtResumeList[id] : new List<IDistrict>(),
                            wantedSalaryRub,
                            educationList.First(t => t.Id == education),
                            educationDescription,
                            educationSpecialty,
                            experienceLengthList.First(t => t.Id == experienceLength),
                            experience,
                            resumeSources.First(t => t.Id == resumeSourceInfo),
                            personalQualities,
                            photoLink,
                            rubricList.First(t => t.Id == rubric),
                            specialitiesResumeList.ContainsKey(id)
                                ? specialitiesResumeList[id]
                                : new List<IRubricSpeciality>(),
                            scheduleWorkList.FirstOrDefault(t => t.Id == scheduleWork),
                            workingTypeList.FirstOrDefault(t => t.Id == workingType),
                            cityList.FirstOrDefault(t => t.Id == city));

                        result.Add(obj);
                    }
                }
            }
            return result;

        }

        public void Dispose()
        {
            
        }

        public bool Check(IList<string> tableList)
        {
            return true;
        }
    }
}