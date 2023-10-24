using Microsoft.EntityFrameworkCore;

namespace UseCases.Articles.Service;

internal class ArticleService(AppDbContext dbContext) : IArticleService
{

    public async Task<Result<ArticleResponse>> CreateBaseVersion(CommandArticleCreateBaseVersion command)
    {
        var baseVersionExists = await dbContext.Articles.FirstOrDefaultAsync(x => x.Title == command.Title);
        if (baseVersionExists is not null)
        {
            return Result.Conflict<ArticleResponse>(command.Title);
        }
        var baseVersion = Article.CreateBaseVersion(command.Title, command.Description, command.Text);
        dbContext.Articles.Add(baseVersion);
        await dbContext.SaveChangesAsync();

        return baseVersion.Adapt<ArticleResponse>().Ok();
    }


    public async Task<Result<ArticleResponse>> CreateVersion(CommandArticleCreateVersion command)
    {
        var baseVersion = await dbContext.Articles.FirstOrDefaultAsync(x => x.Title == command.Title);
        if(baseVersion is null)
        {
            return Result.ParentNotFound<ArticleResponse>(command.Title);
        }
        var Version = baseVersion.CreateVersion(command.Text);
        dbContext.Articles.Add(Version);
        await dbContext.SaveChangesAsync();

        return Version.Adapt<ArticleResponse>().Ok();
    }

    
    public async Task<Result<ArticleResponse>> GetActualVersion(QueryArticleGetActualVersion query)
    {
        var ActualVersion = await dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Title == query.Title)
            .Where(x => x.ActualVersion)
            .FirstOrDefaultAsync();

        return ActualVersion.Adapt<ArticleResponse>().Ok();
    }

    public async Task<Result<IReadOnlyCollection<ArticleResponse>>> GetVersions(QueryArticleGetVersions query)
    {
        var Articles = await dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Title == query.Title)
            .ProjectToType<ArticleResponse>()
            .ToListAsync();
        return (Articles as IReadOnlyCollection<ArticleResponse>).Ok();
    }






}
