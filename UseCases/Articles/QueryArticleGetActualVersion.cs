namespace UseCases.Articles;

/// <summary>
/// Получить актуальную версию статьи.
/// Становится актуальной при модерации
/// </summary>
/// <param name="Title">Заголовок статьи. MaximumLength(40)</param>
public record QueryArticleGetActualVersion(string Title)
{
    public class Validator : AbstractValidator<QueryArticleGetActualVersion>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(40);
        }
    }


}
