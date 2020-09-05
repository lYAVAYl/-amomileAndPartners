using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RaP_DAL;
using RaP_DAL.DAL;
using СamomileAndPartners.Models;

namespace СamomileAndPartners.ViewModels
{
    public class AddEditUserViewModel : ValidateBaseViewModel, IPageViewModel
    {
        // Заголовк страницы
        public string Header { get; set; }

        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        // Список доступных компаний
        public List<Company> CompaniesList { get; set; }
        public Company SelectedCompany { get; set; }

        // ID сотрудника для редактирования (если null => добавление сотрудника)
        private int? id;

        // На редактирование
        public AddEditUserViewModel(User user):this()
        {
            id = user.Id;
            Login = user.Login;
            Password = user.Password;
            SelectedCompany = CompaniesList.FirstOrDefault(x=>x.Id == user.CompanyId);

            Header = "Редактирование сотрудника";
        }

        // На добавление
        public AddEditUserViewModel()
        {
            CompaniesList = RaP_DAO.GetCompanies();

            Header = "Добавить сотрудника";
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        private ICommand _save;
        public ICommand Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand(x =>
                {
                    // Проверка на отсутсвие ошибок валидации
                    if (ErrorCollection[nameof(Login)] == null
                        && ErrorCollection[nameof(Password)] == null
                        && ErrorCollection[nameof(SelectedCompany)] == null)
                    {
                        if (id == null)
                        {
                            RaP_DAO.AddUser(Login, Password, SelectedCompany);
                        }
                        else
                        {
                            RaP_DAO.EditUser((int) id, Login, Password, SelectedCompany);
                        }

                        // Переход на страницу компании со списком сотрудников
                        Mediator.Inform("LoadCompanyUsersPage", SelectedCompany);
                    }
                }));
            }
        }


        /// <summary>
        /// Валидация страницы
        /// </summary>
        /// <param name="columnName">Название элемента для валидации</param>
        /// <returns></returns>
        public override string Validate(string columnName)
        {
            // Текст ошибки
            string error = null;

            switch (columnName)
            {
                case nameof(Login):
                    Login = Login.Trim();
                    if (string.IsNullOrEmpty(Login))
                    {
                        error = "Логин не введён";
                    }
                    else if (Login.Length < 3)
                    {
                        error = "Минимум 3 символа";
                    }
                    else if (Login.Length > 150)
                    {
                        error = "Максимум 150 символов";
                    }
                    else if (RaP_DAO.IsLoginExists(id ?? 0, Login))
                    {
                        error = "Этот логин занят";
                    }
                    else
                    {
                        // Проверка, что в логин состоит только из букв и цифр
                        bool onlyLettersAndDigits = false;
                        bool atLeastOneLetter = false;

                        for (int i = 0; i < Login.Length 
                                        && !onlyLettersAndDigits 
                                        && !atLeastOneLetter; i++)
                        {
                            onlyLettersAndDigits = char.IsLetter(Login[i]) || char.IsDigit(Login[i]);
                            atLeastOneLetter = char.IsLetter(Password[i]);
                        }

                        if (!onlyLettersAndDigits)
                        {
                            error = "Логин должен состоять только из букв и цифр";
                        }
                        else if (!atLeastOneLetter)
                        {
                            error = "Введите хотя бы 1 букву";
                        }
                    }
                    break;
                case nameof(Password):
                    Password = Password.Trim();
                    if (string.IsNullOrEmpty(Password))
                    {
                        error = "Введите пароль";
                    }
                    else if (Password.Length < 6)
                    {
                        error = "Минимум 6 символов";
                    }
                    else if (Password.Length > 50)
                    {
                        error = "Максимум 50 символов";
                    }
                    else if (Password == Login)
                    {
                        error = "Пароль не может совпадать с логином";
                    }
                    else
                    {
                        // Проверка, что в пароле есть хотя бы 1 буква
                        bool atLeastOneLetter = false;
                        for (int i = 0; i < Password.Length && !atLeastOneLetter; i++)
                        {
                            atLeastOneLetter = char.IsLetter(Password[i]);
                        }

                        if (!atLeastOneLetter)
                        {
                            error = "Введите хотя бы 1 букву";
                        }
                    }
                    break;
                case nameof(SelectedCompany):
                    if (SelectedCompany == null)
                    {
                        error = "Выберите компанию сотрудника";
                    }
                    break;
            }

            if (ErrorCollection.ContainsKey(columnName))
                ErrorCollection[columnName] = error;
            else ErrorCollection.Add(columnName, error);

            OnPropertyChanged(nameof(ErrorCollection));

            return error;
        }

    }
}
