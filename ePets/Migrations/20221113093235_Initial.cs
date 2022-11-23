using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ePets.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                });

            migrationBuilder.CreateTable(
                name: "AnimalTypes",
                columns: table => new
                {
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTypes", x => x.AnimalTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnimalTypes",
                columns: new[] { "AnimalTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Bird" },
                    { 2, "Fish" },
                    { 3, "Dog" },
                    { 4, "Reptile" }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Age", "AnimalTypeId", "Bio", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 3, 1, "The bald eagle is a bird of prey found in North America. A sea eagle, it has two known subspecies and forms a species pair with the white-tailed eagle , which occupies the same niche as the bald eagle in the Palearctic. Its range includes most of Canada and Alaska, all of the contiguous United States, and northern Mexico. It is found near large bodies of open water with an abundant food supply and old-growth trees for nesting.", "/images/Animals/BaldEagle.jpg", "Bald Eagle", 65.989999999999995 },
                    { 2, 5, 1, "The barn owl is nocturnal over most of its range; but in Great Britain and some Pacific Islands, it also hunts by day. Barn owls specialise in hunting animals on the ground and nearly all of their food consists of small mammals, which they locate by sound, their hearing being very acute.", "/images/Animals/BarnOwl.jpg", "Barn Owl", 27.989999999999998 },
                    { 3, 3, 1, "Macaws are a group of New World parrots that are long-tailed and often colorful. They are popular in aviculture or as companion parrots, although there are conservation concerns about several species in the wild.", "/images/Animals/MacawParrot.jpg", "Macaw Parrot", 24.75 },
                    { 4, 1, 2, "Clownfish or anemonefish are fishes from the subfamily Amphiprioninae in the family Pomacentridae. Thirty species of clownfish are recognized: one in the genus Premnas, while the remaining are in the genus Amphiprion.", "/images/Animals/ClownFish.jpg", "Clown Fish", 5.9900000000000002 },
                    { 5, 5, 2, "There are many varieties of ornamental koi, originating from breeding that began in Niigata, Japan in the early 19th century.Several varieties are recognized by the Japanese, distinguished by coloration, patterning, and scalation.", "/images/Animals/KoiFish.jpg", "Koi Fish", 7.9900000000000002 },
                    { 6, 3, 3, "The Belgian Shepherd (also known as the Belgian Sheepdog, Belgian Malinois, or the Chien de Berger Belge) is a breed of medium-sized herding dog from Belgium. While predominantly considered a single breed, it is bred in four distinct varieties based on coat type and colour; the long-haired black Groenendael, the rough-haired fawn Laekenois, the short-haired fawn Malinois, and the long-haired fawn Tervuren. ", "/images/Animals/BelgianShepherd.jpg", "Belgian Sheperd", 17.489999999999998 },
                    { 7, 4, 3, "The Golden Retriever is a Scottish breed of retriever dog of medium size. It is characterised by a gentle and affectionate nature and a striking golden coat. It is commonly kept as a pet and is among the most frequently registered breeds in several Western countries. It is a frequent competitor in dog shows and obedience trials; it is also used as a gundog, and may be trained for use as a guide dog.", "/images/Animals/GoldenRetriever.jpg", "Golden Retriever", 27.989999999999998 },
                    { 8, 5, 3, "Husky is a general term for a dog used in the polar regions, primarily and specifically for work as sled dogs. It refers to a traditional northern type, notable for its cold-weather tolerance and overall hardiness. Modern racing huskies that maintain arctic breed traits (also known as Alaskan huskies) represent an ever-changing crossbreed of the fastest dogs.", "/images/Animals/Husky.jpg", "Husky", 32.170000000000002 },
                    { 9, 12, 4, "The name bearded dragon refers to the underside of the throat (or beard) of the lizard, which can turn black and gain weight for a number of reasons, most often as a result of stress, or if they feel threatened", "/images/Animals/BeardedDragon.jpg", "Bearded Dragon", 13.99 },
                    { 10, 11, 4, "The king cobra (Ophiophagus hannah) is a species of venomous elapid snake endemic to jungles in Southern and Southeast Asia. The sole member of the genus Ophiophagus, it is distinguishable from other cobras, most noticeably by its size and neck patterns", "/images/Animals/CobraSnake.jpg", "Cobra", 19.989999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "AnimalId", "Content" },
                values: new object[,]
                {
                    { 1, 1, "Doed he really bald?" },
                    { 2, 1, "So Majestic" },
                    { 3, 2, "ho ho gang!" },
                    { 4, 3, "Wheres captain hook LOL!" },
                    { 5, 4, "found Nemo" },
                    { 6, 5, "So pretty..." },
                    { 7, 6, "best dog ever!" },
                    { 8, 7, "Does He Bite?" },
                    { 9, 8, "So Pretty<3" },
                    { 10, 8, "Can He Survive In The Desert?" },
                    { 11, 7, "lets go to the beach" },
                    { 12, 9, "Dracarys!" },
                    { 13, 9, "GOT > HOTD" },
                    { 14, 10, "Getting Slytherin Vibe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnimalId",
                table: "Comments",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTypes");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
