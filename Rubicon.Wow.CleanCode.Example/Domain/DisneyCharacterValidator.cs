using FluentValidation;

namespace Rubicon.Wow.CleanCode.Example.Domain;

public class DisneyCharacterValidator : AbstractValidator<DisneyCharacter>
{
    public DisneyCharacterValidator()
    {
        RuleFor(c => c.name).NotEqual("Bamiebal");
    }
}
