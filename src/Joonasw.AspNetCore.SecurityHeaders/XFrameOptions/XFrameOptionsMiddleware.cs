﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Joonasw.AspNetCore.SecurityHeaders.XFrameOptions
{
    public class XFrameOptionsMiddleware
    {
        private const string HeaderName = "X-Frame-Options";
        private readonly RequestDelegate _next;
        private readonly string _headerValue;

        public XFrameOptionsMiddleware(RequestDelegate next, IOptions<XFrameOptionsOptions> options)
        {
            _next = next;
            _headerValue = options.Value.BuildHeaderValue();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!ContainsXFrameOptionsHeader(context.Response))
            {
                context.Response.Headers.Add(HeaderName, _headerValue);
            }
            await _next(context);
        }

        private static bool ContainsXFrameOptionsHeader(HttpResponse response)
        {
            return response.Headers.Any(h => h.Key.Equals(HeaderName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
