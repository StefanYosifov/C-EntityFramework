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
            //string bookDate = Console.ReadLine();
            //Console.WriteLine(GetBooksReleasedBefore(db,bookDate));


            //8
            //string booksEndingWith=Console.ReadLine();
            //Console.WriteLine(GetAuthorNamesEndingIn(db,booksEndingWith));

            //9
            //string booksContaining = Console.ReadLine();
            //Console.WriteLine(GetBookTitlesContaining(db,booksContaining));

            //10
            //string authorsWithLastNameStartingWith = Console.ReadLine();
            //Console.WriteLine(GetBooksByAuthor(db,authorsWithLastNameStartingWith));

            //11
            //int bookWithMoreLettersThan = int.Parse(Console.ReadLine());
            //Console.WriteLine(CountBooks(db,bookWithMoreLettersThan));

            //12
            //Console.WriteLine(CountCopiesByAuthor(db));

            //13
            //Console.WriteLine(GetTotalProfitByCategory(db));

            //14
            //Console.WriteLine(GetMostRecentBooks(db));

            //15
            //IncreasePrices(db);

            int deletedBookCount=RemoveBooks(db);
            Console.WriteLine(deletedBookCount);

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
            foreach(var author in namesEndingWith.OrderBy(a=>a.Name))
            {
                sb.AppendLine(author.Name);
            }

            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var booksContainingInput = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new
                {
                    Title = b.Title,
                })
                .OrderBy(b => b.Title);


            StringBuilder sb = new StringBuilder();
            foreach(var book in booksContainingInput)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();

        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksWithAuthorsLastNameStartingWith = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    BookTitle = b.Title,
                    Author = $"{b.Author.FirstName} {b.Author.LastName}",

                })
                .ToArray();


            StringBuilder sb = new StringBuilder();
            foreach(var book in booksWithAuthorsLastNameStartingWith)
            {
                sb.AppendLine($"{book.BookTitle} ({book.Author})");
            }

            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksWithMoreLettersThanInput = context.Books
                .Select(b=>b.Title)
                .Where(b => b.Length > lengthCheck).Count();

            return booksWithMoreLettersThanInput;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorCopies = context.Authors
                .Select(a => new
                {
                    Author = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books
                        .Select(b => b.Copies)
                        .Sum()
                })
                .OrderByDescending(ac=>ac.Copies)
                .ToArray();


            StringBuilder sb = new StringBuilder();
            foreach(var copies in authorCopies)
            {
                    sb.AppendLine($"{copies.Author} - {copies.Copies}");
               
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {

            var booksProfitByCategory = context.Categories
                .Select(c => new
                {
                    TotalPrice = $"{c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price):f2}",
                    Category = c.Name,
                })
                .ToArray()
                .OrderByDescending(c => c.TotalPrice)
                .ThenBy(c => c.Category);


            StringBuilder sb = new StringBuilder();
            foreach(var book in booksProfitByCategory)
            {
                sb.AppendLine($"{book.Category} ${book.TotalPrice}");
            }

            return sb.ToString().Trim();
        }


        public static string GetMostRecentBooks(BookShopContext context)
        {

            var topThreeMostRecentBooks = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Books = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        Title = cb.Book.Title,
                        ReleaseDate = cb.Book.ReleaseDate.Value.Year,
                    }).ToArray()
                }).ToArray();
              

            StringBuilder sb = new StringBuilder();
            foreach(var category in topThreeMostRecentBooks)
            {
                sb.AppendLine($"--{category.CategoryName}");
                foreach(var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate})");
                }
            }


            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(b=>b.ReleaseDate.Value.Year<2010);

            foreach(var book in books)
            {
                book.Price += 5;
            }

            //maybe put it inside the loop
            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToDelete = context.Books.Where(b => b.Copies < 4200).ToArray();
            int booksDeleteCount = 0;
            foreach(var book in booksToDelete)
            {
                context.Remove(book);
                booksDeleteCount++;
                context.SaveChanges();
            }

            return booksDeleteCount;
        }


    }                               
}                                   
                                    
                                    
                                    