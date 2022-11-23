using ePets.Models;

namespace ePets.Repositories
{
    public interface IAnimalService
    {
        IEnumerable<Animal> GetAllAnimals();
        IEnumerable<Animal> GetAnimalsByType(AnimalType Type);
        IEnumerable<Animal> GetAnimalsByType(int TypeId);
        public void AddAnimal(Animal animal);
        public void DeleteAnimal(Animal animal);
        public void DeleteAnimal(int id);
        public void DeleteComment(Comment ChosenComment);
        public void UpdateAnimal(int id , Animal UpdatedAnimal);
        public IEnumerable<AnimalType> GetTypes();
        public AnimalType GetAnimalType(int id);
        public Animal GetSpecificAnimal(int id);

        public IEnumerable<Comment> GetCommentsOfAnimal(int id);
        public void SaveChanges();

    }
}
