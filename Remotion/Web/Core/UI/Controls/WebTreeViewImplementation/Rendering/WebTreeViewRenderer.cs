// This file is part of the re-motion Core Framework (www.re-motion.org)
// Copyright (c) rubicon IT GmbH, www.rubicon.eu
// 
// The re-motion Core Framework is free software; you can redistribute it 
// and/or modify it under the terms of the GNU Lesser General Public License 
// as published by the Free Software Foundation; either version 2.1 of the 
// License, or (at your option) any later version.
// 
// re-motion is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with re-motion; if not, see http://www.gnu.org/licenses.
// 
using System;
using Remotion.Globalization;
using Remotion.Reflection;
using Remotion.ServiceLocation;
using Remotion.Utilities;
using Remotion.Web.UI.Controls.Rendering;

namespace Remotion.Web.UI.Controls.WebTreeViewImplementation.Rendering
{
  /// <summary>
  /// Implements <see cref="IWebTreeViewRenderer"/> for standard mode rendering of <see cref="WebTreeView"/> controls.
  /// <seealso cref="IWebTreeView"/>
  /// </summary>
  [ImplementationFor(typeof(IWebTreeViewRenderer), Lifetime = LifetimeKind.Singleton)]
  public class WebTreeViewRenderer : RendererBase<IWebTreeView>, IWebTreeViewRenderer
  {
    public WebTreeViewRenderer (
        IResourceUrlFactory resourceUrlFactory,
        IGlobalizationService globalizationService,
        IRenderingFeatures renderingFeatures)
        : base(resourceUrlFactory, globalizationService, renderingFeatures)
    {
    }

    public void RegisterHtmlHeadContents (HtmlHeadAppender htmlHeadAppender)
    {
      ArgumentUtility.CheckNotNull("htmlHeadAppender", htmlHeadAppender);

      string scriptFileKey = typeof(WebTreeViewRenderer).GetFullNameChecked() + "_Script";
      var scriptFileUrl = ResourceUrlFactory.CreateResourceUrl(typeof(WebTreeViewRenderer), ResourceType.Html, "TreeView.js");
      htmlHeadAppender.RegisterJavaScriptInclude(scriptFileKey, scriptFileUrl);

      htmlHeadAppender.RegisterCommonStyleSheet();

      string styleKey = typeof(WebTreeViewRenderer).GetFullNameChecked() + "_Style";
      var styleSheetUrl = ResourceUrlFactory.CreateThemedResourceUrl(typeof(WebTreeViewRenderer), ResourceType.Html, "TreeView.css");
      htmlHeadAppender.RegisterStylesheetLink(styleKey, styleSheetUrl, HtmlHeadAppender.Priority.Library);
    }

    public void Render (WebTreeViewRenderingContext renderingContext)
    {
      throw new NotSupportedException("The WebTreeView does not support customized rendering.");
    }
  }
}
