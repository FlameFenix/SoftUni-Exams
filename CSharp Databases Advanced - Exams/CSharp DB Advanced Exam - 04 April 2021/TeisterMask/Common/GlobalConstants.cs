using System;
using System.Collections.Generic;
using System.Text;

namespace TeisterMask.Common
{
    public static class GlobalConstants
    {
        // Employee
        public const string EMPLOYEE_USERNAME_REGEX = @"^[A-Za-z0-9]+$";
        public const int EMPLOYEE_USERNAME_MIN_LENGTH = 2;
        public const int EMPLOYEE_USERNAME_MAX_LENGTH = 40;

        public const string EMPLOYEE_PHONE_REGEX = @"^[0-9]{3}-[0-9]{3}-[0-9]{4}$";
        public const int EMPLOYEE_PHONE_MAX_LENGTH = 12;

        // Project
        public const int PROJECT_NAME_MIN_LENGTH = 2;
        public const int PROJECT_NAME_MAX_LENGTH = 40;


        // Task
        public const int TASK_NAME_MIN_LENGTH = 2;
        public const int TASK_NAME_MAX_LENGTH = 40;

        public const int TASK_EXEC_TYPE_RANGE_FROM = 0;
        public const int TASK_EXEC_TYPE_RANGE_TO = 3;

        public const int TASK_LABEL_TYPE_RANGE_FROM = 0;
        public const int TASK_LABEL_TYPE_RANGE_TO = 4;
    }
}
