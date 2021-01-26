using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class ConformityCertificate : DataBaseEntity
    {
        private int _registrationNumber;
        public int RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                if (value > 0)
                {
                    _registrationNumber = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Регистрационный номер должен быть больше нуля");
                }
            }
        }

        private string _codeTransportUnion;
        public string CodeTransportUnion
        {
            get => _codeTransportUnion;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 10)
                {
                    _codeTransportUnion = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Код товарной номенклатуры должен быть заполнен!");
                }
            }
        }

        public DateTime RegistrationDate { get; set; }

        public DateTime EndDate { get; set; }

        public ConformityCertificate(int registrationNumber, string codeTransportUnion, DateTime registrationDate, DateTime endDate)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
            EndDate = endDate;
        }

        public ConformityCertificate(long? id, IDataAdapter dataAdapter, int registrationNumber, string codeTransportUnion, DateTime registrationDate, DateTime endDate) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
            EndDate = endDate;
        }

        public ConformityCertificate()
        {
        }

        public ConformityCertificate(int id)
        {
            List<ConformityCertificate> conformityCertificates = DataModel.ConformityCertificates.FindAll(s => s.Id == id);
            ConformityCertificate conformityCertificate = new ConformityCertificate();
            foreach (ConformityCertificate conf in conformityCertificates)
            {
                conformityCertificate = new ConformityCertificate(conf.Id, DataAdapter, conf.RegistrationNumber, conf.CodeTransportUnion,
                conf.RegistrationDate, conf.EndDate);
            }

            Id = conformityCertificate.Id;
            DataAdapter = conformityCertificate.DataAdapter;
            RegistrationNumber = conformityCertificate.RegistrationNumber;
            CodeTransportUnion = conformityCertificate.CodeTransportUnion;
            RegistrationDate = conformityCertificate.RegistrationDate;
            EndDate = conformityCertificate.EndDate;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Conformity_Certificate", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "CodeTransportUnion", CodeTransportUnion }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "EndDate", EndDate.ToString("yyyy-MM-dd") } });
            DataModel.ConformityCertificates.Add(new ConformityCertificate(id, DataModel.DataAdapter, RegistrationNumber, CodeTransportUnion, RegistrationDate, EndDate));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Conformity_Certificate", Id.Value);
            DataModel.ConformityCertificates.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Conformity_Certificate", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber },{ "CodeTransportUnion", CodeTransportUnion }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "EndDate", EndDate.ToString("yyyy-MM-dd")}});
        }
    }
}
