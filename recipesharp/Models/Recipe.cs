namespace recipesharp.Models;

public class Recipe
{
    public int Id { get; set; } 
    public string  Title { get; set; }  
    public string Instructions { get; set; }  
    public string ImgUrl { get; set; }  
    public string Category { get; set; }  
    public string CreatorId { get; set; } 
    public Account Creator { get; set; }
}

public class RecipeLiker : Recipe
{
    public int RecipeMemberId { get; set; } 
}
