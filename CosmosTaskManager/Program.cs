using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosTaskManager
{
    class Program
    {
        // ⚠️ Replace with your actual CosmosDB connection values from Azure Portal
        private static readonly string EndpointUri = "https://db-step.documents.azure.com:443/";
        private static readonly string PrimaryKey   = "8UsHzGOWjnYcc941W5EjGxsfRrCbFpLXQXISl6UH8VozVyxh8FnNijWv8eesRYN9u6yi52TfcdlhACDbCO8Xqg==";
        private static readonly string DatabaseId   = "TasksDB";
        private static readonly string ContainerId  = "TasksDB";

        private static CosmosClient   _client    = null!;
        private static Database       _database  = null!;
        private static Container      _container = null!;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════════════════════════════════╗");
            Console.WriteLine("║   CosmosDB Task Manager — CRUD   ║");
            Console.WriteLine("╚══════════════════════════════════╝\n");

            _client = new CosmosClient(EndpointUri, PrimaryKey,
                new CosmosClientOptions { ApplicationName = "CosmosTaskManager" });

            await InitializeDatabaseAsync();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n──────────────────────────────────");
                Console.WriteLine("  1. Создать задачу");
                Console.WriteLine("  2. Просмотреть все задачи");
                Console.WriteLine("  3. Найти задачу по ID");
                Console.WriteLine("  4. Обновить задачу");
                Console.WriteLine("  5. Удалить задачу");
                Console.WriteLine("  6. Выход");
                Console.WriteLine("──────────────────────────────────");
                Console.Write("Выберите действие: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": await CreateTaskAsync();      break;
                    case "2": await ReadAllTasksAsync();    break;
                    case "3": await ReadTaskByIdAsync();    break;
                    case "4": await UpdateTaskAsync();      break;
                    case "5": await DeleteTaskAsync();      break;
                    case "6": running = false;              break;
                    default:  Console.WriteLine("❌ Неверный выбор."); break;
                }
            }

            _client.Dispose();
        }

        // ─── DB Init ──────────────────────────────────────────────────────────────
        static async Task InitializeDatabaseAsync()
        {
            Console.WriteLine("🔌 Подключение к CosmosDB...");

            DatabaseResponse dbResp = await _client.CreateDatabaseIfNotExistsAsync(DatabaseId);
            _database = dbResp.Database;
            Console.WriteLine($"✅ База данных '{DatabaseId}' готова.");

            ContainerResponse cResp = await _database.CreateContainerIfNotExistsAsync(
                new ContainerProperties(ContainerId, "/createdDate"));
            _container = cResp.Container;
            Console.WriteLine($"✅ Контейнер '{ContainerId}' готов.\n");
        }

        // ─── CREATE ───────────────────────────────────────────────────────────────
        static async Task CreateTaskAsync()
        {
            Console.Write("\nЗаголовок: ");
            string title = Console.ReadLine() ?? "Без названия";

            Console.Write("Описание: ");
            string description = Console.ReadLine() ?? "";

            var task = new TaskItem
            {
                Id          = Guid.NewGuid().ToString(),
                Title       = title,
                Description = description,
                CreatedDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                DeletedDate = null
            };

            ItemResponse<TaskItem> response =
                await _container.CreateItemAsync(task, new PartitionKey(task.CreatedDate));

            Console.WriteLine($"\n✅ Задача создана!");
            PrintTask(response.Resource);
        }

        // ─── READ ALL ─────────────────────────────────────────────────────────────
        static async Task ReadAllTasksAsync()
        {
            Console.WriteLine("\n📋 Все задачи:\n");

            var query = new QueryDefinition("SELECT * FROM c ORDER BY c.createdDate DESC");
            using FeedIterator<TaskItem> iter =
                _container.GetItemQueryIterator<TaskItem>(query);

            int count = 0;
            while (iter.HasMoreResults)
            {
                FeedResponse<TaskItem> page = await iter.ReadNextAsync();
                foreach (var item in page)
                {
                    PrintTask(item);
                    count++;
                }
            }
            Console.WriteLine(count == 0 ? "  (нет задач)" : $"\nВсего: {count} задач(и)");
        }

        // ─── READ BY ID ───────────────────────────────────────────────────────────
        static async Task ReadTaskByIdAsync()
        {
            Console.Write("\nID задачи: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Дата создания (partition key, yyyy-MM-dd): ");
            string date = Console.ReadLine() ?? "";

            try
            {
                ItemResponse<TaskItem> response =
                    await _container.ReadItemAsync<TaskItem>(id, new PartitionKey(date));
                Console.WriteLine("\n✅ Задача найдена:");
                PrintTask(response.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("❌ Задача не найдена.");
            }
        }

        // ─── UPDATE ───────────────────────────────────────────────────────────────
        static async Task UpdateTaskAsync()
        {
            Console.Write("\nID задачи для обновления: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Дата создания (partition key, yyyy-MM-dd): ");
            string date = Console.ReadLine() ?? "";

            try
            {
                ItemResponse<TaskItem> existing =
                    await _container.ReadItemAsync<TaskItem>(id, new PartitionKey(date));
                TaskItem task = existing.Resource;

                Console.Write($"Новый заголовок [{task.Title}]: ");
                string newTitle = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(newTitle)) task.Title = newTitle;

                Console.Write($"Новое описание [{task.Description}]: ");
                string newDesc = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(newDesc)) task.Description = newDesc;

                ItemResponse<TaskItem> updated =
                    await _container.ReplaceItemAsync(task, task.Id, new PartitionKey(task.CreatedDate));

                Console.WriteLine("\n✅ Задача обновлена:");
                PrintTask(updated.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("❌ Задача не найдена.");
            }
        }

        // ─── DELETE ───────────────────────────────────────────────────────────────
        static async Task DeleteTaskAsync()
        {
            Console.Write("\nID задачи для удаления: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Дата создания (partition key, yyyy-MM-dd): ");
            string date = Console.ReadLine() ?? "";

            try
            {
                // Soft-delete: set DeletedDate
                ItemResponse<TaskItem> existing =
                    await _container.ReadItemAsync<TaskItem>(id, new PartitionKey(date));
                TaskItem task = existing.Resource;
                task.DeletedDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

                await _container.ReplaceItemAsync(task, task.Id, new PartitionKey(task.CreatedDate));
                Console.WriteLine($"\n✅ Задача помечена как удалённая (soft-delete).");
                Console.WriteLine($"   DeletedDate: {task.DeletedDate}");

                // Uncomment below for hard-delete:
                // await _container.DeleteItemAsync<TaskItem>(id, new PartitionKey(date));
                // Console.WriteLine("✅ Задача удалена.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("❌ Задача не найдена.");
            }
        }

        // ─── Helper ───────────────────────────────────────────────────────────────
        static void PrintTask(TaskItem t)
        {
            Console.WriteLine("┌─────────────────────────────────────────");
            Console.WriteLine($"│ ID          : {t.Id}");
            Console.WriteLine($"│ Заголовок   : {t.Title}");
            Console.WriteLine($"│ Описание    : {t.Description}");
            Console.WriteLine($"│ Дата созд.  : {t.CreatedDate}");
            Console.WriteLine($"│ Дата удал.  : {t.DeletedDate ?? "—"}");
            Console.WriteLine("└─────────────────────────────────────────");
        }
    }
}