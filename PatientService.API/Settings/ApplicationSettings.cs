namespace PatientService.API.Settings
{
    public class ApplicationSettings
    {
        public RabbitMqSetting RabbitMqSetting { get; set; }
        public string ConnectionString { get; set; }
        public ApplicationSettings(RabbitMqSetting rabbitMqSetting, string connectionString)
        {
            RabbitMqSetting = rabbitMqSetting ?? throw new ArgumentNullException(nameof(rabbitMqSetting));
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }
}
