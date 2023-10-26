# Refit Typed Rest Api Client

var articlesApi = RestService.For<IArticlesApi>("http://localhost:5266/api/Articles/");

var CreateBaseVersionResult = await articlesApi.CreateBaseVersion(new CommandArticleCreateBaseVersion("Title", "Description", "Text"));
Console.WriteLine($"CreateBaseVersionResult : {CreateBaseVersionResult.Success}");


var CreateVersionResult = await articlesApi.CreateVersion(new CommandArticleCreateVersion("Title", "Description", "Text"));
Console.WriteLine($"CreateVersionResult : {CreateVersionResult.Success}");

var ActualVersion = await articlesApi.GetActualVersion(new QueryArticleGetActualVersion("Title"));
Console.WriteLine($"ActualVersion : {ActualVersion.Success}");

var Versions = await articlesApi.GetVersions(new QueryArticleGetVersions("Title"));
Console.WriteLine($"{Versions.Success},     Count: {(Versions.Success ? Versions.Value.Count : 0)}");


public interface IArticlesApi
{

    [Get("/")]
    Task<Result<ArticleResponse>> GetActualVersion(QueryArticleGetActualVersion request);


    [Get("/Version")]//
    Task<Result<IReadOnlyCollection<ArticleResponse>>> GetVersions(QueryArticleGetVersions request);

    [Post("/")]
    Task<Result<ArticleResponse>> CreateBaseVersion(CommandArticleCreateBaseVersion request);

    [Post("/Version")]
    Task<Result<ArticleResponse>> CreateVersion(CommandArticleCreateVersion request);

}
