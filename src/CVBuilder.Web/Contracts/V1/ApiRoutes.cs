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
            private const string BaseCv = Base + "/cv";
            public const string CreateCv = BaseCv;
            public const string GetAllCv = BaseCv ;
            public const string GetCvById = BaseCv+"/{id}";
            public const string UpdateCv = BaseCv;

        } 
        public static class File
        {
            private const string BaseFile = Base + "/files";
            public const string CreatFile = BaseFile;
            public const string GetFileById = BaseFile + "/{id}";
            public const string GetAllFileUrl = BaseFile + "/list";
        }

        public static class SkillRoute
        {
            private const string BaseSkill = Base + "/skills";
            public const string CreateSkill = BaseSkill;
            public const string GetSkill = BaseSkill+"/search";
            public const string SkillsGetAll = BaseSkill + "/all";
        }

        public static class EducationRoute
        {
            private const string BaseEducation = Base + "/educations";
            public const string CreateEducation = BaseEducation;
            public const string GetAllEducation = BaseEducation ;
            public const string GetEducation = BaseEducation+"/{id}";
        }

        public static class Experience
        {
            private const string BaseExperience = Base + "/experiences";
            public const string CreateExperience = BaseExperience;
            public const string GetAllExperience = BaseExperience;
            public const string GetExperienceById = BaseExperience+"/{id}";
        }
        public static class Language
        {
            private const string BaseLanguage = Base + "/languages";
            public const string CreateLanguage = BaseLanguage;
            public const string GetLanguage = BaseLanguage+"/search";
            public const string LanguageGetAll = BaseLanguage;
        }
    }
}
