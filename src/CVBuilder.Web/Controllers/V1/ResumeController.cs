using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Application.Resume.Services.Pagination;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Resume;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ImageResult>> UploadImage(int resumeId, IFormFile image)
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

            return Ok(result);
        }
        
        /// <summary>
        /// Create a resume photo
        /// </summary>
        [HttpPost(ApiRoutes.Resume.CreateImage)]
        public async Task<ActionResult<ImageResult>> UploadImage(IFormFile image)
        {
            await using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var command = new UploadResumeImageCommand
            {
                ResumeId = null,
                Data = memoryStream.ToArray(),
                FileName = image.FileName,
                FileType = image.ContentType
            };
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Create resume template
        /// </summary>        
        [HttpPost(ApiRoutes.Resume.CreateTemplate)]
        public async Task<IActionResult> CreateTemplate(string name)
        {
            var command = new CreateTemplateCommand
            {
                HtmlStream = Request.Body,
                TemplateName = name
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update resume template
        /// </summary>        
        [HttpPut(ApiRoutes.Resume.UpdateTemplate)]
        public async Task<IActionResult> UpdateTemplate(int id, string name)
        {
            var command = new UpdateTemplateCommand()
            {
                Id = id,
                TemplateName = name,
                HtmlStream = Request.Body
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// Get resume template by ID
        /// </summary>
        [AllowAnonymous] 
        [HttpGet(ApiRoutes.Resume.GetTemplateById)]
        public async Task<IActionResult> GetTemplateById(int id)
        {
            var command = new GetTemplateByIdQuery()
            {
                Id = id
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete resume template by ID
        /// </summary>        
        [HttpDelete(ApiRoutes.Resume.DeleteTemplateById)]
        public async Task<IActionResult> DeleteTemplateById(int id)
        {
            var command = new DeleteResumeTemplateCommand()
            {
                Id = id
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get list of Resume templates
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllTemplates)]
        public async Task<ActionResult<List<ResumeTemplateResult>>> GetAllResumeTemplates()
        {
            var command = new GetAllResumeTemplatesQuery();
            var result = await Mediator.Send(command);
            return Ok(result);
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
                UserId = LoggedUserId,
                UserRoles = LoggedUserRoles.ToList(),
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
            command.UserRoles = LoggedUserRoles.ToList();
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get list of Resume
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetAllResume)]
        public async Task<ActionResult<PagedResponse<IEnumerable<ResumeCardResponse>>>> GetAllResumeCard(
            [FromQuery] GetAllResumeCardRequest request)
        {
            var validFilter = new GetAllResumeCardRequest(request.Page, request.PageSize, request.Term, request.Positions, request.Skills)
            {
                Sort = request.Sort,
                Order = request.Order,
                IsArchive = request.IsArchive,
            };

            var command = Mapper.Map<GetAllResumeCardQueries>(validFilter);
            command.UserId = LoggedUserId!.Value;
            command.UserRoles = LoggedUserRoles;
            var response = await Mediator.Send(command);
            var list = Mapper.Map<List<ResumeCardResponse>>(response.Item2);
            
            
            var result = new PagedResponse<ResumeCardResponse>(list, validFilter.Page, validFilter.PageSize, response.Item1);
            return Ok(result);
        }
        
        
        /// <summary>
        /// Update salary rate of Resume
        /// </summary>
        [HttpPut(ApiRoutes.Resume.UpdateSalaryRate)]
        public async Task<ActionResult<IEnumerable<ResumeCardResponse>>> UpdateSalaryRate(int resumeId,decimal salaryRate)
        {
            var command = new UpdateSalaryRateResumeCommand()
            {
                ResumeId = resumeId,
                SalaryRate = salaryRate
            };

            var result = await Mediator.Send(command);

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
        /// Get Resume by ID
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetResumeHtmlById)]
        public async Task<ActionResult<ResumeResult>> GetResumeHtmlById(int id)
        {
            var command = new GetResumeHtmlByIdQuery()
            {
                ResumeId = id,
                UserId = LoggedUserId,
                UserRoles = LoggedUserRoles
            };
            var response = await Mediator.Send(command);

            return Ok(new {Html = response});
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

        /// <summary>
        /// Get a DOCX file
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetResumeDocx)]
        public async Task<ActionResult> ResumeDocx(int id)
        {
            var result = await Mediator.Send(new GetDocxByIdQueries(id));
            return File(result, "application/octet-stream", "resume.docx");
        }

        /// <summary>
        /// Update docx in resume template
        /// </summary>        
        [HttpPut(ApiRoutes.Resume.UpdateDocxInTemplate)]
        public async Task<IActionResult> UpdateDocxInTemplate(int id, IFormFile docx)
        {
            await using var memoryStream = new MemoryStream();
            await docx.CopyToAsync(memoryStream);
            var command = new UpdateDocxInTemplateCommand(id, memoryStream.ToArray());
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get DOCX template by template id
        /// </summary>
        [HttpGet(ApiRoutes.Resume.GetDocxTemplateById)]
        public async Task<ActionResult> GetDocxTemplateById(int id)
        {
            var result = await Mediator.Send(new GetDocxTemplateByIdQueries(id));
            return File(result, "application/octet-stream", "resume.docx");
        }
    }
}