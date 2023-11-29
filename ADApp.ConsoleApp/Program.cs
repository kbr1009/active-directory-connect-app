using ADApp.Application.Accounts.Queries.ShowAllAccounts;
using ADApp.Application.OUs.Queries.ShowAllOUs;
using ADApp.ConsoleApp.Services;
using ADApp.Infrastructure.Accounts.Queries;
using ADApp.Infrastructure.ActiveDirectory;
using ADApp.Infrastructure.OUs.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// 参考：
// https://infog32.blog-sim.com/c-/c-%EF%BC%9Adirectorysearcher.properties


ConsoleApp.CreateBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.Configure<DirectoryEntryOptions>(option =>
        {
            option.Path = ctx.Configuration.GetConnectionString("lpath");
            option.User = ctx.Configuration.GetConnectionString("luser");
            option.Password = ctx.Configuration.GetConnectionString("lpassword");
        })
        .AddSingleton<ADSchemaContext>()
        .AddTransient<IShowAllAccountsQuery, ShowAllAccountsQuery>()
        .AddTransient<IShowAllOUsQuery, ShowAllOUsQuery>();
    })
    .Build()
    .AddCommands<ExportADUsersForExcel>()
    .Run();