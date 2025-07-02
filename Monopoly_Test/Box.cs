using SqlKata.Execution;
using SqlKata.Compilers;

namespace Monopoly_Test
{
    /// <summary>
    /// Представляет коробку, размещённую на паллете.
    /// </summary>
    public class Box
    {
        /// <summary>
        /// Уникальный идентификатор коробки.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор паллеты, на которой находится коробка.
        /// </summary>
        public long PalletId { get; set; }

        /// <summary>
        /// Ширина коробки (в см или мм — в зависимости от системы).
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота коробки.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Глубина (длина) коробки.
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Вес коробки.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Дата производства коробки. Может быть null.
        /// </summary>
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// Дата истечения срока годности. Может быть null.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Дата создания записи о коробке.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Вычисляемое свойство: объём коробки (Ш × В × Г).
        /// </summary>
        public double Volume => Width * Height * Depth;

        /// <summary>
        /// Вычисляемая дата истечения срока годности:
        /// если указана ExpirationDate — используется она,
        /// иначе вычисляется как ProductionDate + 100 дней.
        /// </summary>
        public DateTime CalculatedExpirationDate =>
            ExpirationDate ?? (ProductionDate.HasValue ? ProductionDate.Value.AddDays(100) : DateTime.MinValue);
    }
}
