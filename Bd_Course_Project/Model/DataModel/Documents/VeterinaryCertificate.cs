using System;
using System.Collections.Generic;

namespace Bd_Course_Project
{
    public class VeterinaryCertificate : DataBaseEntity
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

        private string _form;
        public string Form
        {
            get => _form;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 3)
                {
                    _form = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Форма сопроводительного документа должна быть заполнена!");
                }
            }
        }

        public DateTime RegistrationDate { get; set; }

        public VeterinaryCertificate(int registrationNumber, string form, DateTime registrationDate)
        {
            RegistrationNumber = registrationNumber;
            Form = form;
            RegistrationDate = registrationDate;
        }

        public VeterinaryCertificate(long? id, IDataAdapter dataAdapter, int registrationNumber, string form, DateTime registrationDate) : base(id, dataAdapter)
        {
            RegistrationNumber = registrationNumber;
            Form = form;
            RegistrationDate = registrationDate;
        }

        public VeterinaryCertificate()
        {
        }

        public VeterinaryCertificate(int id)
        {
            List<VeterinaryCertificate> veterinaryCertificates = DataModel.VeterinaryCertificates.FindAll(s => s.Id == id);
            VeterinaryCertificate veterinaryCertificate = new VeterinaryCertificate();
            foreach (VeterinaryCertificate conf in veterinaryCertificates)
            {
                veterinaryCertificate = new VeterinaryCertificate(conf.Id, DataAdapter, conf.RegistrationNumber, conf.Form,
                conf.RegistrationDate);
            }

            Id = veterinaryCertificate.Id;
            DataAdapter = veterinaryCertificate.DataAdapter;
            RegistrationNumber = veterinaryCertificate.RegistrationNumber;
            Form = veterinaryCertificate.Form;
            RegistrationDate = veterinaryCertificate.RegistrationDate;
        }

        public override long Add()
        {
            long? id = DataModel.DataAdapter.InsertRow("Veterinary_Certificate", new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber }, { "Form", Form }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") } });
            DataModel.VeterinaryCertificates.Add(new VeterinaryCertificate(id, DataModel.DataAdapter, RegistrationNumber, Form, RegistrationDate));

            return (long)id;
        }

        public override void Remove()
        {
            DataModel.DataAdapter.DeleteRow("Veterinary_Certificate", Id.Value);
            DataModel.VeterinaryCertificates.Remove(this);
        }

        public override void Update()
        {
            DataModel.DataAdapter.UpdateRow("Veterinary_Certificate", Id.Value, new Dictionary<string, object>()
            { { "RegistrationNumber", RegistrationNumber },{ "Form", Form }, { "RegistrationDate", RegistrationDate.ToString("yyyy-MM-dd") }});
        }
    }
}
