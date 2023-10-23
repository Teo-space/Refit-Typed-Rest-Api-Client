using Microsoft.EntityFrameworkCore;

namespace UseCases.Articles.Service;

internal class ArticleService(AppDbContext dbContext) : IArticleService
{

    public async Task<ArticleResponse> CreateBaseVersion(CommandArticleCreateBaseVersion command)
    {
        var baseVersion = Article.CreateBaseVersion(command.Title, command.Description, command.Text);
        dbContext.Articles.Add(baseVersion);
        await dbContext.SaveChangesAsync();

        return baseVersion.Adapt<ArticleResponse>();
    }


    public async Task<ArticleResponse> CreateVersion(CommandArticleCreateVersion command)
    {
        var baseVersion = await dbContext.Articles.FirstOrDefaultAsync(x => x.Title == command.Title);
        if(baseVersion == null)
        {
            //До переработки Result
        }
        var Version = baseVersion.CreateVersion(command.Text);
        dbContext.Articles.Add(Version);
        await dbContext.SaveChangesAsync();

        return Version.Adapt<ArticleResponse>();
    }

    
    public async Task<ArticleResponse> GetActualVersion(QueryArticleGetActualVersion query)
    {
        var ActualVersion = await dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Title == query.Title)
            .Where(x => x.ActualVersion)
            .FirstOrDefaultAsync();

        return ActualVersion.Adapt<ArticleResponse>();
    }

    public async Task<IReadOnlyCollection<ArticleResponse>> GetVersions(QueryArticleGetVersions query)
    {
        return await dbContext.Articles.AsNoTracking().Where(x => x.Title == query.Title).ProjectToType<ArticleResponse>().ToListAsync();
    }






}
