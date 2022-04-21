using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Data.Queries;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Responses.Data;
using CVBuilder.Web.Infrastructure.BaseControllers;
using MediatR;

namespace CVBuilder.Web.Controllers.V1
{
   
    [ApiController]
    public class DataController : BaseApiController
    {
        [HttpGet(ApiRoutes.Data.LevelLanguage)]
        public async Task<ActionResult<IEnumerable<DataTypeResult>>> GetAllLanguageLevels()
        {
            return Ok(await GetDataTypes<LanguageLevel>());
        }

        [HttpGet(ApiRoutes.Data.LevelSkill)]
        public async Task<ActionResult<IEnumerable<DataTypeResult>>> DriverStatuses()
        {
            return Ok(await GetDataTypes<SkillLevel>());
        }

        private async Task<IEnumerable<DataTypeResult>> GetDataTypes<TEnum>()
            where TEnum : struct, Enum
        {
            var query = new GetDataTypesQuery { EnumType = typeof(TEnum) };

            var response = await Mediator.Send(query);

            return response;
        }
    }
}
