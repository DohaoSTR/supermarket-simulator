using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class ConformityDeclaration : DataBaseEntity
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

        public ConformityDeclaration(int registrationNumber, string codeTransportUnion, DateTime registrationDate, DateTime endDate)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
            EndDate = endDate;
        }

        public ConformityDeclaration(long? id, IDataAdapter dataAdapter, int registrationNumber, string codeTransportUnion, DateTime registrationDate, DateTime endDate) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
            EndDate = endDate;
        }

        public ConformityDeclaration()
        {
        }

        public ConformityDeclaration(int id)
        {
            List<ConformityDeclaration> conformityDeclarations = DataModel.ConformityDeclarations.FindAll(s => s.Id == id);
            ConformityDeclaration conformityDeclaration = new ConformityDeclaration();
            foreach (ConformityDeclaration conf in conformityDeclarations)
            {
                conformityDeclaration = new ConformityDeclaration(conf.Id, DataAdapter, conf.RegistrationNumber, conf.CodeTransportUnion,
                conf.RegistrationDate, conf.EndDate);
            }

            Id = conformityDeclaration.Id;
            DataAdapter = conformityDeclaration.DataAdapter;
            RegistrationNumber = conformityDeclaration.RegistrationNumber;
            CodeTransportUnion = conformityDeclaration.CodeTransportUnion;
            RegistrationDate = conformityDeclaration.RegistrationDate;
            EndDate = conformityDeclaration.EndDate;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Conformity_Declaration", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "CodeTransportUnion", CodeTransportUnion }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "EndDate", EndDate.ToString("yyyy-MM-dd") } });
            DataModel.ConformityDeclarations.Add(new ConformityDeclaration(id, DataModel.DataAdapter, RegistrationNumber, CodeTransportUnion, RegistrationDate, EndDate));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Conformity_Declaration", Id.Value);
            DataModel.ConformityDeclarations.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Conformity_Declaration", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber },{ "CodeTransportUnion", CodeTransportUnion }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }, { "EndDate", EndDate.ToString("yyyy-MM-dd")}});
        }
    }
}
