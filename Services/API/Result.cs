﻿using Newtonsoft.Json;

namespace Services.API
{
    public class Result
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public Result()
        {
            Code = OK;
        }

        //Codes
        public const int OK = 200;
        public const int BAD_REQUEST = 400;
        public const int UNAUTHORIZED = 401;
        public const int NOT_FOUND = 404;
        public const int UNPROCESSABLE_ENTITY = 422;
        public const int SERVER_ERROR = 500;

        //Extensions
        public static implicit operator Result(string sResult)
        {
            return JsonConvert.DeserializeObject<Result>(sResult);
        }
    }
}
