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
using JetBrains.Annotations;
using OpenQA.Selenium.IE;
using Remotion.Web.Development.WebTesting.Configuration;
using Remotion.Web.Development.WebTesting.DownloadInfrastructure;
using Remotion.Web.Development.WebTesting.DownloadInfrastructure.InternetExplorer;
using Remotion.Web.Development.WebTesting.Utilities;
using Remotion.Web.Development.WebTesting.Utilities.BrowserContentLocators;
using Remotion.Web.Development.WebTesting.WebDriver.Factories;
using Remotion.Web.Development.WebTesting.WebDriver.Factories.InternetExplorer;

namespace Remotion.Web.Development.WebTesting.WebDriver.Configuration.InternetExplorer
{
  /// <summary>
  /// Implements the <see cref="IBrowserConfiguration"/> interface for Internet Explorer.
  /// </summary>
  public class InternetExplorerConfiguration : BrowserConfigurationBase, IInternetExplorerConfiguration
  {
    private readonly IBrowserContentLocator _locator = new InternetExplorerBrowserContentLocator();

    private readonly IDownloadHelper _downloadHelper;

    public InternetExplorerConfiguration ([NotNull] WebTestConfigurationSection webTestConfigurationSection)
        : base (webTestConfigurationSection)
    {
      _downloadHelper = new InternetExplorerDownloadHelper (
          webTestConfigurationSection.DownloadStartedTimeout,
          webTestConfigurationSection.DownloadUpdatedTimeout,
          webTestConfigurationSection.CleanUpUnmatchedDownloadedFiles);
    }

    public override string BrowserExecutableName
    {
      get { return "iexplore"; }
    }

    public override string WebDriverExecutableName
    {
      get { return "IEDriverServer"; }
    }

    public override IBrowserFactory BrowserFactory
    {
      get { return new InternetExplorerBrowserFactory (this); }
    }

    public override IDownloadHelper DownloadHelper
    {
      get { return _downloadHelper; }
    }

    public override IBrowserContentLocator Locator
    {
      get { return _locator; }
    }

    public virtual InternetExplorerOptions CreateInternetExplorerOptions ()
    {
      return new InternetExplorerOptions
             {
                 EnableNativeEvents = true,
                 RequireWindowFocus = true,
                 EnablePersistentHover = false
             };
    }
  }
}