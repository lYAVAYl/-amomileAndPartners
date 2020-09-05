using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СamomileAndPartners.Models
{

    public static class Mediator
    {
        // Словарь команд для смены представления и передачи параментра в метод
        private static IDictionary<string, List<Action<object>>> pageDictionary =
            new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// Добавление представления в словарь
        /// </summary>
        /// <param name="uiName">Команда для загрузки представления</param>
        /// <param name="action">Метод, который создаёт нужное представление</param>
        public static void Append(string uiName, Action<object> action)
        {
            if (!pageDictionary.ContainsKey(uiName))
            {
                var list = new List<Action<object>>();
                list.Add(action);
                pageDictionary.Add(uiName, list);
            }
            else
            {
                bool contains = false;
                foreach (var item in pageDictionary[uiName])
                    if (item.Method.ToString() == action.Method.ToString())
                        contains = true;
                if (!contains)
                    pageDictionary[uiName].Add(action);
            }
        }

        public static void Finish(string uiName, Action<object> action)
        {
            if (pageDictionary.ContainsKey(uiName))
                pageDictionary[uiName].Remove(action);
        }

        /// <summary>
        /// Вывод нужного представления
        /// </summary>
        /// <param name="uiName">Команда для загрузки представления</param>
        /// <param name="args">Параметры, которые нужно передать в метод, который создаёт нужное представление</param>
        public static void Inform(string uiName, object args = null)
        {
            if (pageDictionary.ContainsKey(uiName))
                foreach (var action in pageDictionary[uiName])
                    action(args);
        }
    }
}
