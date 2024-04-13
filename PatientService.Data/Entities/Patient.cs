using HelpersDTO.Base.Models;
using HelpersDTO.CallCenter.DTO.Models;

namespace PatientService.Data.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }
        /// <summary>
        /// Данные документа
        /// </summary>
        public DocumentDTO Document { get; set; }
        public List<ContactDTO> Contacts { get; set; }
        /// <summary>
        /// Статус пациента
        /// </summary>
        public RelevanceStatusEnum Status { get; set; } = RelevanceStatusEnum.New;
    }
}
