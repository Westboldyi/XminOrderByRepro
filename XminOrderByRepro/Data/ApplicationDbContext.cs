using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using XminOrderByRepro.Data;

namespace XminOrderByRepro.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Foo>().Property(p => p.Version).IsRowVersion();
        builder.Entity<Bar>();


        base.OnModelCreating(builder);
    }
}



public class Foo
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public uint Version { get; set; }
}


public class QueryClass
{
    private readonly ApplicationDbContext _dbContext;

    public QueryClass(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Query()
    {
        var query = (from foo in _dbContext.Set<Foo>()
                     join bar in _dbContext.Set<Bar>() on foo equals bar.Foo
                     group new
                     {
                         bar.Id,
                     } by foo);

        var qs = query.ToQueryString();

//generated sql:

// SELECT f."Id", f.xmin, t."Id"
// FROM "Foo" AS f
// INNER JOIN (
//     SELECT b."Id", f0."Id" AS "Id0"
//     FROM "Bar" AS b
//     INNER JOIN "Foo" AS f0 ON b."FooId" = f0."Id"
// ) AS t ON f."Id" = t."Id0"
// ORDER BY f."Id", f.xmin



    }
}

public class Bar
{
    public int Id { get; set; }

    public Foo Foo { get; set; }

}
