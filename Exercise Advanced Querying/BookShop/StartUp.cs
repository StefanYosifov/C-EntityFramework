namespace BookShop
{
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //2
            //string ageRestriction = Console.ReadLine();
            //Console.WriteLine(GetBooksByAgeRestriction(db,ageRestriction));


            //3
            //string goldenBooks = GetGoldenBooks(db);
            //Console.WriteLine(goldenBooks);

            ////4
            //string booksWithPriceOver40 = GetBooksByPrice(db);
            //Console.WriteLine(booksWithPriceOver40);

            //5
            //int bookYear = int.Parse(Console.ReadLine());
            //Console.WriteLine(GetBooksNotReleasedIn(db,bookYear));

            //6
            //string category = Console.ReadLine();
            //Console.WriteLine(GetBooksByCategory(db, category));

            //7
            string bookDate = Console.ReadLine();
            Console.WriteLine(GetBooksReleasedBefore(db,bookDate));

        }


        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var allBookTitles = context.Books.ToArray()
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine,allBookTitles);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenEditionBooks = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType == Models.Enums.EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, goldenEditionBooks);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {

            //Would be better to do the sorting after the select
            var getBooksWithPriceOver40 = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b=>b.Price)
                .Select(b => new
                {
                    Book = $"{b.Title} - ${b.Price:f2}"
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach(var book in getBooksWithPriceOver40)
            {
                sb.AppendLine(book.Book);
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var allBooksNotRealeasedInTheGivenYear = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    Title=b.Title
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach(var book in allBooksNotRealeasedInTheGivenYear)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] inputAsArray = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(input=>input.ToLower())
                .ToArray();

                var allBooksByChosenCategory = context.Books
           .Where(b => b.BookCategories.Any(bc=>inputAsArray.Contains(bc.Category.Name.ToLower())))
           .OrderBy(b=>b.Title)
           .Select(b=> new 
           { 
              Title=b.Title,
           })
           .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach(var book in allBooksByChosenCategory)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateAsDateType = DateTime.Parse(date);

            var booksBeforeDate = context.Books
                .Where(b => b.ReleaseDate < dateAsDateType)
                .Select(b => new
                {
                    Book = $"{b.Title} - {b.EditionType} - ${b.Price:f2}",
                    ReleaseDate=b.ReleaseDate
                })
                .OrderByDescending(b=>b.ReleaseDate)
                .ToArray();


            StringBuilder sb = new StringBuilder();
            foreach(var book in booksBeforeDate)
            {
                sb.AppendLine(book.Book);
            }

            return sb.ToString().Trim();


        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {

            var namesEndingWith = context.Authors
               .Where(a => a.FirstName.EndsWith(input))
               .Select(a => new
               {
                   Name = $"{a.FirstName} {a.LastName}"
               })
               .ToArray();


            StringBuilder sb = new StringBuilder();
            foreach(var author in namesEndingWith)
            {
                sb.AppendLine(author.Name);
            }

            return sb.ToString().Trim();
        }

    }
}


