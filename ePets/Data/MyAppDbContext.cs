using ePets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ePets.Data
{
    public class MyAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }

        //orders related sets
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//entering data
        {
            base.OnModelCreating(modelBuilder);

            //animal type----------------------------------------------------------
            modelBuilder.Entity<AnimalType>().HasData(
                new { AnimalTypeId = 1, TypeName = "Bird" },
                new { AnimalTypeId = 2, TypeName = "Fish" },
                new { AnimalTypeId = 3, TypeName = "Dog" },
                new { AnimalTypeId = 4, TypeName = "Reptile" }
                );

            //animals-------------------------------------------------------------
            modelBuilder.Entity<Animal>().HasData(
                //birds--------------------------------------------------
                new
                {
                    AnimalId = 1,
                    Name = "Bald Eagle",
                    Age = 3,
                    ImageUrl = "/images/Animals/BaldEagle.jpg",
                    Price = 65.99,
                    AnimalTypeId = 1,
                    Bio = "The bald eagle is a bird of prey found in North America. A sea eagle, it has two known subspecies and forms a species pair with the white-tailed eagle , which occupies the same niche as the bald eagle in the Palearctic. Its range includes most of Canada and Alaska, all of the contiguous United States, and northern Mexico. It is found near large bodies of open water with an abundant food supply and old-growth trees for nesting."
                },
                new
                {
                    AnimalId = 2,
                    Name = "Barn Owl",
                    Age = 5,
                    ImageUrl = "/images/Animals/BarnOwl.jpg",
                    Price = 27.99,
                    AnimalTypeId = 1,
                    Bio = "The barn owl is nocturnal over most of its range; but in Great Britain and some Pacific Islands, it also hunts by day. Barn owls specialise in hunting animals on the ground and nearly all of their food consists of small mammals, which they locate by sound, their hearing being very acute."
                },
                new
                {
                    AnimalId = 3,
                    Name = "Macaw Parrot",
                    Age = 3,
                    ImageUrl = "/images/Animals/MacawParrot.jpg",
                    Price = 24.75,
                    AnimalTypeId = 1,
                    Bio = "Macaws are a group of New World parrots that are long-tailed and often colorful. They are popular in aviculture or as companion parrots, although there are conservation concerns about several species in the wild."
                },

                //fishes-------------------------------------------
                new
                {
                    AnimalId = 4,
                    Name = "Clown Fish",
                    Age = 1,
                    ImageUrl = "/images/Animals/ClownFish.jpg",
                    Price = 5.99,
                    AnimalTypeId = 2,
                    Bio = "Clownfish or anemonefish are fishes from the subfamily Amphiprioninae in the family Pomacentridae. Thirty species of clownfish are recognized: one in the genus Premnas, while the remaining are in the genus Amphiprion."
                },
                  new
                  {
                      AnimalId = 5,
                      Name = "Koi Fish",
                      Age = 5,
                      ImageUrl = "/images/Animals/KoiFish.jpg",
                      Price = 7.99,
                      AnimalTypeId = 2,
                      Bio = "There are many varieties of ornamental koi, originating from breeding that began in Niigata, Japan in the early 19th century.Several varieties are recognized by the Japanese, distinguished by coloration, patterning, and scalation."
                  },

                   //dogs------------------------------------------------------------
                   new
                   {
                       AnimalId = 6,
                       Name = "Belgian Sheperd",
                       Age = 3,
                       ImageUrl = "/images/Animals/BelgianShepherd.jpg",
                       Price = 17.49,
                       AnimalTypeId = 3,
                       Bio = "The Belgian Shepherd (also known as the Belgian Sheepdog, Belgian Malinois, or the Chien de Berger Belge) is a breed of medium-sized herding dog from Belgium. While predominantly considered a single breed, it is bred in four distinct varieties based on coat type and colour; the long-haired black Groenendael, the rough-haired fawn Laekenois, the short-haired fawn Malinois, and the long-haired fawn Tervuren. "
                   },
                    new
                    {
                        AnimalId = 7,
                        Name = "Golden Retriever",
                        Age = 4,
                        ImageUrl = "/images/Animals/GoldenRetriever.jpg",
                        Price = 27.99,
                        AnimalTypeId = 3,
                        Bio = "The Golden Retriever is a Scottish breed of retriever dog of medium size. It is characterised by a gentle and affectionate nature and a striking golden coat. It is commonly kept as a pet and is among the most frequently registered breeds in several Western countries. It is a frequent competitor in dog shows and obedience trials; it is also used as a gundog, and may be trained for use as a guide dog."
                    },
                     new
                     {
                         AnimalId = 8,
                         Name = "Husky",
                         Age = 5,
                         ImageUrl = "/images/Animals/Husky.jpg",
                         Price = 32.17,
                         AnimalTypeId = 3,
                         Bio = "Husky is a general term for a dog used in the polar regions, primarily and specifically for work as sled dogs. It refers to a traditional northern type, notable for its cold-weather tolerance and overall hardiness. Modern racing huskies that maintain arctic breed traits (also known as Alaskan huskies) represent an ever-changing crossbreed of the fastest dogs."
                     },

                     //reptiles----------------------------------------------------
                     new
                     {
                         AnimalId = 9,
                         Name = "Bearded Dragon",
                         Age = 12,
                         ImageUrl = "/images/Animals/BeardedDragon.jpg",
                         Price = 13.99,
                         AnimalTypeId = 4,
                         Bio = "The name bearded dragon refers to the underside of the throat (or beard) of the lizard, which can turn black and gain weight for a number of reasons, most often as a result of stress, or if they feel threatened"
                     },
                     new
                     {
                         AnimalId = 10,
                         Name = "Cobra",
                         Age = 11,
                         ImageUrl = "/images/Animals/CobraSnake.jpg",
                         Price = 19.99,
                         AnimalTypeId = 4,
                         Bio = "The king cobra (Ophiophagus hannah) is a species of venomous elapid snake endemic to jungles in Southern and Southeast Asia. The sole member of the genus Ophiophagus, it is distinguishable from other cobras, most noticeably by its size and neck patterns"
                  
                     });


            //comments-------------------------------------------------------------
            modelBuilder.Entity<Comment>().HasData(
             new { CommentId = 1, AnimalId = 1, Content = "Doed he really bald?" }, new { CommentId = 8, AnimalId = 7, Content = "Does He Bite?" },
             new { CommentId = 2, AnimalId = 1, Content = "So Majestic" }, new { CommentId = 9, AnimalId = 8, Content = "So Pretty<3"},
             new { CommentId = 3, AnimalId = 2, Content = "ho ho gang!" }, new { CommentId = 10, AnimalId = 8, Content = "Can He Survive In The Desert?"},
             new { CommentId = 4, AnimalId = 3, Content = "Wheres captain hook LOL!" }, new { CommentId = 11, AnimalId = 7, Content = "lets go to the beach" },
             new { CommentId = 5, AnimalId = 4, Content = "found Nemo"}, new { CommentId = 12, AnimalId = 9, Content = "Dracarys!" },
             new { CommentId = 6, AnimalId = 5, Content = "So pretty..."}, new { CommentId = 13, AnimalId = 9, Content = "GOT > HOTD" },
             new { CommentId = 7, AnimalId = 6 , Content = "best dog ever!"}, new { CommentId = 14, AnimalId = 10, Content = "Getting Slytherin Vibe" }
             );


 
        }
    }
}
