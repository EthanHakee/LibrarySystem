using System;
using System.Collections.Generic;

namespace LibrarySystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Type Type { get; set; }
        public Genre Genre { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }

        public Item(long id, string type, string genre, string title, string author, long isAvailable)
        {
            Id = Convert.ToInt32(id);

            if (!Enum.TryParse(type, out Type TypeResult)) Type = Type.Unknown;
            else Type = TypeResult;

            if (!Enum.TryParse(genre, out Genre GenreResult)) Genre = Genre.Unknown;
            else Genre = GenreResult;

            Title = title;
            Author = author;

            if (isAvailable == 1) IsAvailable = true;
            else IsAvailable = false;
        }

        public Item(Type type, Genre genre, string title, string author, long isAvailable)
        {
            Type = type;
            Genre = genre;
            Title = title;
            Author = author;
            if (isAvailable == 1) IsAvailable = true;
            else IsAvailable = false;
        }

        public static List<string> GetShortDetails(List<Item> items)
        {
            List<string> result = new List<string>();
            foreach (Item item in items)
            {
                result.Add($"{item.Title} : {item.Type} : {item.Author}");
            }

            return result;
        }

        public static List<string> GetAllDetails(Item item)
        {
            List<string> result = new List<string>()
            {
                item.Title,
                item.Author,
                item.Genre.ToString(),
                item.Type.ToString(),
                item.IsAvailable.ToString()
            };

            return result;
        }
    }

    public enum Type
    {
        Unknown,
        Book,
        DVD,
        CD,
        Audiobook,
        VideoGame,
        eBook,
        MusicCD,
        Magazine,
        BluRayDisc,
        BoardGame,
        VHSTape,
        SheetMusic,
        Map,
        ScientificJournal
    }

    public enum Genre
    {
        Unknown,
        Action,
        Adventure,
        Comedy,
        Crime,
        Drama,
        Fantasy,
        Horror,
        Mystery,
        Romance,
        ScienceFiction,
        Thriller,
        Western,
        Childrens,
        Historical,
        Poetry
    }
}
