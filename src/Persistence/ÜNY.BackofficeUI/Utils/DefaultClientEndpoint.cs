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

            public const string GetAllUsers = "/api/Users/GetAllUsers";

            public const string BaseGetStudentsForCourse = "/api/Users/GetEnrolledStudents";


        }

        public struct Contact
        {
            public const string ContactAdd = "/api/Contactİnformation/add";
            public const string ContactUpdate = "/api/Contactİnformation/update";

        }

        public struct AdminAprove
        {
            public const string list = "api/AdminPendingUser/Pendingusers";
            public const string aprove = "api/AdminPendingUser/Approveuser";
        }

        public struct AdminCoursesUnit
        {
            public const string add = "api/CoursesUnitİnformation/add";
        }

        public struct Enrolment
        {
            public const string list = "api/CoursesUnitİnformation/GetUserUnitCourses";

            public const string add = "api/Enrolment/EnrollCourses";
        }

        public struct AdminAproveEnrolments 
        {
            public const string GetPendingEnrollments = "api/AdminEnrollAprove/GetPendingEnrollments";
            public const string aprove = "api/AdminEnrollAprove/ApproveEnrollment";
        }

        public struct AdminFeeİnfo
        {
            public const string FeeİnfoCreate = "/api/AdminFeeİnformation/CreateFee";
        }

        public struct Feeİnfo
        {
            public const string FeeİnfoList = "/api/Feeİnformation/Get";
        }

        public struct AdminExam
        {
            public const string add = "/api/AdminExam/add";
        }

        public struct Courses
        {
            public const string GetAllCourses = "api/Courses/list";
            public const string GetCourses = "api/Courses/GetCourses";
        }

        public struct Exam
        {
            
            public const string Get = "/api/Exam/GetExamlist";
            public const string GetExams = "/api/Exam/GetExams";

        }

        public struct ExamGrade 
        {
            public const string Get = "api/ExamGrade/Get";
        }

        public struct AdminExamGrade
        {
            public const string Add = "api/AdminExamGrade/Add";
        }

    }
}
