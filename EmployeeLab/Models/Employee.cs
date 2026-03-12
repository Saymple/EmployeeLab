using System.ComponentModel.DataAnnotations;

namespace EmployeeLab.Models
{
    public class Employee
    {
        public Employee()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Position = string.Empty;
            Department = string.Empty;
            Email = string.Empty;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName => $"{LastName} {FirstName}";

        [Required(ErrorMessage = "Должность обязательна")]
        [StringLength(100, ErrorMessage = "Должность не может превышать 100 символов")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Отдел обязателен")]
        [StringLength(50, ErrorMessage = "Название отдела не может превышать 50 символов")]
        [Display(Name = "Отдел")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Зарплата обязательна")]
        [Range(19242, 1000000, ErrorMessage = "Зарплата должна быть от 19 242 (МРОТ) до 1 000 000 рублей")]
        [Display(Name = "Зарплата (руб)")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Дата найма обязательна")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата найма")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Введите корректный email адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }

        [Display(Name = "Активный сотрудник")]
        public bool IsActive { get; set; } = true;

        // Метод для расчета стажа работы в годах
        public int GetExperience()
        {
            var today = DateTime.Today;
            var experience = today.Year - HireDate.Year;

            if (HireDate.Date > today.AddYears(-experience))
            {
                experience--;
            }

            return experience < 0 ? 0 : experience;
        }

        // Метод для расчета месячной зарплаты с налогом
        public decimal GetNetSalary()
        {
            return Salary * 0.87m;
        }
    }
}