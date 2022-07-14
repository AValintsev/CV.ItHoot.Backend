using CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator.Interfaces;
using System;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator
{
    public class DateConfig : IConfigProperty
    {
        public string PropertyName { get; }

        public DateConfig(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string ConfigureValue(object property)
        {
            if (property is DateTime birthdate)
            {
                var date = birthdate.ToString("MM/yyyy");
                return date;
            }

            throw new ArgumentException("Invalid type");
        }
    }
}
