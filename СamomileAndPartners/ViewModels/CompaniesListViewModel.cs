using RaP_DAL;
using RaP_DAL.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using СamomileAndPartners.Models;

namespace СamomileAndPartners.ViewModels
{
    public class CompaniesListViewModel:BaseViewModel, IPageViewModel
    {
        public CompaniesCollection Companies { get; set; }

        public CompaniesListViewModel()
        {
            Companies = new CompaniesCollection(RaP_DAO.GetCompanies());
        }

        private ICommand _loadAddCompanyPage;
        public ICommand LoadAddCompanyPage => _loadAddCompanyPage ?? (_loadAddCompanyPage = new RelayCommand(parameter =>
        {
            Mediator.Inform("LoadAddCompanyPage");
        }));
    }

    /// <summary>
    /// Коллекция компаний
    /// Каждый элемент коллекции имеет команды Удалить/Редактировать
    /// </summary>
    public class CompaniesCollection : ObservableCollection<Company>
    {
        public CompaniesCollection(List<Company> companies = null) : base(companies)
        {

        }

        private ICommand _deleteItemCommand;
        private ICommand _editItemCommand;
        private ICommand _openItemCommand;

        public ICommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is Company item)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить компанию «{item.Name}» ?"
                    , "Удалить?"
                    , MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    RaP_DAO.DeleteCompany(item.Id);
                    Remove(item);
                }
            }
        }));

        public ICommand EditItemCommand => _editItemCommand ?? (_editItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is Company item)
            {
                Mediator.Inform("LoadEditCompanyPage", item);
            }
        }));

        public ICommand OpenItemCommand => _openItemCommand ?? (_openItemCommand = new RelayCommand(parameter =>
        {
            if (parameter is Company item)
            {
                Mediator.Inform("LoadCompanyUsersPage", item);
            }
        }));

    }
}
