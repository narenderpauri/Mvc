// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    internal class PageViewDataAttributeFilter : IPageFilter, IPageExecutionCallbackFeature, IViewComponentExecutionCallbackFeature
    {
        private readonly IReadOnlyList<LifecycleProperty> _properties;

        public PageViewDataAttributeFilter(IReadOnlyList<LifecycleProperty> properties)
        {
            _properties = properties;
        }

        public object Subject { get; set; }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            Subject = context.HandlerInstance;
            context.HttpContext.Features.Set<IPageExecutionCallbackFeature>(this);
            context.HttpContext.Features.Set<IViewComponentExecutionCallbackFeature>(this);
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }

        public void OnViewComponentExecuting(ViewContext context)
        {
            SaveProperties(context.ViewData);
        }

        public void OnPageExecuting(PageContext context)
        {
            SaveProperties(context.ViewData);
        }

        private void SaveProperties(ViewDataDictionary viewData)
        {
            for (var i = 0; i < _properties.Count; i++)
            {
                var property = _properties[i];
                var value = property.GetValue(Subject);

                if (value != null)
                {
                    viewData[property.Key] = value;
                }
            }
        }
    }
}
