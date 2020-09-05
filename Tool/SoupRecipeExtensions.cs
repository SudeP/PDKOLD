using PDK.Tool;

public static class SoupRecipeExtensions
{
    public static string Salt;
    public static void SetSalt(string salt) => Salt = salt;
    public static string Stir(this string pureSoup)
    {
        SoupRecipe soupRecipe = new SoupRecipe();
        return soupRecipe.Stir(pureSoup, Salt);
    }
    public static string Stir(this string pureSoup, string salt)
    {
        SoupRecipe soupRecipe = new SoupRecipe();
        return soupRecipe.Stir(pureSoup, salt);
    }
    public static string Fractionate(this string impureSoup)
    {
        SoupRecipe soupRecipe = new SoupRecipe();
        return soupRecipe.Fractionate(impureSoup, Salt);
    }
    public static string Fractionate(this string impureSoup, string salt)
    {
        SoupRecipe soupRecipe = new SoupRecipe();
        return soupRecipe.Fractionate(impureSoup, salt);
    }
}