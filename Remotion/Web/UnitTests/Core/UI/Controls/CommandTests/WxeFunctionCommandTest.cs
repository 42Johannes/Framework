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
using System.Web.UI;
using NUnit.Framework;
using Remotion.Development.Web.UnitTesting.AspNetFramework;
using Remotion.Web.UI.Controls;
using Remotion.Web.UI.Controls.Rendering;
using Remotion.Web.UnitTests.Core.ExecutionEngine.TestFunctions;

namespace Remotion.Web.UnitTests.Core.UI.Controls.CommandTests
{
  [TestFixture]
  public class WxeFunctionCommandTest : BaseTest
  {
    private CommandTestHelper _testHelper;

    [SetUp]
    public virtual void SetUp ()
    {
      _testHelper = new CommandTestHelper();
      HttpContextHelper.SetCurrent(_testHelper.HttpContext);
    }

    [Test]
    public void HasAccess_WithoutSeucrityProvider ()
    {
      Command command = _testHelper.CreateWxeFunctionCommand();

      bool hasAccess = command.HasAccess(null);

      _testHelper.VerifyAll();
      Assert.That(hasAccess, Is.True);
    }

    [Test]
    public void HasAccess_WithAccessGranted ()
    {
      Command command = _testHelper.CreateWxeFunctionCommand(_testHelper.WebSecurityAdapter, _testHelper.WxeSecurityAdapter);
      _testHelper.ExpectWxeSecurityProviderHasStatelessAccess(typeof(TestFunction), true);

      bool hasAccess = command.HasAccess(null);

      _testHelper.VerifyAll();
      Assert.That(hasAccess, Is.True);
    }

    [Test]
    public void HasAccess_WithAccessDenied ()
    {
      Command command = _testHelper.CreateWxeFunctionCommand(_testHelper.WebSecurityAdapter, _testHelper.WxeSecurityAdapter);
      _testHelper.ExpectWxeSecurityProviderHasStatelessAccess(typeof(TestFunction), false);

      bool hasAccess = command.HasAccess(null);

      _testHelper.VerifyAll();
      Assert.That(hasAccess, Is.False);
    }

    [Test]
    public void Render_WithAccessGranted ()
    {
      var command = _testHelper.CreateWxeFunctionCommandAsPartialMock();
      string expectedOnClick = _testHelper.PostBackEvent + _testHelper.OnClick + "return false;";
      _testHelper.ExpectOnceOnHasAccess(command, true);

      command.Object.RenderBegin(_testHelper.HtmlWriter, RenderingFeatures.Default, _testHelper.PostBackEvent, new string[0], _testHelper.OnClick, _testHelper.SecurableObject);

      _testHelper.VerifyAll();
      Assert.IsNotNull(_testHelper.HtmlWriter.Tag, "Missing Tag");
      Assert.AreEqual(HtmlTextWriterTag.A, _testHelper.HtmlWriter.Tag, "Wrong Tag");

      Assert.IsNotNull(_testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Href], "Missing Href");
      Assert.AreEqual("fakeFallbackUrl", _testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Href], "Wrong Href");

      Assert.IsNotNull(_testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Onclick], "Missing OnClick");
      Assert.AreEqual(expectedOnClick, _testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Onclick], "Wrong OnClick");

      Assert.IsNotNull(_testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Title], "Missing Title");
      Assert.AreEqual(_testHelper.ToolTip, _testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Title], "Wrong Title");

      Assert.IsNull(_testHelper.HtmlWriter.Attributes[HtmlTextWriterAttribute.Target], "Has Target");
    }

    [Test]
    public void Render_WithAccessDenied ()
    {
      var command = _testHelper.CreateWxeFunctionCommandAsPartialMock();
      _testHelper.ExpectOnceOnHasAccess(command, false);

      command.Object.RenderBegin(_testHelper.HtmlWriter, RenderingFeatures.Default, _testHelper.PostBackEvent, new string[0], _testHelper.OnClick, _testHelper.SecurableObject);

      _testHelper.VerifyAll();
      Assert.IsNotNull(_testHelper.HtmlWriter.Tag, "Missing Tag");
      Assert.AreEqual(HtmlTextWriterTag.A, _testHelper.HtmlWriter.Tag, "Wrong Tag");
      Assert.AreEqual(0, _testHelper.HtmlWriter.Attributes.Count, "Has Attributes");
    }
  }
}
