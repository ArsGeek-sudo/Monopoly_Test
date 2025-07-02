using SqlKata.Execution;
using SqlKata.Compilers;
using Npgsql;
using System.Collections.Generic;

namespace Monopoly_Test
{
    internal class GetData
    {
        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1;Database=monopoly_test";

        /// <summary>
        /// Получает все паллеты из таблицы pallets.
        /// </summary>
        public async Task<List<Pallet>?> GetPallets()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    PostgresCompiler compiler = new PostgresCompiler();
                    QueryFactory db = new QueryFactory(connection, compiler);

                    var result = await db.Query("pallets").GetAsync();

                    List<Pallet> pallets = new List<Pallet>();

                    foreach (var row in result)
                    {
                        pallets.Add(new Pallet
                        {
                            Id = row.id,
                            Width = row.width,
                            Height = row.height,
                            Depth = row.depth,
                            CreatedAt = row.created_at
                        });
                    }

                    return pallets;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка PostgreSQL!\n", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!\n", ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Получает все коробки из таблицы boxes.
        /// </summary>
        public async Task<List<Box>?> GetBoxes()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    PostgresCompiler compiler = new PostgresCompiler();
                    QueryFactory db = new QueryFactory(connection, compiler);

                    var result = await db.Query("boxes").GetAsync();

                    List<Box> boxes = new List<Box>();

                    foreach (var row in result)
                    {
                        boxes.Add(new Box
                        {
                            Id = row.id,
                            PalletId = row.pallet_id,
                            Width = row.width,
                            Height = row.height,
                            Depth = row.depth,
                            Weight = row.weight,
                            CreatedAt = row.created_at,
                            ProductionDate = row.production_date,
                            ExpirationDate = row.expiration_date
                        });
                    }

                    return boxes;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Ошибка PostgreSQL!\n", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка!\n", ex.Message);
            }

            return null;
        }
    }
}
