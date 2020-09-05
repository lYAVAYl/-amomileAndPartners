using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RaP_DAL;
using RaP_DAL.DAL;
using СamomileAndPartners.Models;

namespace СamomileAndPartners.ViewModels
{
    public class CompanyUsersViewModel:BaseViewModel, IPageViewModel
    {
        public string Header { get; set; }
        public UsersCollection CompanyUsers { get; set; }

        /// <summary>
        /// Для вывода пользователей определённой компании
        /// </summary>
        /// <param name="company">Компания</param>
        public CompanyUsersViewModel(Company company)
        {
            CompanyUsers = new UsersCollection(RaP_DAO.GetCompanyUsers(company));
            Header = "Сотрудники компании «" + company + "»";
        }

        /// <summary>
        /// Для вывода всех пользователей
        /// </summary>
        public CompanyUsersViewModel()
        {
            CompanyUsers = new UsersCollection(RaP_DAO.GetUsers());
            
            Header = "Список сотрудников";
        }


        private ICommand _loadAddUserPage;
        public ICommand LoadAddUserPage => _loadAddUserPage ?? (_loadAddUserPage = new RelayCommand(parameter =>
        { 
            Mediator.Inform("LoadAddUserPage");
        }));
    }

    /// <summary>
    /// Коллекция пользователей
    /// Каждый элемент коллекции имеет команды Удалить/Редактировать
    /// </summary>
    public class UsersCollection : ObservableCollection<User>
    {
        public UsersCollection(List<User> users = null) : base(users)
        {

        }


        private ICommand _deleteItemCommand;
        private ICommand _editItemCommand;
        private ICommand _openItemCommand;

        public ICommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is User item)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить сотрудника {item.Login}?"
                    , "Удалить?"
                    , MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    RaP_DAO.DeleteUser(item.Id);
                    Remove(item);
                }
            }
        }));

        public ICommand EditItemCommand => _editItemCommand ?? (_editItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is User item)
            {
                Mediator.Inform("LoadEditUserPage", item);
            }
        }));

        public ICommand OpenItemCommand => _openItemCommand ?? (_openItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is User item)
            {
                Mediator.Inform("LoadEditUserPage", item);
            }
        }));
    }
}
