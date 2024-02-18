using Microsoft.Extensions.Localization;
using static System.Console;

public class Resources
{
    private readonly IStringLocalizer<Resources> localizer = null!;

    public Resources(IStringLocalizer<Resources> localizer)
    {
        this.localizer = localizer;
    }

    public string? GetEnterYourNamePrompt()
    {
        string resourceStringName = "EnterYourName";

        LocalizedString localizedString = localizer[resourceStringName];

        if (localizedString.ResourceNotFound)
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine(
                $"Error: resource string \"{resourceStringName}\" not found."
                    + Environment.NewLine
                    + $"Search path: {localizedString.SearchedLocation}"
            );
            ForegroundColor = previousColor;

            return $"{localizedString}: ";
        }

        return localizedString;
    }

    public string? GetEnterYourDobPrompt()
    {
        return localizer["EnterYourDob"];
    }

    public string? GetEnterYourSalaryPrompt()
    {
        return localizer["EnterYourSalary"];
    }

    public string? GetPersonDetails(string name, DateTime dob, int minutes, decimal salary)
    {
        return localizer["PersonDetails", name, dob, minutes, salary];
    }
}
