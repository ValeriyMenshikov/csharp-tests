﻿using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupDeleteTests : AuthTestBase
    {
        [Test]
        public void GroupDeleteTest()
        {
            int index = 1;
            app.Group.DeleteByIndex(index);
        }
    }
}
