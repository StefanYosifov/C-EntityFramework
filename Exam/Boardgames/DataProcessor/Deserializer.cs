namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using VaporStore.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            ImportCreatorDto[] creatorDtos = XmlSerialization.DeserializeXml<ImportCreatorDto>(xmlString, "Creators");
            ICollection<Creator> creators = new HashSet<Creator>();

            foreach(var creatorDto in creatorDtos)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if(string.IsNullOrWhiteSpace(creatorDto.FirstName) || string.IsNullOrWhiteSpace(creatorDto.LastName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator validCreator = new Creator()
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName
                };

                foreach(var boardDto in creatorDto.Boardgame)
                {
                    if (!IsValid(boardDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(boardDto.Name))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardDto.Name,
                        Rating = boardDto.Rating,
                        YearPublished = boardDto.YearPublished,
                        CategoryType = (CategoryType)(boardDto.CategoryType),
                        Mechanics = boardDto.Mechanics
                    };

                    validCreator.Boardgames.Add(boardgame);
                }
                creators.Add(validCreator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, validCreator.FirstName, validCreator.LastName, validCreator.Boardgames.Count));
            }

            context.Creators.AddRange(creators);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportSellerDto[] sellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);

            ICollection<Seller> sellers = new HashSet<Seller>();

            foreach(var sellerDto in sellerDtos)
            {
                if (!IsValid(sellerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = new Seller()
                {
                    Name = sellerDto.Name,
                    Address = sellerDto.Address,
                    Country = sellerDto.Country,
                    Website=sellerDto.Website,
                };

                foreach(var boardId in sellerDto.Boardgames.Distinct())
                {
                    var board = context.Boardgames.FirstOrDefault(b => b.Id == boardId);
                    if (board == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var boardGameSeller = new BoardgameSeller()
                    {
                        Boardgame = board
                    };

                    seller.BoardgamesSellers.Add(boardGameSeller);
                }
                sellers.Add(seller);
                sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count));

            }

            context.Sellers.AddRange(sellers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
