﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SourceCheckUtil.ExternalConfig;

namespace SourceCheckUtilTests.ExternalConfig
{
    [TestFixture]
    public class PorterExternalConfigTests
    {
        [Test]
        public void LoadConfigWithImport()
        {
            IExternalConfig config = new PorterExternalConfig("..\\..\\..\\Examples\\ConfigWithImport\\main.config");
            ExternalConfigData data = config.LoadDefault();
            const Int32 expectedAttributeCount = 3;
            Assert.AreEqual(expectedAttributeCount, data.Attributes.Count);
            Assert.AreEqual(0, data.FileProcessing.Count);
            CheckAttribute(data.Attributes, "SomeAttribute", new Dictionary<String, String> {{"data", "iddqd"}});
            CheckAttribute(data.Attributes, "OtherAttribute", new Dictionary<String, String> {{"data", "idkfa"}});
            CheckAttribute(data.Attributes, "AnotherAttribute", new Dictionary<String, String> {{"data", "idclip"}});
        }

        private void CheckAttribute(IList<AttributeData> attributes, String name, IDictionary<String, String> expectedData)
        {
            AttributeData attribute = attributes.FirstOrDefault(attr => String.Equals(name, attr.Name));
            Assert.IsNotNull(attribute);
            Assert.AreEqual(expectedData.Count, attribute.Data.Count);
            foreach (KeyValuePair<String, String> entry in expectedData)
            {
                Assert.True(attribute.Data.ContainsKey(entry.Key));
                Assert.AreEqual(entry.Value, attribute.Data[entry.Key]);
            }
        }
    }
}
