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
using System.Linq;
using NUnit.Framework;
using Remotion.Development.NUnit.UnitTesting;
using Remotion.Utilities;
using Remotion.Validation.Implementation;
using Remotion.Validation.Validators;

namespace Remotion.Validation.UnitTests.Validators
{
  [TestFixture]
  public class PredicateValidatorTest : ValidatorTestBase
  {
    [Test]
    public void Validate_WithPredicateTrue_ReturnsNoValidationFailures ()
    {
      var propertyValidatorContext = CreatePropertyValidatorContext(1);
      var validator = new PredicateValidator((instanceToValidate, propertyValue, context) => true, new InvariantValidationMessage("Fake Message"));

      var validationFailures = validator.Validate(propertyValidatorContext);

      Assert.That(validationFailures, Is.Empty);
    }

    [Test]
    public void Validate_WithPredicate_GetsValidArguments ()
    {
      var propertyValidatorContext = CreatePropertyValidatorContext(1);
      var validator = new PredicateValidator(
          (instanceToValidate, propertyValue, context) =>
          {
            Assert.That(context, Is.SameAs(propertyValidatorContext));
            Assert.That(instanceToValidate, Is.SameAs(propertyValidatorContext.Instance));
            Assert.That(propertyValue, Is.SameAs(propertyValidatorContext.PropertyValue));
            return false;
          },
          new InvariantValidationMessage("Fake Message"));

      validator.Validate(propertyValidatorContext);
    }

    [Test]
    public void Validate_WithPredicateFalse_ReturnsSingleValidationFailure ()
    {
      var propertyValidatorContext = CreatePropertyValidatorContext(1);
      var validator = new PredicateValidator((instanceToValidate, propertyValue, context) => false, new InvariantValidationMessage("Custom validation message."));

      var validationFailures = validator.Validate(propertyValidatorContext).ToArray();

      Assert.That(validationFailures.Length, Is.EqualTo(1));
      //TODO RM-5906: Assert ValidatedObject, ValidatedProperty, ValidatedValue
      Assert.That(validationFailures[0].ErrorMessage, Is.EqualTo("The value must meet the specified condition."));
      Assert.That(validationFailures[0].LocalizedValidationMessage, Is.EqualTo("Custom validation message."));
    }

    [Test]
    public void Validate_WithPredicateNull_ThrowsArgumentNullException ()
    {
      using (CultureScope.CreateInvariantCultureScope())
      {
        Assert.That(
            () => new PredicateValidator(null, new InvariantValidationMessage("Fake Message")),
            Throws.InstanceOf<ArgumentNullException>()
                .With.ArgumentExceptionMessageEqualTo($"Value cannot be null.", "predicate"));
      }
    }
  }
}
