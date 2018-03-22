// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    internal class PageViewDataAttributeFilterFactory : IFilterFactory
    {
        private readonly IReadOnlyList<LifecycleProperty> _properties;

        public PageViewDataAttributeFilterFactory(IReadOnlyList<LifecycleProperty> properties)
        {
            _properties = properties;
        }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // PageViewDataAttributeFilterFactory is stateful and must be recreated for each request.
            return new PageViewDataAttributeFilter(_properties);
        }
    }
}
