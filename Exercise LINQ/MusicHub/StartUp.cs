namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here

            //string result=(ExportAlbumsInfo(context,9));
            //Console.WriteLine(result);

            string secondResult = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(secondResult);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var exportedAlbums = context.Albums.Where(a => a.ProducerId == producerId)
                .ToArray()
                .OrderByDescending(a=>a.Price)
                .Select(a => new
                {
                    a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                    a.Producer,
                    Songs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name,
                    }).OrderByDescending(s => s.SongName).ThenBy(s => s.Price).ToArray(),
                    AlbumPrice = a.Price.ToString("f2")
                }).ToArray();;

            StringBuilder sb = new StringBuilder();

            foreach(var album in exportedAlbums)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.Producer.Name}");
                sb.AppendLine($"-Songs:");

                int songId = 1;

                foreach(var song in album.Songs)
                {
                    sb.AppendLine($"---#{songId}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.Price}");
                    sb.AppendLine($"---Writer: {song.Writer}");
                    songId++;
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice}");
            }

            return sb.ToString().Trim();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songsInfo = context.Songs.ToArray()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    Name = s.Name,
                    Performers = s.SongPerformers
                    .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                    .OrderBy(p => p)
                    .ToArray(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                }).ToArray()
                  .OrderBy(s => s.Name).ThenBy(s=>s.WriterName);


            int songId = 1;
            StringBuilder sb = new StringBuilder();
            foreach(var song in songsInfo)
            {
                sb.AppendLine($"-Song #{songId}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                foreach(var performer in song.Performers)
                {
                    sb.AppendLine($"---Performer: {performer}");
                }
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");
                songId++;
            }

            return sb.ToString().Trim();

        }
    }
}
