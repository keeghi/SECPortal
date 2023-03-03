using System;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public interface IBaseResponse
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }

    [Serializable]
    public class BaseResponse : IBaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        protected BaseResponse()
        {

        }

        public BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static class Factory
        {
            public static BaseResponse Build()
            {
                return new BaseResponse();
            }

            public static BaseResponse<T> BuildSuccessResponse<T>(int totalRows, T data)
            {
                return new BaseResponse<T>(true, string.Empty, data, totalRows);
            }

            public static BaseResponse BuildSuccessResponse(string message)
            {
                return new BaseResponse(true, message);
            }

            public static BaseResponse BuildSuccessResponse()
            {
                return new BaseResponse(true, string.Empty);
            }

            public static BaseResponse BuildFailedResponse(string message)
            {
                return new BaseResponse(false, message);
            }

            public static BaseResponse<T> BuildFailedResponse<T>(int totalRows, T data)
            {
                return new BaseResponse<T>(false, string.Empty, data, totalRows);
            }

        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public int TotalRows { get; set; }
        public T Data { get; set; }

        public BaseResponse(bool isSuccess, string message, T data, int totalRows) : base(isSuccess, message)
        {
            Data = data;
            TotalRows = totalRows;
        }
    }

    public class JwtResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiry { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        private JwtResponse()
        {

        }

        public JwtResponse(string accessToken, bool isSuccess, string message, string username, string fullname)
            : base(isSuccess, message)
        {
            AccessToken = accessToken;
        }

        public JwtResponse(string id, string role, string accessToken, DateTime expiry, string username, string name) : base(true, string.Empty)
        {
            Id = id;
            Role = role;
            AccessToken = accessToken;
            Expiry = expiry;
            Username = username;
            Name = name;
        }

        public class JwtFactory
        {
            public static JwtResponse Build()
            {
                return new JwtResponse();
            }
        }
    }
}
