using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RaP_DAL.DAL
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public static class RaP_DAO
    {
        #region User Commands (команды, связанные с сотрудниками)

        public static User GetUser(int id)
        {
            using (var context = new RaPContext())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        /// <summary>
        /// Проверка на существование логина
        /// </summary>
        /// <param name="id">ID сотрудника, у которого меняется логин</param>
        /// <param name="newLogin">Новый логин</param>
        /// <returns></returns>
        public static bool IsLoginExists(int id, string newLogin)
        {
            using (var context = new RaPContext())
            {
                // Полученить сотрудника, у которого меняется логин
                User user = context.Users.FirstOrDefault(u => u.Id == id);

                // Получить сотрудника с таким же логином, как и новый
                User sameLoginUser = context.Users.FirstOrDefault(u => u.Login == newLogin);

                // Существут ли сотрудник с идентичным логином и является ли он
                // тем же сотрудником, у которого меняется логин
                return sameLoginUser != null && user != sameLoginUser;
            }
        }

        public static List<User> GetUsers()
        {
            using (var context = new RaPContext())
            {
                return context.Users.ToList();
            }
        }

        public static List<User> GetCompanyUsers(Company company)
        {
            using (var context = new RaPContext())
            {
                return (from u in context.Users
                        where u.CompanyId == company.Id
                        select u).ToList();
            }
        }

        public static void AddUser(string login, string password, Company company)
        {
            using (var context = new RaPContext())
            {
                context.Users.Add(new User()
                {
                    Login = login,
                    Password = password,
                    CompanyId = company.Id
                });

                context.SaveChanges();
            }
        }

        public static void EditUser(int id, string login, string password, Company company)
        {
            using (var context = new RaPContext())
            {
                User old = context.Users.FirstOrDefault(u => u.Id == id);

                old.Login = login;
                old.Password = password;
                old.CompanyId = company.Id;

                context.SaveChanges();
            }
        }

        public static void DeleteUser(int id)
        {
            using (var context = new RaPContext())
            {
                User user = context.Users.FirstOrDefault(u=>u.Id == id);
                user.CompanyId = null;

                context.SaveChanges();
            }
        }

        #endregion



        #region Company Commands (команды, связанные с компаниями)

        public static List<Company> GetCompanies()
        {
            List<Company> companies;
            using (var context = new RaPContext())
            {
                companies = (from c in context.Companies
                             select c).ToList();
            }

            return companies;
        }

        public static Company GetCompany(int id)
        {
            using (var context = new RaPContext())
            {
                return context.Companies.FirstOrDefault(c => c.Id == id);
            }
        }

        public static void AddCompany(string name, string status)
        {
            using (var context = new RaPContext())
            {
                context.Companies.Add(new Company()
                {
                    Name = name,
                    ContractStatus = status
                });
                
                context.SaveChanges();
            }
        }

        public static void EditCompany(int id, string name, string status)
        {
            using (var context = new RaPContext())
            {
                Company old = context.Companies.FirstOrDefault(c => c.Id == id);

                old.Name = name;
                old.ContractStatus = status;


                context.SaveChanges();
            }
        }

        public static void DeleteCompany(int id)
        {
            using (var context = new RaPContext())
            {
                Company company = context.Companies.FirstOrDefault(c => c.Id == id);
                context.Companies.Remove(company);

                context.SaveChanges();
            }
        }


        #endregion
    }
}
