using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Resume;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1
{
    public class ResumeController : BaseAuthApiController
    {

        /// <summary>
        /// Upload a resume photo
        /// </summary>
        [HttpPost(ApiRoutes.Resume.UploadImage)]
        public async Task<ActionResult> UploadImage(int resumeId, IFormFile image)
        {
            await using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var command = new UploadResumeImageCommand
            {
                ResumeId = resumeId,
                Data = memoryStream.ToArray(),
                FileName = image.FileName,
                FileType = image.ContentType
            };
            var result = await Mediator.Send(command);
            
            return Ok();
        }

        /// <summary>
        /// Get a PDF file
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetResumePdf)]
        public async Task<ActionResult> ResumePdf(int id)
        {
            var command = new GetPdfByIdQueries
            {
                ResumeId = id,
                JwtToken = $"{Request.Headers["Authorization"]}".Replace("Bearer ","")
            };

            var result = await Mediator.Send(command);
            return File(result, "application/octet-stream", "resume.pdf");
        }

        /// <summary>
        /// Create a new Resume
        /// </summary>
        [HttpPost(ApiRoutes.Resume.CreateResume)]
        public async Task<ActionResult<ResumeResult>> CreateResume(CreateResumeRequest request)
        {
            var command = Mapper.Map<CreateResumeCommand>(request);
            command.UserId = LoggedUserId!.Value;
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get list of Resume
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllResume)]
        public async Task<ActionResult<IEnumerable<ResumeCardResponse>>> GetAllResumeCard(
            [FromQuery] GetAllResumeCardRequest request)
        {
            var command = Mapper.Map<GetAllResumeCardQueries>(request);
            command.UserId = LoggedUserId!.Value;
            command.UserRoles = LoggedUserRoles;
            var response = await Mediator.Send(command);
            var result = Mapper.Map<List<ResumeCardResponse>>(response);
            return Ok(result);
        }

        /// <summary>
        /// Get Resume by ID
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetResumeById)]
        public async Task<ActionResult<ResumeResult>> GetResumeById(int id)
        {
            var command = new GetResumeByIdQuery
            {
                Id = id,
                UserId = LoggedUserId,
                UserRoles = LoggedUserRoles
            };
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing Resume
        /// </summary>
        [HttpPut(ApiRoutes.Resume.UpdateResume)]
        public async Task<ActionResult<ResumeResult>> UpdateResume([FromBody] UpdateResumeRequest request)
        {
            var command = Mapper.Map<UpdateResumeCommand>(request);
            command.UserId = LoggedUserId!.Value;
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Deleting an existing Resume
        /// </summary>
        [HttpDelete(ApiRoutes.Resume.DeleteResume)]
        public async Task<ActionResult<ResumeResult>> DeleteResume(int id)
        {
            var command = new DeleteResumeCommand
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get list of Resume templates
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllResumeTemplates)]
        public async Task<ActionResult<List<ResumeTemplateResult>>> GetAllResumeTemplates()
        {
            var command = new GetAllResumeTemplatesQuery();
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get list of Resume templates by positions
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllResumeByPositions)]
        public async Task<ActionResult<List<ResumeCardResult>>> GetResumesByPositions(string positions)
        {
            
            var command = new GetResumesByPositionQuery()
            {
                Positions = positions.Split(',').ToList()
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        
        /// <summary>
        /// Get list of Resume templates by proposal build template
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllResumeByProposalBuild)]
        public async Task<ActionResult<List<ResumeCardResult>>> GetResumesByProposalBuild(int id)
        {
            
            var command = new GetResumesByProposalBuildQuery()
            {
                ProposalBuildId = id
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Recovering deleted Resume
        /// </summary>
        [HttpPut(ApiRoutes.Resume.RecoverResume)]
        public async Task<ActionResult<ResumeCardResponse>> RecoverResume(int id)
        {
            var command = new RecoverResumeCommand
            {
                Id = id
            };
            var response = await Mediator.Send(command);

            var result = Mapper.Map<ResumeCardResponse>(response);
            return Ok(result);
        }
    }
}