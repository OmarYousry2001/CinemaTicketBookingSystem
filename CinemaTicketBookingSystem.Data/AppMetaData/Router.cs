﻿namespace CinemaTicketBookingSystem.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRoute = "{id}";

        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root+"/"+version+"/";

        public static class ActorRouting
        {
            public const string Prefix = Rule + "Actor/";
            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class DirectorRouting
        {
            public const string Prefix = Rule + "Director/";
            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class SeatRouting
        {
            public const string Prefix = Rule + "Seat/";
            public const string list = Prefix + "List";
            public const string FreeSeatsInShowTime = Prefix + "FreeSeats/{showTimeId}";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix  + "Create";
            public const string Edit = Prefix    + "Edit";
            public const string Delete = Prefix  + SingleRoute;
        }
        public static class HallRouting
        {
            public const string Prefix = Rule + "Hall/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class SeatTypeRouting
        {
            public const string Prefix = Rule + "SeatType/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class GenreRouting
        {
            public const string Prefix = Rule + "Genre/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class MovieRouting
        {
            public const string Prefix = Rule + "Movie/";
            public const string list = Prefix + "List";
            public const string PaginatedList = Prefix + "PaginatedList";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class ShowTimeRouting
        {
            public const string Prefix = Rule + "ShowTime/";

            public const string list = Prefix + "List";
            public const string comingList = Prefix + "ComingList";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class UserRouting
        {
            public const string Prefix = Rule + "User/";

            public const string list = Prefix + "List";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string ChangePassword = Prefix + "ChangePassword";
            public const string Delete = Prefix + SingleRoute;
            public const string UserReservations = Prefix + "Reservations/" + SingleRoute;
            public const string ConfirmEmail = "Api/V1/User/ConfirmEmail";
            public const string SendResetPassword = Prefix + "SendResetPassword";
            public const string ConfirmResetPasswordCode = Prefix + "ConfirmResetPasswordCode";
            public const string ResetPassword = Prefix + "ResetPassword";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication/";

            public const string ValidateToken = Prefix + "ValidateToken";

            public const string SignIn = Prefix + "SignIn";

            public const string RefreshToken = Prefix + "RefreshToken";
        }
        public static class ReservationRouting
        {
            public const string Prefix = Rule + "Reservation/";

            public const string list = Prefix + "List";
            public const string PaginatedList = Prefix + "PaginatedList";
            public const string GetById = Prefix + SingleRoute;


            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + SingleRoute;
        }
        public static class PaymentRouting
        {
            public const string Prefix = Rule + "Payment/";
            public const string Create = Prefix + "{reservationId}";
            public const string webhook = Prefix + "webhook";
        }
        //public static class AuthorizationRouting
        //{
        //    public const string Prefix = Rule + "Authorization/";

        //    public const string CreateRole = Prefix + "Role/Create";
        //    public const string EditRole = Prefix + "Role/Edit";
        //    public const string DeleteRole = Prefix + "Role/" + SingleRoute;

        //    public const string list = Prefix + "Role/" + "List";
        //    public const string GetById = Prefix + "Role/" + SingleRoute;
        //    public const string GetUserRoles = Prefix + "Role/User-Roles" + SingleRoute;
        //    public const string EditUserRoles = Prefix + "Role/Edit-User-Roles";

        //}
        //-------------------------
        public static class StudentRouting
        {
            public const string Prefix = Rule+"Student";
            public const string List = Prefix+"/List";
            public const string GetByID = Prefix+ SingleRoute;
            public const string Create = Prefix+"/Create";
            public const string Edit = Prefix+"/Edit";
            public const string Delete = Prefix+"/{id}";
            public const string Paginated = Prefix+"/Paginated";

        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule+"Department";
            public const string GetByID = Prefix+"/Id";
            public const string GetDepartmentStudentsCount = Prefix+"/Department-Students-Count";
            public const string GetDepartmentStudentsCountById = Prefix+"/Department-Students-Count-ById/{id}";

        }
        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule+"User";
            public const string Create = Prefix+"/Create";
            public const string Paginated = Prefix+"/Paginated";
            public const string GetByID = Prefix+ SingleRoute;
            public const string Edit = Prefix+"/Edit";
            public const string Delete = Prefix+"/{id}";
            public const string ChangePassword = Prefix+"/Change-Password";
        }
        public static class Authentication
        {
            public const string Prefix = Rule+"Authentication";
            public const string SignIn = Prefix+"/SignIn";
            public const string RefreshToken = Prefix+"/Refresh-Token";
            public const string ValidateToken = Prefix+"/Validate-Token";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
            public const string SendResetPasswordCode = Prefix+ "/SendResetPasswordCode";
            public const string ConfirmResetPasswordCode = Prefix+ "/ConfirmResetPasswordCode";
            public const string ResetPassword = Prefix+ "/ResetPassword";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "AuthorizationRouting";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{id}";
            public const string RoleList = Roles + "/Role-List";
            public const string GetRoleById = Roles + "/Role-By-Id/{id}";
            public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";
        }
        public static class EmailsRoute
        {
            public const string Prefix = Rule+"EmailsRoute";
            public const string SendEmail = Prefix+"/SendEmail";
        }
        public static class InstructorRouting
        {
            public const string Prefix = Rule+"InstructorRouting";
            public const string GetSalarySummationOfInstructor = Prefix+"/Salary-Summation-Of-Instructor";
            public const string AddInstructor = Prefix+"/Create";
        }


    }
}
