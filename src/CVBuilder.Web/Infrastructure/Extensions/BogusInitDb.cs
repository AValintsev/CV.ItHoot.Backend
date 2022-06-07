﻿using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.EFContext;
using CVBuilder.Models;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class BogusInitDb
{
    public static async Task Init(EFDbContext context, IAppUserManager userManager, IShortUrlService shortUrlService)
    {
        var users = context.Users.Where(x => x.ShortUrlId == null);
        foreach (var user in users)
        {
            user.ShortUrl = new ShortUrl
            {
                Url = shortUrlService.GenerateShortUrl()
            };
        }

        var resumes = context.TeamResumes.Where(x => x.ShortUrlId == null);
        foreach (var resume in resumes)
        {
            resume.ShortUrl = new ShortUrl
            {
                Url = shortUrlService.GenerateShortUrl()
            };
        }

        context.UpdateRange(users);
        context.UpdateRange(resumes);
        await context.SaveChangesAsync();

        // if(await context.Users.AnyAsync())
        //     return;
        //
        // var testUsers = new Faker<User>("en")
        //     .RuleFor(x=>x.Email,y=>y.Person.Email)
        //     .RuleFor(x=>x.UserName,y=>y.Person.Email)
        //     .RuleFor(x => x.FirstName, y => y.Person.FirstName)
        //     .RuleFor(x => x.LastName, y => y.Person.LastName)
        //     .RuleFor(x => x.CreatedAt, y => DateTime.UtcNow)
        //     .RuleFor(x => x.UpdatedAt, y => DateTime.UtcNow)
        //     .RuleFor(x=>x.ShortUrl, y=> new ShortUrl(){Url = shortUrlService.GenerateShortUrl()});
        // var users = testUsers.Generate(80);
        //
        // foreach (var user in users.Take(20))
        // {
        //     var createdUser = await userManager.CreateAsync(user, "123456");
        //     var addRole = await userManager.AddToRolesAsync(user, new List<string>()
        //     {
        //         Enums.RoleTypes.User.ToString(),
        //     });
        // }
        //
        // foreach (var user in users.Skip(20).Take(20))
        // {
        //     var createdUser = await userManager.CreateAsync(user, "123456");
        //     var addRole = await userManager.AddToRolesAsync(user, new List<string>()
        //     {
        //         Enums.RoleTypes.Admin.ToString(),
        //     });
        // }
        //
        // foreach (var user in users.Skip(40).Take(20))
        // {
        //     var createdUser = await userManager.CreateAsync(user, "123456");
        //     var addRole = await userManager.AddToRolesAsync(user, new List<string>()
        //     {
        //         Enums.RoleTypes.HR.ToString(),
        //     });
        // }
        //
        // foreach (var user in users.Skip(60).Take(20))
        // {
        //     var createdUser = await userManager.CreateAsync(user, "123456");
        //     var addRole = await userManager.AddToRolesAsync(user, new List<string>()
        //     {
        //         Enums.RoleTypes.Client.ToString(),
        //     });
        // }
    }
}