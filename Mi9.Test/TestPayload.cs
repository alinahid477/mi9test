using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mi9.Lib.Models;
using System.Collections.Generic;

namespace Mi9.Test
{
    [TestClass]
    public class TestPayload
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }


        private PayloadModel payload = null;
        private string payloadSource = null;
        [TestInitialize]
        public void Init()
        {
            payload = new PayloadModel();
            /*string filePath = @"sample_request.json";
            Mi9.Lib.Services.IOService service = new Lib.Services.IOService();
            this.payloadSource = service.Read(filePath);*/
            this.TestRead();
        }

        [TestMethod]
        public void TestRead()
        {
            string filePath = @"sample_request.json";
            Mi9.Lib.Services.IOService service = new Lib.Services.IOService();
            payloadSource = service.Read(filePath);
            Assert.IsNotNull(payloadSource);
        }

        [TestMethod]
        public void TestPayloadRead()
        {
            payload.Read(payloadSource);
            Assert.IsNotNull(payload.InputPayload);
            Assert.IsTrue(payload.InputPayload.Payload.Count > 0);
        }

        [TestMethod]
        public void TestPayloadWrite()
        {
            this.TestPayloadRead();
            string output = payload.Write();
            Assert.IsFalse(string.IsNullOrEmpty(output));
        }
    }
}
