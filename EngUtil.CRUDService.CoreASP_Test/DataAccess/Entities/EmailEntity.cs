// --------------------------------------------------------------------------------
// <copyright filename="EmailEntity.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities
{
    public class EmailEntity : EntityBase
    {
        public string EMailAddress { get; set; }

        public Guid PersonId { get; set; }

        public PersonEntity Person { get; set; }
    }
}
