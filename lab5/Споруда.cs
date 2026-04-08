using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace lab5
{
    /// <summary>
    /// Модель споруди з чотирма властивостями різних типів (одна — колекція),
    /// трьома методами та двома конструкторами.
    /// </summary>
    public class Споруда
    {
        public string Назва { get; set; }

        public double ПлощаКвМ { get; set; }

        public int КількістьПоверхів { get; set; }

        public List<string> Приміщення { get; set; }

        public Споруда()
        {
            Назва = "Без назви";
            ПлощаКвМ = 0;
            КількістьПоверхів = 1;
            Приміщення = new List<string>();
        }

        public Споруда(string назва, double площаКвМ, int кількістьПоверхів, IEnumerable<string> приміщення)
        {
            Назва = назва;
            ПлощаКвМ = площаКвМ;
            КількістьПоверхів = кількістьПоверхів;
            Приміщення = приміщення != null ? new List<string>(приміщення) : new List<string>();
        }

        public void ДодатиПриміщення(string назваПриміщення)
        {
            if (!string.IsNullOrWhiteSpace(назваПриміщення))
                Приміщення.Add(назваПриміщення.Trim());
        }

        public double РозрахуватиВартістьУтримання(double ставкаЗаКвМ)
        {
            return ПлощаКвМ * ставкаЗаКвМ;
        }

        public string КороткийОпис()
        {
            return $"{Назва}: {ПлощаКвМ} м², {КількістьПоверхів} пов., приміщень: {Приміщення.Count}";
        }

        /// <summary>
        /// Виводить у TreeView перелік усіх публічних властивостей об'єкта Споруда:
        /// ім'я, тип та значення (для колекції — елементи як дочірні вузли).
        /// </summary>
        public static void ВивестиВластивостіУTreeView(Споруда обєкт, TreeView treeView)
        {
            if (treeView == null || обєкт == null)
                return;

            treeView.Nodes.Clear();
            var root = new TreeNode(typeof(Споруда).Name);
            treeView.Nodes.Add(root);

            foreach (var prop in typeof(Споруда).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object value;
                try
                {
                    value = prop.GetValue(обєкт);
                }
                catch
                {
                    value = null;
                }

                var typeName = prop.PropertyType.Name;
                string valueText = ФорматЗначення(value);
                var node = new TreeNode($"{prop.Name}  |  Тип: {typeName}  |  Значення: {valueText}");
                root.Nodes.Add(node);

                if (value != null && !(value is string) && value is IEnumerable enumerable)
                {
                    int i = 0;
                    foreach (var item in enumerable)
                    {
                        node.Nodes.Add(new TreeNode($"[{i++}]  {item ?? "(null)"}"));
                    }
                }
            }

            treeView.ExpandAll();
        }

        private static string ФорматЗначення(object value)
        {
            if (value == null)
                return "(null)";

            if (value is string s)
                return s;

            if (value is IEnumerable && !(value is string))
            {
                var list = (value as IEnumerable).Cast<object>().ToList();
                return $"колекція, елементів: {list.Count}";
            }

            return value.ToString();
        }
    }
}
