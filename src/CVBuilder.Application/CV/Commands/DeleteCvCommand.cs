using CVBuilder.Application.CV.Responses.CvResponse;
using MediatR;

namespace CVBuilder.Application.CV.Commands
{
    public class DeleteCvCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}