using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class StateRegistrationCertificate : DataBaseEntity
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

        public StateRegistrationCertificate(int registrationNumber, string codeTransportUnion, DateTime registrationDate)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
        }

        public StateRegistrationCertificate(long? id, IDataAdapter dataAdapter, int registrationNumber, string codeTransportUnion, DateTime registrationDate) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
        }

        public StateRegistrationCertificate()
        {
        }

        public StateRegistrationCertificate(int id)
        {
            List<StateRegistrationCertificate> stateRegistrationCertificates = DataModel.StateRegistrationCertificates.FindAll(s => s.Id == id);
            StateRegistrationCertificate stateRegistrationCertificate = new StateRegistrationCertificate();
            foreach (StateRegistrationCertificate conf in stateRegistrationCertificates)
            {
                stateRegistrationCertificate = new StateRegistrationCertificate(conf.Id, DataAdapter, conf.RegistrationNumber, conf.CodeTransportUnion,
                conf.RegistrationDate);
            }

            Id = stateRegistrationCertificate.Id;
            DataAdapter = stateRegistrationCertificate.DataAdapter;
            RegistrationNumber = stateRegistrationCertificate.RegistrationNumber;
            CodeTransportUnion = stateRegistrationCertificate.CodeTransportUnion;
            RegistrationDate = stateRegistrationCertificate.RegistrationDate;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("State_Registration_Certificate", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "CodeTransportUnion", CodeTransportUnion } });
            DataModel.StateRegistrationCertificates.Add(new StateRegistrationCertificate(id, DataModel.DataAdapter, RegistrationNumber, CodeTransportUnion, RegistrationDate));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("State_Registration_Certificate", Id.Value);
            DataModel.StateRegistrationCertificates.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("State_Registration_Certificate", Convert.ToInt32(Id), new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "CodeTransportUnion", CodeTransportUnion }});
        }
    }
}
