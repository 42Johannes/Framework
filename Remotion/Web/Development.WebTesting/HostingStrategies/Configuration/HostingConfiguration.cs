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
using System.Collections.Generic;
using System.Configuration;
using JetBrains.Annotations;
using Remotion.Utilities;
using Remotion.Web.Development.WebTesting.Configuration;

namespace Remotion.Web.Development.WebTesting.HostingStrategies.Configuration
{
  /// <summary>
  /// Default implementation of <see cref="IHostingConfiguration"/>.
  /// </summary>
  /// <remarks>
  /// <see cref="WebTestConfigurationSection"/> <see cref="WebTestConfigurationSection.HostingProviderSettings"/> 
  /// provides the hosting strategy type, use <c>IisExpress</c> for hosting the application in an auto-configured IIS Express or <c>Docker</c>
  /// for hosting the application in a Docker container. Leave <see cref="WebTestConfigurationSection.HostingProviderSettings"/> empty if the
  /// web application is already hosted, eg. in IIS.
  /// </remarks>
  public class HostingConfiguration : IHostingConfiguration
  {
    private readonly ITestSiteLayoutConfiguration _testSiteLayoutConfiguration;

    private static readonly Dictionary<string, Type> s_wellKnownHostingStrategyTypes =
        new Dictionary<string, Type>
        {
            { "IisExpress", typeof(IisExpressHostingStrategy) },
            { "Docker", typeof(DockerHostingStrategy) },
        };

    private readonly ProviderSettings _hostingProviderSettings;
    private TimeSpan _verifyWebApplicationStartedTimeout;

    public HostingConfiguration ([NotNull] WebTestConfigurationSection webTestConfigurationSection, [NotNull] ITestSiteLayoutConfiguration testSiteLayoutConfiguration)
    {
      ArgumentUtility.CheckNotNull("webTestConfigurationSection", webTestConfigurationSection);
      ArgumentUtility.CheckNotNull("testSiteLayoutConfiguration", testSiteLayoutConfiguration);

      _hostingProviderSettings = webTestConfigurationSection.HostingProviderSettings;
      _verifyWebApplicationStartedTimeout = webTestConfigurationSection.VerifyWebApplicationStartedTimeout;
      _testSiteLayoutConfiguration = testSiteLayoutConfiguration;
    }

    public TimeSpan VerifyWebApplicationStartedTimeout => _verifyWebApplicationStartedTimeout;

    public IHostingStrategy GetHostingStrategy ()
    {
      if (string.IsNullOrEmpty(_hostingProviderSettings.Type))
        return new NullHostingStrategy();

      var hostingStrategyTypeName = _hostingProviderSettings.Type;
      var hostingStrategyType = GetHostingStrategyType(hostingStrategyTypeName);
      Assertion.IsNotNull(hostingStrategyType, string.Format("Hosting strategy '{0}' could not be loaded.", hostingStrategyTypeName));

      var hostingStrategy = (IHostingStrategy)Activator.CreateInstance(hostingStrategyType, new object[] { _testSiteLayoutConfiguration, _hostingProviderSettings.Parameters })!;
      return hostingStrategy;
    }

    [CanBeNull]
    private Type? GetHostingStrategyType (string hostingStrategyTypeName)
    {
      if (s_wellKnownHostingStrategyTypes.ContainsKey(hostingStrategyTypeName))
        return s_wellKnownHostingStrategyTypes [hostingStrategyTypeName];

      return Type.GetType(hostingStrategyTypeName, throwOnError: false, ignoreCase: false);
    }
  }
}
