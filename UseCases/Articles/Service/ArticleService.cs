using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UseCases.Articles.Service;

internal class ArticleService(AppDbContext dbContext, ILogger<ArticleService> logger) : IArticleService
{

    public async Task<Result<ArticleResponse>> CreateBaseVersion(CommandArticleCreateBaseVersion command)
    {
        logger.LogTrace($"Executing {command.GetType().Name} Params {{@command}}", command);

        var baseVersionExists = await dbContext.Articles.FirstOrDefaultAsync(x => x.Title == command.Title);
        if (baseVersionExists is not null)
        {
            logger.LogWarning($"Executing {command.GetType().Name}. Conflict: Title: {{@Title}}", command.Title);
            return Result.Conflict<ArticleResponse>(command.Title);
        }
        var baseVersion = Article.CreateBaseVersion(command.Title, command.Description, command.Text);
        dbContext.Articles.Add(baseVersion);
        await dbContext.SaveChangesAsync();

        return baseVersion.Adapt<ArticleResponse>().Ok();
    }


    public async Task<Result<ArticleResponse>> CreateVersion(CommandArticleCreateVersion command)
    {
        logger.LogTrace($"Executing {command.GetType().Name} Params {{@command}}", command);

        var baseVersion = await dbContext.Articles.FirstOrDefaultAsync(x => x.Title == command.Title);
        if(baseVersion is null)
        {
            logger.LogWarning($"Executing {command.GetType().Name}. ParentNotFound: Title: {{@Title}}", command.Title);
            return Result.ParentNotFound<ArticleResponse>(command.Title);
        }
        var Version = baseVersion.CreateVersion(command.Text);
        dbContext.Articles.Add(Version);
        await dbContext.SaveChangesAsync();

        return Version.Adapt<ArticleResponse>().Ok();
    }

    
    public async Task<Result<ArticleResponse>> GetActualVersion(QueryArticleGetActualVersion query)
    {
        logger.LogTrace($"Executing {query.GetType().Name} Params {{@query}}", query);

        var ActualVersion = await dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Title == query.Title)
            .Where(x => x.ActualVersion)
            .FirstOrDefaultAsync();
        if(ActualVersion is null)
        {
            logger.LogWarning($"Executing {query.GetType().Name}. NotFound: Title: {{@Title}}", query.Title);
            return Result.NotFound<ArticleResponse>(query.Title);
        }

        return ActualVersion.Adapt<ArticleResponse>().Ok();
    }

    public async Task<Result<IReadOnlyCollection<ArticleResponse>>> GetVersions(QueryArticleGetVersions query)
    {
        logger.LogTrace($"Executing {query.GetType().Name} Params {{@query}}", query);

        var Articles = await dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Title == query.Title)
            .ProjectToType<ArticleResponse>()
            .ToListAsync();
        return (Articles as IReadOnlyCollection<ArticleResponse>).Ok();
    }






}
