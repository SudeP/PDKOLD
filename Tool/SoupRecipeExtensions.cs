using PDK.Tool;

public static class SoupRecipeExtensions
{
    public static SoupRecipe soupRecipe = new SoupRecipe();
    public static string Salt;
    public static void SetSalt(string salt) => Salt = salt;
    public static string Stir(this string pureSoup) => pureSoup.Stir(Salt);
    public static string Stir(this string pureSoup, string salt) => soupRecipe.Stir(pureSoup, salt);
    public static string Fractionate(this string impureSoup) => impureSoup.Fractionate(Salt);
    public static string Fractionate(this string impureSoup, string salt) => soupRecipe.Fractionate(impureSoup, salt);
}