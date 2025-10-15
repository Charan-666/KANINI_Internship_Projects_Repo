namespace Complaint_2._0.Constants
{
    public static class Messages
    {
        // Auth Messages
        public const string InvalidEmailOrPassword = "Invalid email or password";
        public const string UserAlreadyExists = "User with this email already exists";
        public const string AdminAlreadyExists = "Admin user already exists";
        public const string AdminCreatedSuccess = "Admin user created successfully. Email: admin@admin.com, Password: admin123";
        public const string RegistrationSuccess = "Registration successful. Please login to continue.";
        public const string RegistrationFailed = "Registration failed";

        // Complaint Messages
        public const string ComplaintCreated = "Complaint created";
        public const string ComplaintNotFound = "Complaint not found";
        public const string StatusUpdated = "Status updated";
        public const string ComplaintAssigned = "Complaint assigned";
        public const string ComplaintDeleted = "Complaint deleted";

        // File Messages
        public const string InvalidFileType = "Only image files (JPEG, PNG, GIF) are allowed";
        public const string FileSizeExceeded = "File size cannot exceed 5MB";

        // General Messages
        public const string OperationFailed = "Operation failed";
        public const string NotFound = "Resource not found";
        public const string Unauthorized = "Unauthorized access";
    }
}