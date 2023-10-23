namespace UseCases.Articles;

/// <summary>
/// Получить всю историю версиий
/// </summary>
/// <param name="Title">Заголовок статьи. MaximumLength(40)</param>
public record QueryArticleGetVersions(string Title)
{
    public class Validator : AbstractValidator<QueryArticleGetVersions>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(40);
        }
    }


}
