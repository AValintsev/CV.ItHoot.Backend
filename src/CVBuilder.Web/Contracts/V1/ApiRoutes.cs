namespace CVBuilder.Web.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Identity
        {
            private const string BaseIdentity = Base + "/identity";

            public const string Login = BaseIdentity + "/login";
            public const string Register = BaseIdentity + "/register";
            public const string GetCurrentUserByToken = BaseIdentity + "/getcurrentuserbytoken";

            public const string Refresh = BaseIdentity + "/refresh";
            public const string Logout = BaseIdentity + "/logout";
            public const string Revoke = BaseIdentity + "/revoke";

            public const string GenerateToken = BaseIdentity + "/generateToken";
        }

        public static class Confirmation
        {
            private const string BaseConfirmation = Base + "/confirmation";

            public const string SendEmail = BaseConfirmation + "/sendEmail";
            public const string ConfirmEmail = BaseConfirmation + "/confirmEmail";
            public const string SendSms = BaseConfirmation + "/sendSms";
            public const string ConfirmPhone = BaseConfirmation + "/confirmPhone";
        }

        public static class Data
        {
            private const string BaseData = Base + "/data";
            private const string BaseDataType = BaseData + "/types";

            public const string LevelLanguage = BaseDataType + "/levelLanguages";
            public const string LevelSkill = BaseDataType + "/levelSkills";
        }

        public static class CV
        {
            private const string BaseCV = Base + "/cv";

            public const string CreateCV = BaseCV + "/create";
            public const string GetCV = BaseCV + "/id";
            //public const string GetAllCvInfo = BaseCV + "/index";
            public const string GetAllCvCards = BaseCV + "/list";
            public const string GetCvById = BaseCV;
            public const string UpdateCv = BaseCV + "/updateCv";

        } 
        public static class File
        {
            private const string BaseFile = Base + "/file";
            public const string CreatFile = BaseFile + "/create";
            public const string GetFileById = BaseFile + "/id";
            public const string GetAllFileUrl = BaseFile + "/list";
        }

        public static class SkillRoute
        {
            private const string BaseSkill = Base + "/skill";
            public const string CreateSkill = BaseSkill + "/create";
            public const string GetSkill = BaseSkill;
            public const string SkillsGetAll = BaseSkill + "/all";
        }

        public static class EducationRoute
        {
            private const string BaseEducation = Base + "/education";
            public const string CreateEducation = BaseEducation + "/create";
            public const string GetEducation = BaseEducation;
            public const string EducationGetAll = BaseEducation + "/all";
        }

        public static class Experience
        {
            private const string BaseExperience = Base + "/Experience";
            public const string CreateExperience = BaseExperience + "/create";
            public const string GetExperience = BaseExperience;
            public const string ExperienceGetAll = BaseExperience + "/all";
        }
        public static class Language
        {
            private const string BaseLanguage = Base + "/Language";
            public const string CreateLanguage = BaseLanguage + "/create";
            public const string GetLanguage = BaseLanguage;
            public const string LanguageGetAll = BaseLanguage + "/all";
        }
    }
}
