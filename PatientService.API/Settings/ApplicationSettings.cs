﻿namespace PatientService.API.Settings
{
    public class ApplicationSettings
    {
        public RabbitMqSetting RabbitMqSetting { get; set; }
        public string ConnectionString { get; set; }
    }
}
