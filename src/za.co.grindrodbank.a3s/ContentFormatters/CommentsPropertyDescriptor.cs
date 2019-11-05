/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace za.co.grindrodbank.a3s.ContentFormatters
{
    public sealed class CommentsPropertyDescriptor : IPropertyDescriptor
    {
        private readonly IPropertyDescriptor baseDescriptor;

        public CommentsPropertyDescriptor(IPropertyDescriptor baseDescriptor)
        {
            this.baseDescriptor = baseDescriptor;
            Name = baseDescriptor.Name;
        }

        public string Name { get; set; }

        public Type Type { get { return baseDescriptor.Type; } }

        public Type TypeOverride
        {
            get { return baseDescriptor.TypeOverride; }
            set { baseDescriptor.TypeOverride = value; }
        }

        public int Order { get; set; }

        public ScalarStyle ScalarStyle
        {
            get { return baseDescriptor.ScalarStyle; }
            set { baseDescriptor.ScalarStyle = value; }
        }

        public bool CanWrite { get { return baseDescriptor.CanWrite; } }

        public void Write(object target, object value)
        {
            baseDescriptor.Write(target, value);
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return baseDescriptor.GetCustomAttribute<T>();
        }

        public IObjectDescriptor Read(object target)
        {
            var description = baseDescriptor.GetCustomAttribute<DescriptionAttribute>();
            return description != null
                ? new CommentsObjectDescriptor(baseDescriptor.Read(target), description.Description)
                : baseDescriptor.Read(target);
        }
    }
}

