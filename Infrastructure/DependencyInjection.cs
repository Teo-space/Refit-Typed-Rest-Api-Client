using Infrastructure.EntityFrameworkCore;

public static class DependencyInjection__ForumsInfrastructure
{
    static void Configure(this IServiceCollection Services)
    {
        Services.AddLogging();
    }


    public static void AddInfrastructureUseSqlServer(this IServiceCollection Services, string ConnectionString)
    {
        Services.Configure();

        Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(ConnectionString);
        });

    }


    public static void AddInfrastructureUseSqlite(this IServiceCollection Services, string ConnectionString= $"FileName=AppDbContext.db")
	{
        Services.Configure();
        Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlite(ConnectionString);
		});
	}

	public static void AddInfrastructureUseInMemoryDatabase(this IServiceCollection Services, string DataBaseName)
	{
        Services.Configure();
        Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseInMemoryDatabase(DataBaseName);
		});
	}


    public static void AddInfrastructureUseMySql(this IServiceCollection Services, string ConnectionString)
	{
        Services.Configure();

        Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
		});
	}



}