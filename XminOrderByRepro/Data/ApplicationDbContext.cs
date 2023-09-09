using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading;
using XminOrderByRepro.Data;

namespace XminOrderByRepro.Data;

public class ApplicationDbContext : DbContext
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
        builder.Entity<Bar>();


        //Configuring with a string converter will result in a count of 4 entities materialized (unexpected).

        builder.Entity<Foo>().Property(p => p.JsonBlob).HasConversion<string>(to => "{}", from => new List<string>());

        //Ignoring/not configuring the JsonBlob property will result in a count of 3 entities materialized (expected).
        //builder.Entity<Foo>().Ignore(p => p.JsonBlob);

        base.OnModelCreating(builder);
    }
}



public class Foo
{

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public List<string> JsonBlob { get; set; } = new();
}
public class Bar
{
    private Bar()
    {

    }
    public Bar(Foo foo)
    {
        Foo = foo;
    }
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public Foo Foo { get; set; }

}


public class QueryClass
{
    private readonly ApplicationDbContext _dbContext;

    public QueryClass(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Query2()
    {
        await _dbContext.Set<Bar>().ExecuteDeleteAsync();
        await _dbContext.Set<Foo>().ExecuteDeleteAsync();

        var foo1 = new Foo();
        var foo2 = new Foo();
        var foo3 = new Foo();
        _dbContext.Set<Foo>().Add(foo1);
        _dbContext.Set<Foo>().Add(foo2);
        _dbContext.Set<Foo>().Add(foo3);

        _dbContext.Set<Bar>().Add(new Bar(foo1));
        _dbContext.Set<Bar>().Add(new Bar(foo1));
        _dbContext.Set<Bar>().Add(new Bar(foo2));
        _dbContext.Set<Bar>().Add(new Bar(foo3));
        await _dbContext.SaveChangesAsync();

        _dbContext.ChangeTracker.Clear();
        var query = (from foo in _dbContext.Set<Foo>()
                     join bar in _dbContext.Set<Bar>() on foo equals bar.Foo
                     group new
                     {
                         bar.Id,
                     } by foo);

        var qs = query.ToQueryString();
        var result = await query.ToListAsync();

        Debug.Assert(result.Count == 3); //3 Foo's returned if 'JsonBlob' is ignored by EF.
        Debug.Assert(result.Count == 4); //4 Foo's returned if string converter is configured for 'JsonBlob' by EF.
    }
}

