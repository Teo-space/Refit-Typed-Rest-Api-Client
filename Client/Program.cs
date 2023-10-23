using ApiContracts;
using Refit;
using UseCases.Articles;

Console.WriteLine("Press any key to start");
Console.ReadLine();

//!!!Выставите правильный путь к сервису!!!
var articlesApi = RestService.For<IArticlesApi>("http://localhost:5266/api/Articles/");
//Для теста
var defaultEndpoint = await articlesApi.Default();
Console.WriteLine(defaultEndpoint);


var CreateBaseVersionResult = await articlesApi.CreateBaseVersion(new CommandArticleCreateBaseVersion("Title", "Description", "Text"));
Console.WriteLine(CreateBaseVersionResult);


var CreateVersionResult = await articlesApi.CreateVersion(new CommandArticleCreateVersion("Title", "Description", "Text"));
Console.WriteLine(CreateVersionResult);

var ActualVersion = await articlesApi.GetActualVersion(new QueryArticleGetActualVersion("Title"));
Console.WriteLine(ActualVersion);

var versions = await articlesApi.GetVersions(new QueryArticleGetVersions("Title"));
Console.WriteLine($"{versions},     Count: {versions.Count}");




public interface IArticlesApi
{
    [Get("/Default")]//Для теста 
    Task<string> Default();

    /// <summary>
    /// Получить актуальную версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Get("/")]
    Task<ArticleResponse> GetActualVersion(QueryArticleGetActualVersion request);

    /// <summary>
    /// Получить историю версий
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Get("/Version")]//
    Task<IReadOnlyCollection<ArticleResponse>> GetVersions(QueryArticleGetVersions request);

    /// <summary>
    /// Создать базовую версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Post("/")]
    Task<ArticleResponse> CreateBaseVersion(CommandArticleCreateBaseVersion request);

    /// <summary>
    /// Создать дочернюю версию
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Post("/Version")]
    Task<ArticleResponse> CreateVersion(CommandArticleCreateVersion request);







}


