using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RaP_DAL;
using RaP_DAL.DAL;
using СamomileAndPartners.Models;

namespace СamomileAndPartners.ViewModels
{
    class AddEditCompanyViewModel:ValidateBaseViewModel, IPageViewModel
    {
        // Заголовок страницы
        public string Header { get; set; }

        public string Name { get; set; }

        // Статус договора
        public List<string> StatusList { get; set; }
        public string SelectedStatus { get; set; }

        // ID компании для редактирования (если null => добавление)
        private int? id;

        /// <summary>
        /// Для редактирования компании
        /// </summary>
        /// <param name="company">Компания</param>
        public AddEditCompanyViewModel(Company company):this()
        {
            id = company.Id;
            Name = company.Name;
            SelectedStatus = StatusList.FirstOrDefault(s => s == company.ContractStatus);

            Header = "Редактировать компанию «" + company + "»";
        }

        /// <summary>
        /// Для добавления компании
        /// </summary>
        public AddEditCompanyViewModel()
        {
            StatusList = new List<string>()
            {
                "Заключен"
                ,"Расторгнут"
                ,"Ещё не заключен"
            };

            Header = "Добавить компанию";
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
                    if (ErrorCollection[nameof(Name)] == null
                        && ErrorCollection[nameof(SelectedStatus)] == null)
                    {
                        if (id == null)
                        {
                            RaP_DAO.AddCompany(Name.Trim(), SelectedStatus);
                        }
                        else
                        {
                            RaP_DAO.EditCompany((int)id, Name.Trim(), SelectedStatus);
                        }
                    }

                    // Переход на страницу со списком компаний
                    Mediator.Inform("LoadCompaniesListPage");
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
                case nameof(Name):
                    if (string.IsNullOrEmpty(Name))
                    {
                        error = "Введите название компании";
                    }
                    else if (Name.Length < 3)
                    {
                        error = "Минимум 3 символа";
                    }
                    else if (Name.Length > 150)
                    {
                        error = "Максимум 150 символов";
                    }
                    else
                    {
                        bool atLeastOneLetter = false;
                        for (int i = 0; i < Name.Length && !atLeastOneLetter; i++)
                        {
                            atLeastOneLetter = char.IsLetter(Name[i]);
                        }

                        if (!atLeastOneLetter)
                        {
                            error = "Введите хотя бы 1 букву";
                        }
                    }
                    break;
                case nameof(SelectedStatus):
                    if (string.IsNullOrEmpty(SelectedStatus))
                    {
                        error = "Выберите статус договора";
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
