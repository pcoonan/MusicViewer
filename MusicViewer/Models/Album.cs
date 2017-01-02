namespace MusicViewer.Models
{
    public class Album
    {
        public Album()
        {
            Artist = new Artist();
        }
        public Album(int albumId) : base()
        {
            Id = albumId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Artist Artist { get; set; }
    }
}