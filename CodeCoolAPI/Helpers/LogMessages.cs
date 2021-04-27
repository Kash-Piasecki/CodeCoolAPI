namespace CodeCoolAPI.Helpers
{
    public static class LogMessages
    {
        public const string EntitiesFound = "Entities Found, Code 200 OK";
        public const string EntityFound = "Entity Found, Code 200 OK";
        public const string EntityCreated = "Entity Created, Code 201 Created At Action";
        public const string EntityUpdated = "Entity Updated, Code 200 OK";
        public const string EntityDeleted = "Entity Removed, Code 204 No Content";
        public const string EntityNotFound = "Entity Not Found Exception. Error Code 404 NotFound";
        public const string BadRequest = "Bad Request Exception. Error Code 404 BadRequest";
        public const string InternalServerError = "Internal Server Exception. Error Code 500 InternalServerError";
    }
}