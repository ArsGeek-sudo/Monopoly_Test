using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Test
{
    internal class Menu
    {
        int selectedIndex = 0;

        ConsoleKey key;

        public void PalletDialogue(List<Pallet> pallets)
        {
            if (pallets != null && pallets.Count > 0)
            {
                // Сортировка по ExpirationDate от самой дальней даты к ближайшей
                pallets = pallets
                    .OrderByDescending(pallet => pallet.ExpirationDate ?? DateTime.MinValue)
                    .ToList();

                do
                {
                    Console.Clear();
                    Console.WriteLine("Выберите палету (стрелками ↑ ↓, Enter для выбора):\n");

                    for (int i = 0; i < pallets.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.WriteLine($"  Паллета {pallets[i].Id}");

                        Console.ResetColor();
                    }

                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex == 0) ? pallets.Count - 1 : selectedIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex == pallets.Count - 1) ? 0 : selectedIndex + 1;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine($"Вы выбрали паллету {pallets[selectedIndex].Id}\n");
                        Console.WriteLine($"Ширина: {pallets[selectedIndex].Width} см");
                        Console.WriteLine($"Высота: {pallets[selectedIndex].Height} см");
                        Console.WriteLine($"Глубина: {pallets[selectedIndex].Depth} см");
                        Console.WriteLine($"Собственный вес: {pallets[selectedIndex].OwnWeight} кг");
                        Console.WriteLine($"Общий вес: {pallets[selectedIndex].TotalWeight} кг");
                        Console.WriteLine($"Объём: {pallets[selectedIndex].TotalVolume} м3");
                        Console.WriteLine($"Срок годности: {(pallets[selectedIndex].ExpirationDate.HasValue ?
                            pallets[selectedIndex].ExpirationDate.Value.ToShortDateString() : "Нет")}\n");

                        if (pallets[selectedIndex].Boxes.Count > 0)
                        {
                            Console.WriteLine("Коробки на паллете:");
                            foreach (var box in pallets[selectedIndex].Boxes)
                            {
                                Console.WriteLine($"  - Коробка {box.Id}: {box.Width}x{box.Height}x{box.Depth} м, вес {box.Weight} кг, срок годности {box.CalculatedExpirationDate.ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("На паллете нет коробок.");
                        }

                        Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
                        Console.ReadKey();
                        break;
                    }

                } while (true);
            }
            else
            {
                Console.WriteLine($"Паллеты отсутствуют");
            }
        }

    }
}
