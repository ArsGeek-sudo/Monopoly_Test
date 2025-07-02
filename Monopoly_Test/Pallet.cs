using SqlKata.Execution;
using SqlKata.Compilers;

namespace Monopoly_Test
{
    /// <summary>
    /// Представляет паллету на складе, содержащую коробки.
    /// </summary>
    public class Pallet
    {
        /// <summary>
        /// Уникальный идентификатор паллеты.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ширина паллеты.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота паллеты.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Глубина паллеты.
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Дата создания паллеты.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Список коробок, находящихся на паллете.
        /// </summary>
        public List<Box> Boxes { get; set; } = new List<Box>();

        /// <summary>
        /// Собственный вес паллеты (30 кг по условию).
        /// </summary>
        public double OwnWeight => 30.0;

        /// <summary>
        /// Общий вес паллеты = собственный вес + вес всех коробок.
        /// </summary>
        public double TotalWeight => OwnWeight + Boxes.Sum(b => b.Weight);

        /// <summary>
        /// Объём паллеты без учёта коробок.
        /// </summary>
        public double OwnVolume => Width * Height * Depth;

        /// <summary>
        /// Общий объём паллеты = объём самой паллеты + объём всех коробок.
        /// </summary>
        public double TotalVolume => OwnVolume + Boxes.Sum(b => b.Volume);

        /// <summary>
        /// Срок годности паллеты: минимальный срок годности среди всех коробок.
        /// Если коробок нет, то null.
        /// </summary>
        public DateTime? ExpirationDate =>
            Boxes.Any() ? Boxes.Min(b => b.CalculatedExpirationDate) : null;

        /// <summary>
        /// Проверка, может ли коробка поместиться на паллету по габаритам (ширина и глубина).
        /// </summary>
        public bool CanContain(Box box) =>
            box.Width <= Width && box.Depth <= Depth;
    }
}
