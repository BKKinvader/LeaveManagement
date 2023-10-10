﻿using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace LeaveManagement_API.Model
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set;}

    }
}
