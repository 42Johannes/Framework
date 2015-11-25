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
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OBWTest.ValidatorFactoryDecorators;
using Remotion.Collections;
using Remotion.ObjectBinding;
using Remotion.ObjectBinding.Sample;
using Remotion.ObjectBinding.Web.UI.Controls;
using Remotion.ObjectBinding.Web.Validation.UI.Controls;
using Remotion.ServiceLocation;
using Remotion.Utilities;
using Remotion.Validation;
using Remotion.Web.UI.Controls;

namespace OBWTest.Validation
{
  public class BocValidationTestForm
      : SingleBocTestWxeBasePage,
          IFormGridRowProvider //  Provides new rows and rows to hide to the FormGridManager
  {
    private AutoInitDictionary<HtmlTable, FormGridRowInfoCollection> _listOfFormGridRowInfos =
        new AutoInitDictionary<HtmlTable, FormGridRowInfoCollection>();

    private AutoInitDictionary<HtmlTable, StringCollection> _listOfHiddenRows = new AutoInitDictionary<HtmlTable, StringCollection>();

    protected Button PostBackButton;
    protected Button SaveButton;
    protected BindableObjectDataSourceControl CurrentObject;
    protected BocDataSourceValidator DataSourceValidator;
    protected FormGridManager FormGridManager;
    protected BocTextValue LastNameField;
    protected BocTextValue FirstNameField;
    protected BocTextValue TextField;
    protected BocMultilineTextValue MultilineTextField;
    protected BocDateTimeValue DateTimeField;
    protected BocEnumValue EnumField;
    protected BocReferenceValue ReferenceField;
    protected BocBooleanValue BooleanField;
    protected BocList ListField;
    protected HtmlTable FormGrid;
    protected HtmlHeadContents HtmlHeadContents;
    protected UserControlBinding UserControlPartnerBinding;

    protected void Page_Load (object sender, EventArgs e)
    {
    }

    protected override void OnLoad (EventArgs e)
    {
      base.OnLoad (e);
      CurrentObject.BusinessObject = (IBusinessObject) ((BocValidationTestWxeFunction)CurrentFunction).Person;
      CurrentObject.LoadValues (IsPostBack);

      if (!IsPostBack)
      {
        IBusinessObjectWithIdentity[] objects = (IBusinessObjectWithIdentity[]) ArrayUtility.Convert (
            XmlReflectionBusinessObjectStorageProvider.Current.GetObjects (typeof (Person)),
            typeof (IBusinessObjectWithIdentity));
        ReferenceField.SetBusinessObjectList (objects);
      }
    }

    protected override void OnInit (EventArgs e)
    {
      // SwitchingValidatorFactoryState.Instance.UseFilteringFactory = true;
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit (e);
    }

    #region Web Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ()
    {
      this.SaveButton.Click += new System.EventHandler (this.SaveButton_Click);
      this.Load += new System.EventHandler (this.Page_Load);
    }

    #endregion

    private void SaveButton_Click (object sender, EventArgs e)
    {
      
      PrepareValidation ();
      //FormGridManager.Validate ();
      if (CurrentObject.SaveValues (false))
      {
        var person = (Person) CurrentObject.BusinessObject;
        var validationResult = ValidationBuilder.BuildValidator (typeof (Person)).Validate (person);
        var validationResultPartner = ValidationBuilder.BuildValidator (typeof (Person)).Validate (person.Partner);
        var errors = validationResult.Errors.Concat (validationResultPartner.Errors);

        if (validationResult.IsValid && validationResultPartner.IsValid)
        {
          person.SaveObject ();
        }
        else
        {
          DataSourceValidator.ApplyValidationFailures (errors);
          DataSourceValidator.Validate();
        }
      }

    }

    public virtual StringCollection GetHiddenRows (HtmlTable table)
    {
      return (StringCollection) _listOfHiddenRows[table];
    }

    public virtual FormGridRowInfoCollection GetAdditionalRows (HtmlTable table)
    {
      return (FormGridRowInfoCollection) _listOfFormGridRowInfos[table];
    }

    public IValidatorBuilder ValidationBuilder
    {
      get
      {
        return SafeServiceLocator.Current.GetInstance<IValidatorBuilder>();
      }
    }
  }
}