using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RaP_DAL;
using RaP_DAL.DAL;
using СamomileAndPartners.Models;
using СamomileAndPartners.ViewModels;

namespace СamomileAndPartners.Views
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;


        // Список страниц, доступных для отрисовки
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        // Свойство, отвечающее за отображение нужной страницы
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        // Изменить текущую страницу
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            CurrentPageViewModel = viewModel;
        }


        /// <summary>
        /// Загрузить список компаний
        /// </summary>
        private ICommand _loadCompaniesListPage;
        public ICommand LoadCompaniesListPage
        {
            get
            {
                return _loadCompaniesListPage ?? (_loadCompaniesListPage = new RelayCommand(x =>
                {
                    CompaniesList("");
                }));
            }
        }

        /// <summary>
        /// Загрузить список пользоветелй
        /// </summary>
        private ICommand _loadUsersListPage;
        public ICommand LoadUsersListPage
        {
            get
            {
                return _loadUsersListPage ?? (_loadUsersListPage = new RelayCommand(x =>
                {
                    UsersList("");
                }));
            }
        }



        /// <summary>
        /// Список компаний
        /// </summary>
        /// <param name="obj"></param>
        private void CompaniesList(object obj)
        {
            ChangeViewModel(new CompaniesListViewModel());
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="obj"></param>
        private void AddUser(object obj)
        {
            ChangeViewModel(new AddEditUserViewModel());
        }

        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="obj"></param>
        private void EditUser(object obj)
        {
            ChangeViewModel(new AddEditUserViewModel((User)obj));
        }

        /// <summary>
        /// Добавить компанию
        /// </summary>
        /// <param name="obj"></param>
        private void AddCompany(object obj)
        {
            ChangeViewModel(new AddEditCompanyViewModel());
        }

        /// <summary>
        /// Редактировать компанию
        /// </summary>
        /// <param name="obj"></param>
        private void EditCompany(object obj)
        {
            ChangeViewModel(new AddEditCompanyViewModel((Company)obj));
        }

        /// <summary>
        /// Список сотрудников компании
        /// </summary>
        /// <param name="obj"></param>
        private void CompanyUsers(object obj)
        {
            ChangeViewModel(new CompanyUsersViewModel((Company)obj));
        }

        /// <summary>
        /// Список всех сотрудников
        /// </summary>
        /// <param name="obj"></param>
        private void UsersList(object obj)
        {
            ChangeViewModel(new CompanyUsersViewModel());
        }

        public MainWindowViewModel()
        {
            // Загрузка первой страницы
            CurrentPageViewModel = new CompaniesListViewModel();


            // Установка команд
            Mediator.Append("LoadCompaniesListPage", CompaniesList);
            Mediator.Append("LoadUsersPage", UsersList);
            Mediator.Append("LoadCompanyUsersPage", CompanyUsers);

            Mediator.Append("LoadEditUserPage", EditUser);
            Mediator.Append("LoadAddUserPage", AddUser);

            Mediator.Append("LoadEditCompanyPage", EditCompany);
            Mediator.Append("LoadAddCompanyPage", AddCompany);
        }

    }
}
