namespace BooksAPI.BE.Constants;

public static class AuthorConstants
{
    public static class AuthorRole
    {
        public const string Story = "Story";
        public const string Art = "Story";
        public const string StoryAndArt = "StoryAndArt";

        public static readonly IReadOnlyList<string> AuthorRoles = new[]
        {
            Story, Art, StoryAndArt
        };
    }
}