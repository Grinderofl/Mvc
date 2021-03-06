// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.Framework.Internal;
using Microsoft.Framework.WebEncoders;

namespace Microsoft.AspNet.Mvc.Rendering
{
    /// <summary>
    /// String content which knows how to write itself.
    /// </summary>
    public class HtmlString : IHtmlContent
    {
        private readonly string _input;

        /// <summary>
        /// Returns an <see cref="HtmlString"/> with empty content.
        /// </summary>
        public static readonly HtmlString Empty = new HtmlString(string.Empty);

        /// <summary>
        /// Returns an <see cref="HtmlString"/> containing <see cref="Environment.NewLine"/>.
        /// </summary>
        public static readonly HtmlString NewLine = new HtmlString(Environment.NewLine);

        /// <summary>
        /// Creates a new instance of <see cref="HtmlString"/>.
        /// </summary>
        /// <param name="input"><c>string</c> to initialize <see cref="HtmlString"/>.</param>
        public HtmlString(string input)
        {
            _input = input;
        }

        /// <inheritdoc />
        public void WriteTo([NotNull] TextWriter writer, [NotNull] IHtmlEncoder encoder)
        {
            writer.Write(_input);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _input;
        }
    }
}
