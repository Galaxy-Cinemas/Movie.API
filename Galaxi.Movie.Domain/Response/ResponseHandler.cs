﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Movie.Domain.Response
{
    public class ResponseHandler<T>
    {
        public int? StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ResponseHandler()
        {
            Errors = new List<string>();
        }
        public static ResponseHandler<T> CreateNotFoundResponse(string message, string error)
        {
            return new ResponseHandler<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error },
                StatusCode = 404 // NotFound
            };
        }

        public static ResponseHandler<T> CreateErrorResponse(string message, Exception ex)
        {
            return new ResponseHandler<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { ex.Message },
                StatusCode = 500 // InternalServerError
            };
        }
        public static ResponseHandler<T> CreateErrorResponse(string message, List<string> error)
        {
            return new ResponseHandler<T>
            {
                Success = false,
                Message = message,
                Errors =  error ,
                StatusCode = 400 //  Bad Request
            };
        }

        public static ResponseHandler<T> CreateSuccessResponse(string message, T data)
        {
            return new ResponseHandler<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = 200 // OK
            };
        }

    }
}