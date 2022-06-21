using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Data.Queries;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;

namespace CVBuilder.Web.Controllers.V1
{
    public class DataController : BaseApiController
    {
        /// <summary>
        /// Get list of LevelLanguage
        /// </summary>
        [HttpGet(ApiRoutes.Data.LevelLanguage)]
        public async Task<ActionResult<IEnumerable<DataTypeResult>>> GetAllLanguageLevels()
        {
            return Ok(await GetDataTypes<LanguageLevel>());
        }

        /// <summary>
        /// Get list of LevelSkills
        /// </summary>
        [HttpGet(ApiRoutes.Data.LevelSkill)]
        public async Task<ActionResult<IEnumerable<DataTypeResult>>> DriverStatuses()
        {
            return Ok(await GetDataTypes<SkillLevel>());
        }


        private async Task<IEnumerable<DataTypeResult>> GetDataTypes<TEnum>()
            where TEnum : struct, Enum
        {
            var query = new GetDataTypesQuery {EnumType = typeof(TEnum)};

            var response = await Mediator.Send(query);

            return response;
        }
    }
}