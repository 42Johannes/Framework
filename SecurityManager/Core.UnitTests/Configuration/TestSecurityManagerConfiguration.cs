// This file is part of re-strict (www.re-motion.org)
// Copyright (c) rubicon IT GmbH, www.rubicon.eu
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License version 3.0 
// as published by the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program; if not, see http://www.gnu.org/licenses.
// 
// Additional permissions are listed in the file re-motion_exceptions.txt.
// 
using System;
using System.IO;
using System.Xml;
using Remotion.SecurityManager.Configuration;
using Remotion.Utilities;

namespace Remotion.SecurityManager.UnitTests.Configuration
{
  internal class TestSecurityManagerConfiguration : SecurityManagerConfiguration
  {
    public void DeserializeSection (string xmlFragment)
    {
      ArgumentUtility.CheckNotNullOrEmpty("xmlFragment", xmlFragment);

      XmlDocument document = new XmlDocument();
      document.LoadXml(xmlFragment);

      using (MemoryStream stream = new MemoryStream())
      {
        document.Save(stream);
        stream.Position = 0;
        using (XmlReader reader = XmlReader.Create(stream))
        {
          DeserializeSection(reader);
        }
      }
    }
  }
}
