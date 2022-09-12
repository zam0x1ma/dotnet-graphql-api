using commanderGQL.GraphQL;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPooledDbContextFactory<AppDbContext>(opt =>
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("CommandConStr")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.UseGraphQLVoyager(
    new VoyagerOptions
    {
        GraphQLEndPoint = "/graphql"
    },
    path: "/graphql-voyager");

app.Run();
