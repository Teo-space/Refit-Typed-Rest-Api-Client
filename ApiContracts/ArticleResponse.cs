using NUlid;

namespace ApiContracts;


/// <summary>
/// Статья
/// </summary>
public sealed record ArticleResponse
{    
    /// <summary>
    /// Идентификатор статьи. Общий для всех версий одной статьи
    /// </summary>
    public string ArticleId { get; init; }
    /// <summary>
    /// Идентификатор версии статьи
    /// </summary>
    public string ArticleVersionId { get; init; }

    /// <summary>
    /// Признак актуальной версии. 
    /// Устанавливается при модерации.
    /// </summary>
    public bool ActualVersion { get; init; }


    /// <summary>
    /// Заголовок. Max Length(40)
    /// </summary>
    public string Title { get; init; }
    /// <summary>
    /// Короткое описание.  Max Length (100)
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Основной текст статьи. Max Lenth(1000)
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Дата создания версии
    /// </summary>
    public DateTime CreatedAt { get; init; }


}


