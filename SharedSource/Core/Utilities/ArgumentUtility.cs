// Copyright (c) rubicon IT GmbH, www.rubicon.eu
//
// See the NOTICE file distributed with this work for additional information
// regarding copyright ownership.  rubicon licenses this file to you under 
// the Apache License, Version 2.0 (the "License"); you may not use this 
// file except in compliance with the License.  You may obtain a copy of the 
// License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the 
// License for the specific language governing permissions and limitations
// under the License.
// 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
#nullable enable
// ReSharper disable once CheckNamespace
namespace Remotion.Utilities
{
  /// <summary>
  /// This utility class provides methods for checking arguments.
  /// </summary>
  /// <remarks>
  /// Some methods of this class return the value of the parameter. In some cases, this is useful because the value will be converted to another 
  /// type:
  /// <code><![CDATA[
  /// void foo (object o) 
  /// {
  ///   int i = ArgumentUtility.CheckNotNullAndType<int> ("o", o);
  /// }
  /// ]]></code>
  /// In some other cases, the input value is returned unmodified. This makes it easier to use the argument checks in calls to base class constructors
  /// or property setters:
  /// <code><![CDATA[
  /// class MyType : MyBaseType
  /// {
  ///   public MyType (string name) : base (ArgumentUtility.CheckNotNullOrEmpty ("name", name))
  ///   {
  ///   }
  /// 
  ///   public override Name
  ///   {
  ///     set { base.Name = ArgumentUtility.CheckNotNullOrEmpty ("value", value); }
  ///   }
  /// }
  /// ]]></code>
  /// </remarks>
  static partial class ArgumentUtility
  {
    [AssertionMethod]
#if !DEBUG
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static T CheckNotNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL), NoEnumeration, System.Diagnostics.CodeAnalysis.NotNull] T actualValue)
        where T : notnull
    {
      // ReSharper disable CompareNonConstrainedGenericWithNull
      if (actualValue == null)
          // ReSharper restore CompareNonConstrainedGenericWithNull
        throw new ArgumentNullException(argumentName);

      return actualValue;
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] T actualValue)
        where T : notnull
    {
      CheckNotNull(argumentName, actualValue);
    }

    [AssertionMethod]
#if !DEBUG
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static string CheckNotNullOrEmpty (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL), System.Diagnostics.CodeAnalysis.NotNull] string actualValue)
    {
      if (actualValue == null)
        throw new ArgumentNullException(argumentName);

      if (actualValue.Length == 0)
        throw CreateArgumentEmptyException(argumentName);

      return actualValue;
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNullOrEmpty (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string actualValue)
    {
      CheckNotNullOrEmpty(argumentName, actualValue);
    }

    [AssertionMethod]
    public static T CheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T collection)
        where T: ICollection
    {
      CheckNotNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);

      return collection;
    }

    [AssertionMethod]
    public static void CheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> collection)
    {
      CheckNotNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);
    }

    [AssertionMethod]
    public static void CheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IReadOnlyCollection<T> collection)
    {
      CheckNotNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T collection)
        where T: ICollection
    {
      CheckNotNullOrEmpty(argumentName, collection);
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> collection)
    {
      CheckNotNullOrEmpty(argumentName, collection);
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNullOrEmpty<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IReadOnlyCollection<T> collection)
    {
      CheckNotNullOrEmpty(argumentName, collection);
    }

    [AssertionMethod]
    public static T CheckNotNullOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T collection)
        where T: ICollection
    {
      CheckNotNullOrItemsNullImplementation(argumentName, collection);

      return collection;
    }

    [AssertionMethod]
    public static void CheckNotNullOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> collection)
    {
      CheckNotNullOrItemsNullImplementation(argumentName, collection);
    }

    [AssertionMethod]
    public static void CheckNotNullOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IReadOnlyCollection<T> collection)
    {
      CheckNotNullOrItemsNullImplementation(argumentName, collection);
    }

    private static void CheckNotNullOrItemsNullImplementation (string argumentName, IEnumerable enumerable)
    {
      CheckNotNull(argumentName, enumerable);

      int i = 0;
      foreach (object item in enumerable)
      {
        if (item == null)
          throw CreateArgumentItemNullException(argumentName, i);
        ++i;
      }
    }

    [AssertionMethod]
    public static T CheckNotNullOrEmptyOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T collection)
        where T: ICollection
    {
      CheckNotNullOrItemsNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);

      return collection;
    }

    [AssertionMethod]
    public static void CheckNotNullOrEmptyOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection<T> collection)
    {
      CheckNotNullOrItemsNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);
    }

    [AssertionMethod]
    public static void CheckNotNullOrEmptyOrItemsNull<T> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IReadOnlyCollection<T> collection)
    {
      CheckNotNullOrItemsNull(argumentName, collection);
      CheckNotEmpty(argumentName, collection);
    }

    [AssertionMethod]
#if !DEBUG
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    [return: NotNullIfNotNull("actualValue")]
    public static string? CheckNotEmpty ([InvokerParameterName] string argumentName, string? actualValue)
    {
      if (actualValue != null && actualValue.Length == 0)
        throw CreateArgumentEmptyException(argumentName);

      return actualValue;
    }

    [AssertionMethod]
    [return: NotNullIfNotNull("collection")]
    public static T? CheckNotEmpty<T> ([InvokerParameterName] string argumentName, T collection)
        where T: ICollection?
    {
      if (collection != null && collection.Count == 0)
        throw CreateArgumentEmptyException(argumentName);

      return collection;
    }

    [AssertionMethod]
    [return: NotNullIfNotNull("collection")]
    public static void CheckNotEmpty<T> ([InvokerParameterName] string argumentName, ICollection<T>? collection)
    {
      if (collection != null && collection.Count == 0)
        throw CreateArgumentEmptyException(argumentName);
    }

    [AssertionMethod]
    [return: NotNullIfNotNull("collection")]
    public static void CheckNotEmpty<T> ([InvokerParameterName] string argumentName, IReadOnlyCollection<T>? collection)
    {
      if (collection != null && collection.Count == 0)
        throw CreateArgumentEmptyException(argumentName);
    }

    [AssertionMethod]
    public static Guid CheckNotEmpty ([InvokerParameterName] string argumentName, Guid actualValue)
    {
      if (actualValue == Guid.Empty)
        throw CreateArgumentEmptyException(argumentName);

      return actualValue;
    }

    public static object CheckNotNullAndType (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] object actualValue,
        Type expectedType)
    {
      if (actualValue == null)
        throw new ArgumentNullException(argumentName);

      // ReSharper disable UseMethodIsInstanceOfType
      if (!expectedType.GetTypeInfo().IsAssignableFrom(actualValue.GetType().GetTypeInfo()))
        throw CreateArgumentTypeException(argumentName, actualValue.GetType(), expectedType);
      // ReSharper restore UseMethodIsInstanceOfType

      return actualValue;
    }

    ///// <summary>Returns the value itself if it is not <see langword="null"/> and of the specified value type.</summary>
    ///// <typeparam name="TExpected"> The type that <paramref name="actualValue"/> must have. </typeparam>
    ///// <exception cref="ArgumentNullException"> <paramref name="actualValue"/> is <see langword="null"/>.</exception>
    ///// <exception cref="ArgumentException"> <paramref name="actualValue"/> is an instance of another type (which is not a subclass of <typeparamref name="TExpected"/>).</exception>
    //public static TExpected CheckNotNullAndType<TExpected> (string argumentName, object actualValue)
    //  where TExpected: class
    //{
    //  if (actualValue == null)
    //    throw new ArgumentNullException (argumentName);
    //  return CheckType<TExpected> (argumentName, actualValue);
    //}

    /// <summary>Returns the value itself if it is not <see langword="null"/> and of the specified value type.</summary>
    /// <typeparam name="TExpected"> The type that <paramref name="actualValue"/> must have. </typeparam>
    /// <exception cref="ArgumentNullException">The <paramref name="actualValue"/> is a <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="actualValue"/> is an instance of another type.</exception>
    public static TExpected CheckNotNullAndType<TExpected> (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] [System.Diagnostics.CodeAnalysis.NotNull] object actualValue)
        where TExpected : notnull
    {
      if (actualValue == null)
        throw new ArgumentNullException(argumentName);

      if (! (actualValue is TExpected))
        throw CreateArgumentTypeException(argumentName, actualValue.GetType(), typeof(TExpected));
      return (TExpected)actualValue;
    }

    /// <summary>Checks of the <paramref name="actualValue"/> is of the <paramref name="expectedType"/>.</summary>
    /// <exception cref="ArgumentNullException">The <paramref name="actualValue"/> is a <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="actualValue"/> is an instance of another type.</exception>
    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckNotNullAndType (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] object actualValue,
        Type expectedType)
    {
      CheckNotNullAndType(argumentName, actualValue, expectedType);
    }

    [return: NotNullIfNotNull("actualValue")]
    public static object? CheckType ([InvokerParameterName] string argumentName, [NoEnumeration] object? actualValue, Type expectedType)
    {
      if (actualValue == null)
      {
        if (NullableTypeUtility.IsNullableType_NoArgumentCheck(expectedType))
          return null;
        else
          throw CreateArgumentTypeException(argumentName, null, expectedType);
      }

      // ReSharper disable UseMethodIsInstanceOfType
      if (!expectedType.GetTypeInfo().IsAssignableFrom(actualValue.GetType().GetTypeInfo()))
        throw CreateArgumentTypeException(argumentName, actualValue.GetType(), expectedType);
      // ReSharper restore UseMethodIsInstanceOfType

      return actualValue;
    }

    /// <summary>Returns the value itself if it is of the specified type.</summary>
    /// <typeparam name="TExpected"> The type that <paramref name="actualValue"/> must have. </typeparam>
    /// <exception cref="ArgumentException"> 
    ///     <paramref name="actualValue"/> is an instance of another type (which is not a subtype of <typeparamref name="TExpected"/>).</exception>
    /// <exception cref="ArgumentNullException"> 
    ///     <paramref name="actualValue" /> is null and <typeparamref name="TExpected"/> cannot be null. </exception>
    /// <remarks>
    ///   For non-nullable value types, you should use either <see cref="CheckNotNullAndType{TExpected}"/> or pass the type 
    ///   <see cref="Nullable{T}" /> instead.
    /// </remarks>
    [return: NotNullIfNotNull("actualValue")]
    public static TExpected CheckType<TExpected> ([InvokerParameterName] string argumentName, [NoEnumeration] object? actualValue)
    {
      if (actualValue == null)
      {
        try
        {
          return (TExpected)actualValue!;
        }
        catch (NullReferenceException)
        {
          throw new ArgumentNullException(argumentName);
        }
      }

      if (!(actualValue is TExpected))
        throw CreateArgumentTypeException(argumentName, actualValue.GetType(), typeof(TExpected));

      return (TExpected)actualValue;
    }


    /// <summary>Checks whether <paramref name="actualType"/> is not <see langword="null"/> and can be assigned to <paramref name="expectedType"/>.</summary>
    /// <exception cref="ArgumentNullException">The <paramref name="actualType"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="actualType"/> cannot be assigned to <paramref name="expectedType"/>.</exception>
    public static Type CheckNotNullAndTypeIsAssignableFrom (
        [InvokerParameterName] string argumentName,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] Type actualType,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] Type expectedType)
    {
      if (actualType == null)
        throw new ArgumentNullException(argumentName);
      return CheckTypeIsAssignableFrom(argumentName, actualType, expectedType);
    }

    /// <summary>Checks whether <paramref name="actualType"/> can be assigned to <paramref name="expectedType"/>.</summary>
    /// <exception cref="ArgumentException">The <paramref name="actualType"/> cannot be assigned to <paramref name="expectedType"/>.</exception>
    [return: NotNullIfNotNull("actualType")]
    public static Type? CheckTypeIsAssignableFrom (
        [InvokerParameterName] string argumentName,
        Type? actualType,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] Type expectedType)
    {
      CheckNotNull("expectedType", expectedType);
      if (actualType != null)
      {
        if (!expectedType.GetTypeInfo().IsAssignableFrom(actualType.GetTypeInfo()))
        {
          string message = string.Format(
              "Parameter '{0}' is a '{2}', which cannot be assigned to type '{1}'.",
              argumentName,
              expectedType,
              actualType);
          throw new ArgumentException(message, argumentName);
        }
      }

      return actualType;
    }

    /// <summary>Checks whether <paramref name="actualType"/> can be assigned to <paramref name="expectedType"/>.</summary>
    /// <exception cref="ArgumentException">The <paramref name="actualType"/> cannot be assigned to <paramref name="expectedType"/>.</exception>
    [Conditional("DEBUG")]
    [AssertionMethod]
    public static void DebugCheckTypeIsAssignableFrom (
        [InvokerParameterName] string argumentName,
        Type? actualType,
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] Type expectedType)
    {
      CheckTypeIsAssignableFrom(argumentName, actualType, expectedType);
    }

    /// <summary>Checks whether all items in <paramref name="collection"/> are of type <paramref name="itemType"/> or a null reference.</summary>
    /// <exception cref="ArgumentException"> If at least one element is not of the specified type or a derived type. </exception>
    [return: NotNullIfNotNull("collection")]
    public static T? CheckItemsType<T> ([InvokerParameterName] string argumentName, T collection, Type itemType)
        where T: ICollection?
    {
      // ReSharper disable CompareNonConstrainedGenericWithNull
      if (collection != null)
          // ReSharper restore CompareNonConstrainedGenericWithNull
      {
        int index = 0;
        foreach (object item in collection)
        {
          // ReSharper disable UseMethodIsInstanceOfType
          if (item != null && !itemType.GetTypeInfo().IsAssignableFrom(item.GetType().GetTypeInfo()))
            throw CreateArgumentItemTypeException(argumentName, index, itemType, item.GetType());
          // ReSharper restore UseMethodIsInstanceOfType

          ++index;
        }
      }

      return collection;
    }

    /// <summary>Checks whether all items in <paramref name="collection"/> are of type <paramref name="itemType"/> and not null references.</summary>
    /// <exception cref="ArgumentException"> If at least one element is not of the specified type or a derived type. </exception>
    /// <exception cref="ArgumentNullException"> If at least one element is a null reference. </exception>
    [return: NotNullIfNotNull("collection")]
    public static T? CheckItemsNotNullAndType<T> ([InvokerParameterName] string argumentName, T collection, Type itemType)
        where T: ICollection?
    {
      // ReSharper disable CompareNonConstrainedGenericWithNull
      if (collection != null)
          // ReSharper restore CompareNonConstrainedGenericWithNull
      {
        int index = 0;
        foreach (object item in collection)
        {
          if (item == null)
            throw CreateArgumentItemNullException(argumentName, index);
          // ReSharper disable UseMethodIsInstanceOfType
          if (!itemType.GetTypeInfo().IsAssignableFrom(item.GetType().GetTypeInfo()))
            throw CreateArgumentItemTypeException(argumentName, index, itemType, item.GetType());
          // ReSharper restore UseMethodIsInstanceOfType

          ++index;
        }
      }

      return collection;
    }

    [MustUseReturnValue]
    public static ArgumentException CreateArgumentEmptyException ([InvokerParameterName] string argumentName)
    {
      return new ArgumentException(string.Format("Parameter '{0}' cannot be empty.", argumentName), argumentName);
    }

    [MustUseReturnValue]
    public static ArgumentException CreateArgumentTypeException ([InvokerParameterName] string argumentName, Type? actualType, Type? expectedType)
    {
      string actualTypeName = actualType != null ? actualType.ToString() : "<null>";
      if (expectedType == null)
      {
        return new ArgumentException(string.Format("Parameter '{0}' has unexpected type '{1}'.", argumentName, actualTypeName), argumentName);
      }
      else
      {
        return new ArgumentException(
            string.Format("Parameter '{0}' has type '{2}' when type '{1}' was expected.", argumentName, expectedType, actualTypeName),
            argumentName);
      }
    }

    [MustUseReturnValue]
    public static ArgumentException CreateArgumentItemTypeException (
        [InvokerParameterName] string argumentName,
        int index,
        Type expectedType,
        Type actualType)
    {
      return new ArgumentException(
          string.Format(
              "Item {0} of parameter '{1}' has the type '{2}' instead of '{3}'.",
              index,
              argumentName,
              actualType,
              expectedType),
          argumentName);
    }

    [MustUseReturnValue]
    public static ArgumentNullException CreateArgumentItemNullException ([InvokerParameterName] string argumentName, int index)
    {
      return new ArgumentNullException(argumentName, string.Format("Item {0} of parameter '{1}' is null.", index, argumentName));
    }
  }
}
