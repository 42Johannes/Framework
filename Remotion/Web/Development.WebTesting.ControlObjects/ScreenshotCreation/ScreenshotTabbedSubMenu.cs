﻿// This file is part of the re-motion Core Framework (www.re-motion.org)
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
using Coypu;
using JetBrains.Annotations;
using Remotion.Utilities;
using Remotion.Web.Development.WebTesting.ScreenshotCreation;
using Remotion.Web.Development.WebTesting.ScreenshotCreation.Fluent;
using Remotion.Web.Development.WebTesting.Utilities;

namespace Remotion.Web.Development.WebTesting.ControlObjects.ScreenshotCreation
{
  /// <summary>
  /// Marker class for the screenshot fluent API. Represents the sub-menu of a <see cref="TabbedMenu"/>.
  /// </summary>
  public class ScreenshotTabbedSubMenu : ISelfResolvable
  {
    private readonly IFluentScreenshotElementWithCovariance<TabbedMenuControlObject> _fluentTabbedMenu;
    private readonly IFluentScreenshotElement<ElementScope> _fluentElement;

    public ScreenshotTabbedSubMenu (
        [NotNull] IFluentScreenshotElementWithCovariance<TabbedMenuControlObject> fluentTabbedMenu,
        [NotNull] IFluentScreenshotElement<ElementScope> fluentElement)
    {
      ArgumentUtility.CheckNotNull("fluentTabbedMenu", fluentTabbedMenu);
      ArgumentUtility.CheckNotNull("fluentElement", fluentElement);


      _fluentTabbedMenu = fluentTabbedMenu;
      _fluentElement = fluentElement;
    }

    public IFluentScreenshotElement<ElementScope> FluentElement
    {
      get { return _fluentElement; }
    }

    public IFluentScreenshotElementWithCovariance<TabbedMenuControlObject> FluentTabbedMenu
    {
      get { return _fluentTabbedMenu; }
    }

    public ElementScope Element
    {
      get { return _fluentElement.Target; }
    }

    public TabbedMenuControlObject TabbedMenu
    {
      get { return _fluentTabbedMenu.Target; }
    }

    /// <inheritdoc />
    public ResolvedScreenshotElement ResolveBrowserCoordinates ()
    {
      return _fluentElement.ResolveBrowserCoordinates();
    }

    /// <inheritdoc />
    public ResolvedScreenshotElement ResolveDesktopCoordinates (IBrowserContentLocator locator)
    {
      ArgumentUtility.CheckNotNull("locator", locator);

      return _fluentElement.ResolveDesktopCoordinates(locator);
    }
  }
}
