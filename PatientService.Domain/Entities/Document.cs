namespace PatientService.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        public string? Series { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        public string? SubdivisionCode { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string? IssuedBy { get; set; }

        /// <summary>
        /// Актуальный документ
        /// </summary>
        public bool? IsCurrent { get; set; }

        public virtual Patient? Patient { get; set; } = new();
    }
}
