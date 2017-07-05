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
using Remotion.ObjectBinding.Web.Development.WebTesting.IntegrationTests.GenericTestCaseInfrastructure.Factories;

namespace Remotion.ObjectBinding.Web.Development.WebTesting.IntegrationTests.GenericTestCaseInfrastructure.TestParameters
{
  /// <summary>
  /// Parameters for <see cref="SingleControlSelectorTestCaseFactory{TControlSelector,TControl}"/>.
  /// </summary>
  public class SingleTestParameters : ITestParameters
  {
    private string _foundControlID;

    public SingleTestParameters ()
    {
    }

    public string FoundControlID
    {
      get { return _foundControlID; }
    }

    /// <inheritdoc />
    public string Name
    {
      get { return TestConstants.Single; }
    }

    /// <inheritdoc />
    public bool Apply (string[] data)
    {
      if (data.Length != 1)
        return false;

      _foundControlID = data[0];
      return true;
    }
  }
}