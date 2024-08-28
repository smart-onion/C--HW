
using System.Runtime.CompilerServices;

namespace HW1
{
    public static class TrainStoreManager
    {
        private static ApplicationContext db = null;

        public static void SetDatabase(ApplicationContext db)
        {
            TrainStoreManager.db = db;
        }

        public static void AddTrain(Train train)
        {
            db.Train.Add(train);
            db.SaveChanges();
        }

        public static Train? GetTrainById(int id)
        {
            return db.Train.FirstOrDefault(train => train.Id == id);
        }

        public static void RemoveTrainById(int id)
        {
            var train = db.Train.FirstOrDefault(t => t.Id == id);
            db.Train.Remove(train);
            db.SaveChanges();


        }

        public static void EditTrainById(int id, Train newTrain)
        {
            var train = db.Train.FirstOrDefault(t => t.Id == id);
            train.Copy(newTrain);
            db.SaveChanges();

        }
    }
}
