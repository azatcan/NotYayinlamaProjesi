using ÜNY.WebAPI.Model.CoursesModel;

namespace ÜNY.WebAPI.Model.EnrolmentModel
{
    public class EnrollCoursesViewModel
    {
        public string UnitName { get; set; }
        public List<CourseEViewModel> AvailableCourses { get; set; }
    }
}
