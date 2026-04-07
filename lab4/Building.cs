using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    /// <summary>
    /// Lab variant 3: Building class (reflection / dynamic type identification).
    /// </summary>
    public class Building
    {
        public string Назва { get; set; }
        public int Поверхів { get; set; }
        public double ПлощаКвМ { get; set; }
        public List<string> Приміщення { get; set; }

        public Building()
        {
            Назва = string.Empty;
            Поверхів = 1;
            ПлощаКвМ = 0;
            Приміщення = new List<string>();
        }

        public Building(string name, int floorCount, double areaSquareMeters, IEnumerable<string> rooms)
        {
            Назва = name;
            Поверхів = floorCount;
            ПлощаКвМ = areaSquareMeters;
            Приміщення = new List<string>(rooms ?? Enumerable.Empty<string>());
        }

        public string GetShortDescription()
        {
            return string.Format("{0}: {1} floors, {2:F1} m²", Назва, Поверхів, ПлощаКвМ);
        }

        public bool ContainsFloor(int floorNumber)
        {
            return floorNumber >= 1 && floorNumber <= Поверхів;
        }

        public void AddRoom(string roomName)
        {
            if (!string.IsNullOrWhiteSpace(roomName))
                Приміщення.Add(roomName.Trim());
        }
    }
}
