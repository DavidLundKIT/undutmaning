using BCrypt.Net;
using Xunit;

namespace CrackPassword;

public class CrackPasswordHarness
{
    // (316, 'pelle.patchberg', 'mail.cascada.ex', '$2b$12$5rudATAULHHOSLdwlDiUZOxop6szZW6tszHLPmQ91W700NnNrOrfq'),
    private readonly string expectedPasswordHash = "$2b$12$5rudATAULHHOSLdwlDiUZOxop6szZW6tszHLPmQ91W700NnNrOrfq"; // Example hash for "password"
    [Fact]
    public void SpecificPasswordCrack_OK()
    {
        string password = "Bulbasaur1997";
        //string passwordHash = BCrypt.HashPassword(password);
        var isValid = BCrypt.Net.BCrypt.Verify(password, expectedPasswordHash);
        Assert.True(isValid);
    }

    [Fact]
    public void BruteForcePasswordCrack()
    {
        var pokemonList = ReadLinesFromFile("PokemonGen1.txt");
        //pokemonList.Sort();
        var years = Enumerable.Range(1990, 37).Select(y => y.ToString()).ToList(); // Example range of years
        foreach (var pokemon in pokemonList)
        {
            foreach (var year in years)
            {
                string password = $"{pokemon}{year}";
                var isValid = BCrypt.Net.BCrypt.Verify(password, expectedPasswordHash);
                Assert.False(isValid, $"Password found: {password}"); // Fails when Password found
            }
        }
        Assert.Fail("Not gen 1 Pokemon");
    }

    public string[] ReadLinesFromFile(string filename)
    {
        string baseDir = AppContext.BaseDirectory;
        string filePath = $"{baseDir}\\Data\\{filename}";
        string[] orbits = File.ReadAllLines(filePath);
        return orbits;
    }
}
