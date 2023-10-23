using Domain;

namespace Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

public class EntityConfigurationArticle : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.HasKey(x => x.ArticleVersionId);
        builder.Property(x => x.ArticleVersionId).HasConversion(x => x.ToGuid(), x => new Ulid(x))
            //.ValueGeneratedOnAdd().UseIdentityColumn()//For Autoincrement long\int ids
            ;

        builder.HasIndex(x => x.ArticleId);
        builder.Property(x => x.ArticleId).HasConversion(x => x.ToGuid(), x => new Ulid(x));


        builder.HasIndex(x => x.Title);
        builder.Property(x => x.Title).HasMaxLength(40);
        

        builder.Property(x => x.Description).HasMaxLength(100);
        builder.Property(x => x.Text).HasMaxLength(10000);


    }



}
