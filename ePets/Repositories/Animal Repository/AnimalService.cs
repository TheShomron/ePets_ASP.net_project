using ePets.Data;
using ePets.Models;

namespace ePets.Repositories
{
    public class AnimalService : IAnimalService
    {
        private readonly MyAppDbContext _context;

        public AnimalService(MyAppDbContext context)
        {
            _context=context;
        }

        public void AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public void DeleteAnimal(Animal animal)
        {
            var ChosenAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
            _context.Animals.Remove(ChosenAnimal);
            _context.SaveChanges();
        }

        public void DeleteAnimal(int id)
        {
            var ChosenAnimal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            _context.Animals.Remove(ChosenAnimal);
            _context.SaveChanges();
        }

        public void DeleteComment(Comment ChosenComment)
        {
            _context.Comments.Remove(ChosenComment);
            _context.SaveChanges();
        }

        public  IEnumerable<Animal>  GetAllAnimals()
        {

            return _context.Animals;
        }

        public IEnumerable<Animal> GetAnimalsByType(AnimalType Type)
        {
            return _context.Animals.Where(an => an.AnimalTypeId == Type.AnimalTypeId);
        }

        public IEnumerable<Animal> GetAnimalsByType(int TypeId)
        {
            if (TypeId<1)
            {
                return GetAllAnimals();  
            }
            return _context.Animals.Where(an => an.AnimalTypeId == TypeId);
        }

        public AnimalType GetAnimalType(int id)
        {
                return _context.AnimalTypes.First(type => type.AnimalTypeId == id);
            
        }

        public IEnumerable<Comment> GetCommentsOfAnimal(int id)
        {
            return _context.Comments.Where(com => com.AnimalId == id);
        }

        public Animal GetSpecificAnimal(int id)
        {
            return _context.Animals.FirstOrDefault(a => a.AnimalId == id);
        }

        public IEnumerable<AnimalType> GetTypes()
        {
            return _context.AnimalTypes;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateAnimal(int id, Animal UpdatedAnimal)
        {
            var ChosenAnimal = _context.Animals.SingleOrDefault(an => an.AnimalId ==UpdatedAnimal.AnimalId) ;
            ChosenAnimal.Name = UpdatedAnimal.Name;
            ChosenAnimal.Age = UpdatedAnimal.Age;
            ChosenAnimal.Price = UpdatedAnimal.Price;
            ChosenAnimal.ImageUrl = UpdatedAnimal.ImageUrl;
            ChosenAnimal.Bio = UpdatedAnimal.Bio;
            ChosenAnimal.AnimalTypeId = UpdatedAnimal.AnimalTypeId;
            _context.SaveChanges();

        }
    }
}
