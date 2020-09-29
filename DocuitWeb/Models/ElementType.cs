using System;
using System.Collections.Generic;
using DocuitWeb.Models;

namespace DocuItService.Models
{
    public partial class ElementType
    {
        public int CompanyId { get; set; }
        public int ElementTypeId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
    }
}
