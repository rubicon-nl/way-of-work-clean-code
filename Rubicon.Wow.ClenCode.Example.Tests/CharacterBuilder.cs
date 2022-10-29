using Rubicon.Wow.CleanCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class CharacterBuilder
{
    private DisneyCharacter _disneyChar;

    private CharacterBuilder()
    {

    }

    public static CharacterBuilder Create(int id, string name)
    {        
        string imageUrl = $"https://static.wikia.nocookie.net/disney/images/5/{id}/Screenshot_2016-09-12_at_8.13.42_PM.png";
        string url = $"https://api.disneyapi.dev/characters/{id}";
        CharacterBuilder bld = new CharacterBuilder();
        bld._disneyChar = new DisneyCharacter(name, imageUrl, url) {  Id = id};
        return bld;
    }

    public CharacterBuilder PlayedInMovies(params string[] movies)
    {
        _disneyChar = _disneyChar with { Films = new EquatableHashSet<string>(movies)};
        return this;
    }

    public DisneyCharacter Build()
    {
        return _disneyChar;
    }
}
