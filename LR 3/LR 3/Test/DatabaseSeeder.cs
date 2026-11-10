//using LR_3.DBCommunication.Documents;
//using LR_3.Models;
//using MongoDB.Driver;

//namespace LR_3.Test
//{
//    public class DatabaseSeeder
//    {
//        private readonly IMongoDatabase _db;

//        public DatabaseSeeder(IMongoDatabase db)
//        {
//            _db = db;
//        }

//        public async Task SeedTodosAsync()
//        {
//            var collection = _db.GetCollection<EventDocument>("events");

//            await collection.DeleteManyAsync(FilterDefinition<EventDocument>.Empty);

//            var items = new List<EventDocument>
//            {
//            new EventDocument
//            {
//                Title = "Музичний фестиваль 'Захід'",
//                Description = "Наймасштабніший літній фестиваль просто неба з виступами українських та закордонних гуртів.",
//                location = "Львівська область, Чарівна долина",
//                startDate = DateTime.UtcNow.AddDays(15),
//                status = EventStatus.Published
//            },
//            new EventDocument
//            {
//                Title = "IT-конференція TechFuture 2026",
//                Description = "Щорічна зустріч розробників, архітекторів та лідерів індустрії. Обговорення новітніх трендів .NET та штучного інтелекту.",
//                location = "Київ, КВЦ Парковий",
//                startDate = DateTime.UtcNow.AddMonths(1),
//                status = EventStatus.Published
//            },
//            new EventDocument
//            {
//                Title = "Вистава 'Тіні забутих предків'",
//                Description = "Сучасна театральна інтерпретація класичного твору Михайла Коцюбинського з неймовірними декораціями.",
//                location = "Хмельницький, Обласний драмтеатр",
//                startDate = DateTime.UtcNow.AddDays(7),
//                status = EventStatus.Published
//            }
//        };
//            await collection.InsertManyAsync(items);
//        }
//    }
//}
