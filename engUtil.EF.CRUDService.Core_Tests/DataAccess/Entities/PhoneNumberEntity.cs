// --------------------------------------------------------------------------------
// <copyright filename="PhoneNumberEntity.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;


namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities
{
    public class PhoneNumberEntity : EntityBase
    {
        public string Number { get; set; }

        public NumberType NumberType { get; set; }

        public Guid PersonId { get; set; }

        public PersonEntity Person { get; set; }
    }
}
