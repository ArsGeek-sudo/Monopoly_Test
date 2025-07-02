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

        public void ShowTopPallets(List<Pallet> pallets)
        {
            if (pallets == null || pallets.Count == 0)
            {
                Console.WriteLine("Паллеты отсутствуют.");
                return;
            }

            // Фильтрация паллет, которые содержат коробки
            var filteredPallets = pallets
                .Where(pallet => pallet.Boxes != null && pallet.Boxes.Count > 0)
                .Select(pallet => new
                {
                    Pallet = pallet,
                    MaxExpirationDate = pallet.Boxes.Max(box => box.CalculatedExpirationDate)
                })
                .OrderByDescending(pallet => pallet.MaxExpirationDate) // Сортировка по убыванию максимального срока годности
                .ThenBy(pallet => pallet.Pallet.TotalVolume)          // Сортировка по возрастанию объема
                .Take(3)                                   // Выбор 3 паллет
                .ToList();

            if (filteredPallets.Count == 0)
            {
                Console.WriteLine("Нет паллет с коробками.");
                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Топ-3 паллеты с коробками с наибольшим сроком годности, отсортированные по объему:\n");

            foreach (var item in filteredPallets)
            {
                var pallet = item.Pallet;
                Console.WriteLine($"Паллета {pallet.Id}:");
                Console.WriteLine($"  Общий объем: {pallet.TotalVolume} м3");
                Console.WriteLine($"  Палета годна до: {item.MaxExpirationDate.ToShortDateString()}");
                Console.WriteLine($"  Количество коробок: {pallet.Boxes.Count}\n");
            }

            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
            Console.ReadKey();
        }

        public void BoxDialogue(List<Box> boxes)
        {
            if (boxes != null && boxes.Count > 0)
            {
                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Выберите коробку (стрелками ↑ ↓, Enter для выбора):\n");

                    for (int i = 0; i < boxes.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.WriteLine($"  Коробка {boxes[i].Id}");

                        Console.ResetColor();
                    }

                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex == 0) ? boxes.Count - 1 : selectedIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex == boxes.Count - 1) ? 0 : selectedIndex + 1;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine($"Вы выбрали коробку {boxes[selectedIndex].Id}\n");
                        Console.WriteLine($"Ширина: {boxes[selectedIndex].Width} см");
                        Console.WriteLine($"Высота: {boxes[selectedIndex].Height} см");
                        Console.WriteLine($"Глубина: {boxes[selectedIndex].Depth} см");
                        Console.WriteLine($"Вес: {boxes[selectedIndex].Weight} кг");
                        Console.WriteLine($"Объём: {boxes[selectedIndex].Volume} м3");
                        Console.WriteLine($"Срок годности: {boxes[selectedIndex].CalculatedExpirationDate.ToShortDateString()}\n");

                        Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
                        Console.ReadKey();
                        break;
                    }

                } while (true);
            }
            else
            {
                Console.WriteLine("Коробки отсутствуют.");
                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
                Console.ReadKey();
            }
        }
    }
}
