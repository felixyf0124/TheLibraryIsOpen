﻿namespace TheLibraryIsOpen.Models.DBModels
{
    public class Music
    {
        public int MusicId { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }

        // Default constructor
        public Music() {}

        /* Here the constructor assign values to attributes besides musicId.
         * The musicId is generated by database, when insert an entry to the "music" table (assumed it's already & primary key autoincrement).
         * The last id just entered from table would be assigned to musicId for the music object. So that to avoid same id appears when server gets restarted.
        */
        public Music(string artist, string album, string genre, int year, string isbn)
        {
            Artist = artist;
            Album = album;
            Genre = genre;
            Year = year;
            Isbn = isbn;
        }

        // another construcor who  assigns client id is added as requested.
        public Music(int mId, string artist, string album, string genre, int year, string isbn)
        {
            MusicId = mId;
        }

        // Return information about the music
        public override string ToString()
        {
            return "Music:\nArtist Name:" + Artist + "\nAlbum: " + Album + "\nGenre:" + Genre + "\nYear:" + Year + "\nISBN:" + Isbn + "\nMusic ID:" + MusicId;
        }
    }
}
