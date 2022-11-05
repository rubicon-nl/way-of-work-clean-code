using Rubicon.Wow.CleanCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class CharacterBuilder
{
    private string[]? _movies;
    private int _id;
    private string? _name;    
    private string? _imageUrl;
    private string? _url;

    public static CharacterBuilder Create(int id, string name)
    {   
        CharacterBuilder bld = new CharacterBuilder();
        bld._id = id;
        bld._imageUrl = $"https://static.wikia.nocookie.net/disney/images/5/{id}/Screenshot_2016-09-12_at_8.13.42_PM.png";
        bld._name = name;
        bld._url = $"https://api.disneyapi.dev/characters/{id}";
        return bld;
    }

    public CharacterBuilder PlayedInMovies(params string[] movies)
    {
        _movies = movies;
        return this;
    }

    public DisneyCharacter Build()
    {
        var filmList = new EquatableHashSet<string>(_movies ?? new string[0]);
        var disneyChar = new DisneyCharacter(_name!, _imageUrl!, _url!) { Id = _id, Films = filmList };
        return disneyChar;
    }
}
