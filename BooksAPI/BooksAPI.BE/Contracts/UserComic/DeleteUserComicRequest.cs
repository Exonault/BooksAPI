namespace BooksAPI.BE.Contracts.UserComic;

public class DeleteUserComicRequest
{
    public string UserId { get; set; }
    public Guid ComicId { get; set; }
}