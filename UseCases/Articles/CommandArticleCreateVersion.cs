namespace UseCases.Articles;

/// <summary>
/// Создать новую версию для существующей статьи
/// </summary>
/// <param name="Title">Заголовок существующей статьи для которой создаем новую версию. MaximumLength(40)</param>
/// <param name="Description">Короткое описание. MaximumLength(100)</param>
/// <param name="Text">Текст. MaximumLength(1000)</param>
public record CommandArticleCreateVersion(string Title, string Description, string Text)
{
    public class Validator : AbstractValidator<CommandArticleCreateVersion>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Text).NotNull().NotEmpty().MaximumLength(1000);
        }
    }


}
