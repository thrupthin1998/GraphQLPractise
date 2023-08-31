using GraphQLPractise.Data;
using GraphQLPractise.GraphQL;
using GraphQLPractise.GraphQL.MutationFilters;
using GraphQLPractise.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DatabaseConnection"), new MySqlServerVersion(new Version(8, 0, 11)))
);

builder.Services.AddGraphQLServer().
    AddQueryType<Query>().
    AddMutationType<Mutation>().
    AddErrorFilter<GraphQlErrorFilter>();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();

