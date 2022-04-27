using CVBuilder.Application.Expiriance.Respons;

namespace CVBuilder.Application.Expiriance.Queries
{
    public class GetExperiancByIdComand : MediatR.IRequest<GetExpirianceByIdResult>
    {
        public int Id { get; set; } 
    }
}
