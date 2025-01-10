namespace ÜNY.BackofficeUI.Utils
{
    public class DefaultClientEndpoint
    {
        public struct Authentice
        {
            public const string Login = "/api/Account/login";

            public const string Register = "/api/Account/register";
            public const string Logout = "/api/Account/logout";
            public const string GetUserRoles = "/api/Account/getUserRoles";
            public const string GetUnitList = "/api/Account/getUnitlist";
            public const string GetGenderList = "/api/Account/getGenderlist";
            
        }

        public struct Users
        {
            public const string List = "/api/Users/list";
            
        }
    }
}
