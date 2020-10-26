using System;
using System.Collections.Generic;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class Candidate
    {
        /// <summary>
        /// ИД источника данных
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Номер телефона (человеко-читаемый формат)
        /// </summary>
        public String PhoneNumber { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public String ExpectedPosition { get; set; }
        /// <summary>
        /// Адрес эл. почты курирующего сотрудника HR
        /// </summary>
        public String HRMail { get; set; }

        private static IDomainCaster domainCaster = new TSQLCaster();

        public static Candidate SelectCandidate(int id)
        {
            return domainCaster.GetCandidate(id);
        }

        public void Insert()
        {
            domainCaster.GetACandidate(this).dbInsert();
        }

        public void Delete()
        {            
            domainCaster.GetACandidate(this).dbDelete();
        }        

        public Candidate() { }
    }
}
