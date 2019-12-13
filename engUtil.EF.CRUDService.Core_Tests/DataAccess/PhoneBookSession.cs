// --------------------------------------------------------------------------------
// <copyright filename="PhoneBookSession.cs" date="12-13-2019">
// (c) 2019 All Rights Reserved
// </copyright>
// <author>
// Oliver Engels
// </author>
// --------------------------------------------------------------------------------
using engUtil.CRUDService.Interfaces;

namespace engUtil.EF.CRUDService.Core_Tests.DataAccess
{
    public class PhoneBookSession : ISessionContext<PhoneBookContext>
    {
        private string _connectionString;

        public PhoneBookSession(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PhoneBookContext GetContext()
        {
            return new PhoneBookContext(_connectionString);
        }
    }
}
