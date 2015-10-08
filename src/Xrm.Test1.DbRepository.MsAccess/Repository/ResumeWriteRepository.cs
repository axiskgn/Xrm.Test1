using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.Factory;
using Xrm.Test1.DataModel.Repositories;
using Xrm.Test1.DataModel.ResumeSource;
using Xrm.Test1.DbRepository.MsAccess.Common;

namespace Xrm.Test1.DbRepository.MsAccess.Repository
{
    class ResumeWriteRepository:IResumeWriteRepository, IMsAccesRepository
    {
        private readonly OleDbConnection _connection;
        private readonly IResumeFactory _resumeFactory;

        public ResumeWriteRepository(OleDbConnection connection, IResumeFactory resumeFactory)
        {
            _connection = connection;
            _resumeFactory = resumeFactory;
        }

        public IResume Insert(IPerson person, string header, DateTime addDate, DateTime editDate, IList<IDistrict> districts, int wantedSalaryRub,
            IEducation education, string educationDescription, string educationSpecialty, IExperienceLength experienceLength,
            string experience, IResumeSourceInfo resumeSourceInfo, string personalQualities, string photoLink, IRubric rubric,
            IList<IRubricSpeciality> specialities, IScheduleWork scheduleWork, IWorkingType workingType, ICity city)
        {
            int newId;
            using (var cmd = new OleDbCommand(
                @"INSERT INTO Resume (personId, header, addDate, editDate, wantedSalaryRub, education, educationDescription, educationSpecialty, 
experienceLength, resumeSourceInfo, experience, personalQualities, photoLink, rubric, scheduleWork, workingType, city)
        VALUES (@personId, @header, @addDate, @editDate, @wantedSalaryRub, @education, @educationDescription, @educationSpecialty, 
@experienceLength, @resumeSourceInfo, @experience, @personalQualities, @photoLink, @rubric, @scheduleWork, @workingType, @city)"
                , _connection))
            {

                cmd.Parameters.Add("@personId", OleDbType.Integer).Value = person.Id;
                cmd.Parameters.Add("@header", OleDbType.VarChar).Value = header;
                cmd.Parameters.Add("@addDate", OleDbType.Date).Value = addDate;
                cmd.Parameters.Add("@editDate", OleDbType.Date).Value = editDate;
                cmd.Parameters.Add("@wantedSalaryRub", OleDbType.Integer).Value = wantedSalaryRub;
                cmd.Parameters.Add("@education", OleDbType.Integer).Value = education.Id;
                cmd.Parameters.Add("@educationDescription", OleDbType.VarChar).Value = educationDescription;
                cmd.Parameters.Add("@educationSpecialty", OleDbType.VarChar).Value = educationSpecialty;
                cmd.Parameters.Add("@experienceLength", OleDbType.Integer).Value = experienceLength.Id;
                cmd.Parameters.Add("@resumeSourceInfo", OleDbType.Integer).Value = resumeSourceInfo.Id;
                cmd.Parameters.Add("@experience", OleDbType.VarChar).Value = experience;
                cmd.Parameters.Add("@personalQualities", OleDbType.VarChar).Value = personalQualities;
                cmd.Parameters.Add("@photoLink", OleDbType.VarChar).Value = photoLink??string.Empty;
                cmd.Parameters.Add("@rubric", OleDbType.Integer).Value = rubric.Id;
                cmd.Parameters.Add("@scheduleWork", OleDbType.Integer).Value = scheduleWork.Id;
                cmd.Parameters.Add("@workingType", OleDbType.Integer).Value = workingType.Id;
                if (city != null)
                {
                    cmd.Parameters.Add("@city", OleDbType.Integer).Value = city.Id;
                }
                else
                {
                    cmd.Parameters.Add("@city", OleDbType.Integer).Value = 0;
                }
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT @@IDENTITY";
                newId = (int) cmd.ExecuteScalar();
            }
            foreach (var val in districts)
            {
                using ( var cmdP = new OleDbCommand(
                            "INSERT INTO ResumeDistrict (idResume, district) VALUES (@idResume, @district)", _connection))
                {
                    cmdP.Parameters.Add("@idResume", OleDbType.Integer).Value = newId;
                    cmdP.Parameters.Add("@district", OleDbType.Integer).Value = val.Id;
                    cmdP.ExecuteNonQuery();
                }
            }

            foreach (var val in specialities)
            {
                using ( var cmdP = new OleDbCommand(
                            "INSERT INTO Specialities (idResume, specialities) VALUES (@idResume, @specialities)", _connection))
                {
                    cmdP.Parameters.Add("@idResume", OleDbType.Integer).Value = newId;
                    cmdP.Parameters.Add("@specialities", OleDbType.Integer).Value = val.Id;
                    cmdP.ExecuteNonQuery();
                }
            }

            return _resumeFactory.CreateEntity(newId, person, header, addDate, editDate, districts,
                wantedSalaryRub, education, educationDescription, educationSpecialty,
                experienceLength, experience, resumeSourceInfo, personalQualities,
                photoLink, rubric, specialities, scheduleWork, workingType, city);
        }

        public void Dispose()
        {
            
        }

        public bool Check(IList<string> tableList)
        {
            if (!tableList.Contains("Resume"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE Resume(id COUNTER, personId int, header string, addDate datetime, editDate datetime, wantedSalaryRub int,
education int, educationDescription LONGTEXT, educationSpecialty LONGTEXT, experienceLength int, resumeSourceInfo int,
experience LONGTEXT, personalQualities LONGTEXT, photoLink string, rubric int, scheduleWork int, workingType int, city int)",
                    _connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (!tableList.Contains("ResumeDistrict"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE ResumeDistrict(id COUNTER, idResume int, district int)", _connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            if (!tableList.Contains("Specialities"))
            {
                using (var command = new OleDbCommand(@"
CREATE TABLE Specialities(id COUNTER, idResume int, specialities int)", _connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
