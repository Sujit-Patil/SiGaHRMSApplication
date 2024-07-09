namespace SiGaHRMS.Data.Constants
{
    public static class UserActionConstants
    {
        public static readonly string InvalidUser = "1001";
        public static readonly string RoleCreationFailed = "1002";
        public static readonly string AssigningRoleFailed = "1003";
        public static readonly string TimeSheetAddFailed = "1004";
        public static readonly string TimeSheetUpdateFailed = "1005";
        public static readonly string TimeSheetDeleteFailed = "1006";
        public static readonly string TaskAddFailed = "1007";

        public static readonly IDictionary<string, string> ErrorDescriptions = new Dictionary<string, string>
        {
            {InvalidUser, "UserName or Password is incorrect." },
            {RoleCreationFailed, "Role creation failed or role already exists." },
            {AssigningRoleFailed, "Error encountered while assigning the role" },
            {TimeSheetAddFailed,"Error encountered while creating new Timesheet " },
            {TimeSheetUpdateFailed,"Error encountered while updating exists Timesheet " },
            {TimeSheetDeleteFailed,"Error encountered while deleting exists Timesheet " },
            {TaskAddFailed,"Error encountered while creating new Task For Timesheet " },
        };

        public static string Format(string code, params object[] args)
        {
            if (ErrorDescriptions.ContainsKey(code))
            {
                return string.Format(ErrorDescriptions[code], args);
            }

            return string.Empty;
        }
    }
}
