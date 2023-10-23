using Domain;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Articles.Service;

/// <summary>
/// Сервис для работы со статьями
/// </summary>
public interface IArticleService
{
    /// <summary>
    /// Создать базовую версию
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<ArticleResponse> CreateBaseVersion(CommandArticleCreateBaseVersion command);

    /// <summary>
    /// Создать дочернюю версию
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<ArticleResponse> CreateVersion(CommandArticleCreateVersion command);

    /// <summary>
    /// Получить актуальную на данный момент версию
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<ArticleResponse> GetActualVersion(QueryArticleGetActualVersion query);

    /// <summary>
    /// Получить все версии статьи
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<ArticleResponse>> GetVersions(QueryArticleGetVersions query);




}

