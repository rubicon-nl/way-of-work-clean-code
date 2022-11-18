﻿using Rubicon.Wow.CleanCode.Data;
using Xunit;

namespace Rubicon.Wow.CleanCode.Example.Tests.TestData;
internal class CharacterGeneratoTheoryData : TheoryData<DisneyCharacter, DisneyCharacter, DisneyCharacter>
{
    public CharacterGeneratoTheoryData()
    {
        var mickey = CharacterBuilder.Create(1, "Mickey Mouse")
          .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donald = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();
        var goofy = CharacterBuilder.Create(1, "Goofy")
            .PlayedInMovies("World of Illusion").Build();

        Add(mickey, donald, goofy);

        Add(donald, mickey, goofy);

        Add(goofy, mickey, donald);
    }

}




