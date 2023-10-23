namespace UseCases.Articles;


/// <summary>
/// Команда для создания базовой версии статьи
/// </summary>
/// <param name="Title">Заголовок. MaximumLength(40)</param>
/// <param name="Description">Короткое описание. MaximumLength(100)</param>
/// <param name="Text">Текст. MaximumLength(1000)</param>
public record CommandArticleCreateBaseVersion(string Title, string Description, string Text)
{
    public class Validator : AbstractValidator<CommandArticleCreateBaseVersion>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Text).NotNull().NotEmpty().MaximumLength(1000);
        }
    }
}

