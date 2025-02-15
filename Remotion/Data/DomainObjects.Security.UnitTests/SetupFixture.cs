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
using NUnit.Framework;
using Remotion.Configuration;
using Remotion.Data.DomainObjects.Configuration;
using Remotion.Data.DomainObjects.Development;
using Remotion.Data.DomainObjects.Mapping;
using Remotion.Data.DomainObjects.Persistence.Configuration;
using Remotion.Data.DomainObjects.Persistence.Rdbms;
using Remotion.Data.DomainObjects.Security.UnitTests.TestDomain;
using Remotion.Development.UnitTesting;

namespace Remotion.Data.DomainObjects.Security.UnitTests
{
  [SetUpFixture]
  public class SetUpFixture
  {
    [OneTimeSetUp]
    public void OneTimeSetUp ()
    {
      try
      {
        var providers = new ProviderCollection<StorageProviderDefinition>();
        providers.Add(new RdbmsProviderDefinition(StubStorageProvider.StorageProviderID, new StubStorageFactory(), "NonExistingRdbms"));
        var storageConfiguration = new StorageConfiguration(providers, providers[StubStorageProvider.StorageProviderID]);
        DomainObjectsConfiguration.SetCurrent(new FakeDomainObjectsConfiguration(storage: storageConfiguration));

        Dev.Null = MappingConfiguration.Current;
      }
      catch (Exception ex)
      {
        Console.WriteLine("SetUpFixture failed: " + ex);
        Console.WriteLine();
        throw;
      }
    }
  }
}
