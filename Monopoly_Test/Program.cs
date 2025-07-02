using SqlKata.Compilers;

namespace Monopoly_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] options = { "Просмотреть паллеты", "Отобразить палеты с наибольшим сроком годности\n" +
                    "  отсортированные по возрастанию объёма",
                "Просмотреть коробки", "Добавить паллету", "Добавить коробку", "Выход" };
            int selectedIndex = 0;
            GetData getData = new GetData();
            Menu menu = new Menu();

            bool isWorks = true;

            ConsoleKey key;

            while (isWorks)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Выберите опцию (стрелками ↑ ↓, Enter для выбора):\n");

                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            // Подсветка текущего выбора
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.WriteLine($"  {options[i]}");

                        // Сброс цвета
                        Console.ResetColor();
                    }

                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                    }

                } while (key != ConsoleKey.Enter);

                Console.Clear();
                Console.WriteLine($"Вы выбрали: {options[selectedIndex]}\n");

                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine("Загрузка паллет...");

                        var pallets = getData.GetPallets().Result;

                        if (pallets != null)
                        {
                            // Сортировка по ExpirationDate от самой дальней даты к ближайшей
                            pallets = pallets
                                .OrderByDescending(pallet => pallet.ExpirationDate ?? DateTime.MinValue)
                                .ToList();

                            menu.PalletDialogue(pallets);
                        }
                        break;
                    case 1:
                        Console.WriteLine("Загрузка паллет...");

                        pallets = getData.GetPallets().Result;

                        if (pallets != null)
                        {
                            // Сортировка по ExpirationDate от самой дальней даты к ближайшей
                            pallets = pallets
                                .OrderByDescending(pallet => pallet.ExpirationDate ?? DateTime.MinValue)
                                .ToList();

                            menu.ShowTopPallets(pallets);
                        }
                        break;
                    case 2:

                        Console.ReadKey();
                        break;
                    case 3:

                        Console.ReadKey();
                        break;
                    case 4:

                        Console.ReadKey();
                        break;
                    case 5:
                        isWorks = false;
                        break;
                }
            }
        }
    }
}
