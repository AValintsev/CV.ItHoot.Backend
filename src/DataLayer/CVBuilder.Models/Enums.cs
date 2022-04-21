using System;
using System.ComponentModel;

namespace CVBuilder.Models
{
    public class Enums
    {
        public enum RoleTypes
        {
            [Description("Admin")] Admin = 1,
            [Description("HR")] HR = 2,
            [Description("User")] User = 3
        }
    }

    public enum LanguageLevel
    {
        [Description("Basic")] Basic = 1,
        [Description("Intermediate")] Intermediate = 2,
        [Description("Advanced")] Advanced = 3,
        [Description("Fluent")] Fluent = 4
    }

    public enum SkillLevel
    {
        [Description("Basic")] Basic = 1,
        [Description("Intermediate")] Intermediate = 2,
        [Description("Advanced")] Advanced = 3
    }
}