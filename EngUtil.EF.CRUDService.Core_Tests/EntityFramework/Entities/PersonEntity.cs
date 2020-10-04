// --------------------------------------------------------------------------------
// <copyright filename="PersonEntity.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities
{
    public class PersonEntity : EntityBase
    {
        public string Name { get; set; }

        public string Surename { get; set; }

        public string StreetAddress { get; set; }

        public string Location { get; set; }

        public string State { get; set; }

        public string ZIPCode { get; set; }

        public DateTime DayOfBirth { get; set; }
        
        public ICollection<PhoneNumberEntity> Numbers { get; set; }

        public ICollection<EmailEntity> EMails { get; set; }
    }
}
