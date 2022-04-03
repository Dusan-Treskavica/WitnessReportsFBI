namespace Common.Constants
{
    public static class HTTPResponseCodes
    {
        // OK - 20x
        public const int OK = 200;
        public const int CREATED = 201;
        public const int ACCEPTED = 202;
        public const int NO_CONTENT = 204;

        //ERROR - 40x
        public const int BAD_REQUEST = 400;
        public const int UNAUTHORIZED = 403;
        public const int NOT_FOUND = 404;
        public const int NOT_ALLOWED = 405;
        public const int CONFLICT_RESOURCES = 409;

        //SERVER ERROR - 50x
        public const int INTERNAL_SERVER_ERROR = 500;
    }
}
