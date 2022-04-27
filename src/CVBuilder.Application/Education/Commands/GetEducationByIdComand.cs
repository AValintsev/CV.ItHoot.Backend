using CVBuilder.Application.Education.Response;

namespace CVBuilder.Application.Education.Comands
{
    public class GetEducationByIdComand : MediatR.IRequest<EducationByIdResult>
    {
        public int  Id { get; set; }
    }
}
