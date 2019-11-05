/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;

namespace za.co.grindrodbank.a3s.ContentFormatters
{
    public class CommentGatheringFullObjectGraphTraversalStrategy : FullObjectGraphTraversalStrategy
    {
        private readonly ITypeInspector typeDescriptor;

        public CommentGatheringFullObjectGraphTraversalStrategy(ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion, INamingConvention namingConvention)
            : base(typeDescriptor, typeResolver, maxRecursion, namingConvention)
        {
            this.typeDescriptor = typeDescriptor;
        }

        protected override void TraverseProperties<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
        {
            visitor.VisitMappingStart(value, typeof(string), typeof(object), context);

            var source = value.NonNullValue();
            foreach (var propertyDescriptor in typeDescriptor.GetProperties(value.Type, source))
            {
                var propertyValue = propertyDescriptor.Read(source);

                if (visitor.EnterMapping(propertyDescriptor, propertyValue, context))
                {
                    Traverse(propertyDescriptor.Name, new ObjectDescriptor(propertyDescriptor.Name, typeof(string), typeof(string)), visitor, context, path);
                    Traverse(propertyDescriptor.Name, propertyValue, visitor, context, path);
                }
            }

            visitor.VisitMappingEnd(value, context);
        }
    }
}
