// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Authentication.JwtBearer
{
    public class ValidatedTokenContext : BaseJwtBearerContext
    {
        public ValidatedTokenContext(HttpContext context, JwtBearerOptions options)
            : base(context, options)
        {
        }
    }
}
