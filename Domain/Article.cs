using NUlid;

namespace Domain;

/// <summary>
/// Статья
/// </summary>
public sealed class Article
{
    /// <summary>
    /// Идентификатор статьи. Общий для всех версий одной статьи
    /// </summary>
    public Ulid ArticleId { get; set; }

    /// <summary>
    /// Идентификатор версии статьи
    /// </summary>
    public Ulid ArticleVersionId { get; set; }

    /// <summary>
    /// Признак актуальной версии. 
    /// Устанавливается при модерации.
    /// </summary>
    public bool ActualVersion { get; set; }


    /// <summary>
    /// Заголовок. Max Length(40)
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Короткое описание.  Max Length (100)
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Основной текст статьи. Max Lenth(1000)
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Дата создания версии
    /// </summary>
    public DateTime CreatedAt { get; set; }



    public static Article CreateBaseVersion(string Title, string Description, string Text)
    {
        Article article = new Article();
        article.ArticleId = Ulid.NewUlid();
        article.ArticleVersionId = article.ArticleId;

        article.Title = Title;
        article.Description = Description;
        article.Text = Text;
        article.CreatedAt = DateTime.Now;
        //До разработки функционала модерации актуальная версия помечается автоматически
        article.ActualVersion = true;


        return article;
    }


    public Article CreateVersion(string Text)
    {
        Article article = new Article();

        article.ArticleId = this.ArticleId;
        article.ArticleVersionId = Ulid.NewUlid();

        article.Title = Title;
        article.Description = Description;
        article.Text = Text;
        article.CreatedAt = DateTime.Now;
        //До разработки функционала модерации актуальная версия замещается новой автоматически
        article.ActualVersion = true;

        return article;
    } 


}


