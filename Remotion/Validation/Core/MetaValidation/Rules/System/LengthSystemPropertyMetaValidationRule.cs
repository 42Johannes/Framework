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
using System.Linq;
using Remotion.Reflection;
using Remotion.Utilities;
using Remotion.Validation.Validators;

namespace Remotion.Validation.MetaValidation.Rules.System
{
  /// <summary>
  /// Implements the <see cref="IPropertyMetaValidationRule"/> interface to verify that there no more than a single (distinct) minimum 
  /// and a single (distinct) maximum length constraint applied to each property.
  /// </summary>
  public class LengthSystemPropertyMetaValidationRule : SystemPropertyMetaValidationRuleBase<LengthValidator>
  {
    public LengthSystemPropertyMetaValidationRule (IPropertyInformation propertyInfo)
        : base(propertyInfo)
    {
    }

    public override IEnumerable<MetaValidationRuleValidationResult> Validate (IEnumerable<LengthValidator> validationRules)
    {
      ArgumentUtility.CheckNotNull("validationRules", validationRules);

      var rules = validationRules.ToArray();

      yield return
          GetValidationResult(
              rules.Select(r => r.Min).Where(min => min > 0).Distinct().Count() <= 1
              && rules.Select(r => r.Max).Where(max => max != null).Distinct().Count() <= 1);
    }
  }
}
