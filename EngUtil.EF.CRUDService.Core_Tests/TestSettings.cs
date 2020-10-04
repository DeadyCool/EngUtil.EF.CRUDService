using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public static class TestSettings
    {
        public static string DbLitePath => $"{Path.Combine(Path.GetTempPath(), "AddressBook.sqlite")}";
    }
}
