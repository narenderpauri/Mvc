// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    internal class ControllerViewDataAttributeFilterFactory : IFilterFactory
    {
        private readonly IReadOnlyList<LifecycleProperty> _properties;

        public ControllerViewDataAttributeFilterFactory(IReadOnlyList<LifecycleProperty> properties)
        {
            _properties = properties;
        }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // ControllerViewDataAttributeFilter is stateful and must be recreated for each request.
            return new ControllerViewDataAttributeFilter(_properties);
        }
    }
}
