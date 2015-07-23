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
using Microsoft.Scripting;
using Remotion.Utilities;

namespace Remotion.Scripting
{
#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TResult>> (scriptFunctionName);
    }

    public TResult Execute ()
    {
      return ScriptContext.Execute (() => _func ());
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1)
    {
      return ScriptContext.Execute (() => _func (a1));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2)
    {
      return ScriptContext.Execute (() => _func (a1, a2));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4, TFixedArg5 a5)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4, a5));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4, TFixedArg5 a5, TFixedArg6 a6)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4, a5, a6));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4, TFixedArg5 a5, TFixedArg6 a6, TFixedArg7 a7)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4, a5, a6, a7));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4, TFixedArg5 a5, TFixedArg6 a6, TFixedArg7 a7, TFixedArg8 a8)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4, a5, a6, a7, a8));
    }
  }

#pragma warning disable 1712 // Some template parameters do not have documentation comments
  /// <summary>
  /// Encapsulates a script containing multiple statements and a main function definition. For script execution, this
  /// function is evaluated and the result returned.
  /// </summary>
  /// <typeparam name="TResult">The result type of the function definition.</typeparam>
  /// <remarks>
  /// Under IronPython, <see cref="ScriptFunction{TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TFixedArg9, TResult}"/> scripts are less safe than <see cref="ExpressionScript{TResult}"/>-based scripts because
  /// they can contain import statements, allowing them to access objects and classes not directly accessible via their <see cref="ScriptEnvironment"/>.
  /// </remarks>
  public class ScriptFunction<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TFixedArg9, TResult> : ScriptBase
#pragma warning restore 1712
  {
    private readonly Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TFixedArg9, TResult> _func;

    /// <summary>
    /// Initializes a new script instance. This immediately executes the given 
    /// <paramref name="scriptText"/>, and stores a delegate to the <paramref name="scriptFunctionName" />, which is run when <see cref="Execute"/>
    /// is called.
    /// </summary>
    /// <param name="scriptContext">
    ///   The <see cref="ScriptContext"/> to use when executing the script. The script context is used to isolate re-motion modules from each other.
    /// </param>
    /// <param name="scriptLanguageType">The script language to use for the script.</param>
    /// <param name="scriptText">
    ///   The source code of the script. This must be source code matching the language defined by <paramref name="scriptLanguageType"/>. The script
    ///   is immediately executed, i.e., all global statements are immediately run. After that, a delegate defining the main function (defined by 
    ///   <paramref name="scriptFunctionName"/>) is stored for later use. When <see cref="Execute"/> is invoked, that function is run and its result
    ///   returned.
    /// </param>
    /// <param name="scriptEnvironment">
    ///   The <see cref="ScriptEnvironment"/> defining the variables and imported symbols the script has access to.
    /// </param>
    /// <param name="scriptFunctionName">
    ///   The name of the script's main function. This function must be defined by <paramref name="scriptText"/>, and it is invoked when the
    ///   script is executed.
    /// </param>
    public ScriptFunction (
        ScriptContext scriptContext, 
        ScriptLanguageType scriptLanguageType, 
        string scriptText, 
        ScriptEnvironment scriptEnvironment, 
        string scriptFunctionName)
      : base (
        ArgumentUtility.CheckNotNull ("scriptContext", scriptContext), 
        scriptLanguageType,
        ArgumentUtility.CheckNotNullOrEmpty ("scriptText", scriptText))
    {
      ArgumentUtility.CheckNotNull ("scriptEnvironment", scriptEnvironment);
      ArgumentUtility.CheckNotNullOrEmpty ("scriptFunctionName", scriptFunctionName);

      var engine = ScriptingHost.GetScriptEngine (scriptLanguageType);
      var scriptSource = engine.CreateScriptSourceFromString (scriptText, SourceCodeKind.Statements);
      
      // Immediately execute the script. This will cause all function definitions to be stored as variables. We can then extract the script function
      // and store it as a delegate.
      scriptSource.Execute (scriptEnvironment.ScriptScope);

      _func = scriptEnvironment.ScriptScope.GetVariable<Func<TFixedArg1, TFixedArg2, TFixedArg3, TFixedArg4, TFixedArg5, TFixedArg6, TFixedArg7, TFixedArg8, TFixedArg9, TResult>> (scriptFunctionName);
    }

    public TResult Execute (TFixedArg1 a1, TFixedArg2 a2, TFixedArg3 a3, TFixedArg4 a4, TFixedArg5 a5, TFixedArg6 a6, TFixedArg7 a7, TFixedArg8 a8, TFixedArg9 a9)
    {
      return ScriptContext.Execute (() => _func (a1, a2, a3, a4, a5, a6, a7, a8, a9));
    }
  }

}