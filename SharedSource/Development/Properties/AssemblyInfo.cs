﻿// Copyright (c) rubicon IT GmbH, www.rubicon.eu
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

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Remotion Shared Source Development Library")]
[assembly: AssemblyDescription("Contains source files intended for embedding in unit test projects.")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

[assembly: InternalsVisibleTo ("Remotion.SharedSource.UnitTests")]
[assembly: InternalsVisibleTo (Rhino.Mocks.RhinoMocks.NormalName)]
