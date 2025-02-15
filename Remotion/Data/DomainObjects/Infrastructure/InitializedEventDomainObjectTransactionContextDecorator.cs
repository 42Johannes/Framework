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
using Remotion.Utilities;

namespace Remotion.Data.DomainObjects.Infrastructure
{
  /// <summary>
  /// Provides an implementation of the <see cref="IDomainObjectTransactionContext"/> interface that is returned while the 
  /// <see cref="DomainObject.OnReferenceInitializing"/> is run. It does not allow access to properties and methods that read or modify
  /// the state of the <see cref="DomainObject"/> in the associated <see cref="ClientTransaction"/>.
  /// </summary>
  public class InitializedEventDomainObjectTransactionContextDecorator : IDomainObjectTransactionContext
  {
    private readonly IDomainObjectTransactionContext _actualContext;

    public InitializedEventDomainObjectTransactionContextDecorator (IDomainObjectTransactionContext actualContext)
    {
      ArgumentUtility.CheckNotNull("actualContext", actualContext);

      _actualContext = actualContext;
    }

    public ClientTransaction ClientTransaction
    {
      get { return _actualContext.ClientTransaction; }
    }

    public DomainObjectState State
    {
      get { throw CreateInvalidOperationException(); }
    }

    public object Timestamp
    {
      get { throw CreateInvalidOperationException(); }
    }

    public void RegisterForCommit ()
    {
      throw CreateInvalidOperationException();
    }

    public void EnsureDataAvailable ()
    {
      throw CreateInvalidOperationException();
    }

    public bool TryEnsureDataAvailable ()
    {
      throw CreateInvalidOperationException();
    }

    private InvalidOperationException CreateInvalidOperationException ()
    {
      return new InvalidOperationException("While the OnReferenceInitializing event is executing, this member cannot be used.");
    }
  }
}
