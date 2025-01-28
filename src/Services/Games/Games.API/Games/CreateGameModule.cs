using Carter;
using Games.API.Models;
using Marten;

namespace Games.API.Games
{
    public class CreateGameModule : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/game", async (Game request, IDocumentSession session) =>
            {
                session.Store(request);
                await session.SaveChangesAsync();
                return Results.Created();
            })
            .WithName("CreateGame")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Game")
            .WithDescription("Create Game");
        }
    }
}
