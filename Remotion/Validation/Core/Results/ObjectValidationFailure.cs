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
using JetBrains.Annotations;
using Remotion.Reflection;
using Remotion.Utilities;

namespace Remotion.Validation.Results
{
  public class ObjectValidationFailure : ValidationFailure
  {
    [NotNull]
    public IReadOnlyCollection<ValidatedProperty> ValidatedProperties { get; }

    public ObjectValidationFailure (
        [NotNull] object validatedObject,
        [NotNull] string errorMessage,
        [NotNull] string localizedValidationMessage)
        : this(validatedObject, Array.Empty<ValidatedProperty>(), errorMessage, localizedValidationMessage)
    {
    }

    public ObjectValidationFailure (
        [NotNull] object validatedObject,
        [NotNull] IReadOnlyCollection<ValidatedProperty> validatedProperties,
        [NotNull] string errorMessage,
        [NotNull] string localizedValidationMessage)
        : base(validatedObject, errorMessage, localizedValidationMessage)
    {
      ArgumentUtility.CheckNotNull("validatedProperties", validatedProperties);

      ValidatedProperties = validatedProperties;
    }
  }
}
