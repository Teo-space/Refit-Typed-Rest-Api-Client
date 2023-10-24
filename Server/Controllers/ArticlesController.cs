using ApiContracts;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Articles;
using UseCases.Articles.Service;

namespace Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ArticlesController(IArticleService articleService)
    : ControllerBase
{

    /// <summary>
    /// Создать базовую версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<ArticleResponse>> CreateBaseVersion([FromBody] CommandArticleCreateBaseVersion request)
        => await articleService.CreateBaseVersion(request);

    /// <summary>
    /// Создать дочернюю версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Version/")]
    public async Task<Result<ArticleResponse>> CreateVersion([FromBody] CommandArticleCreateVersion request)
        => await articleService.CreateVersion(request);

    /// <summary>
    /// end point для теста refit по умолчанию
    /// </summary>
    /// <returns></returns>
    [HttpGet("Default/")]
    public string Default() => "Default";


    /// <summary>
    ///  Получить актуальную на данный момент версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Result<ArticleResponse>> GetActualVersion([FromQuery] QueryArticleGetActualVersion request)
    => await articleService.GetActualVersion(request);

    /// <summary>
    /// Получить все версии статьи
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Version/")]
    public async Task<Result<IReadOnlyCollection<ArticleResponse>>> GetVersions([FromQuery] QueryArticleGetVersions request)
        => await articleService.GetVersions(request);


}

