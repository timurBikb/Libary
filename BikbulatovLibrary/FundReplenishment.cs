//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BikbulatovLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class FundReplenishment
    {
        public int ID { get; set; }
        public int FundID { get; set; }
        public int EmployeeID { get; set; }
        public System.DateTime Date { get; set; }
        public string LiteratureSource { get; set; }
        public int LiteratureType { get; set; }
        public string Publisher { get; set; }
        public Nullable<int> PublishDate { get; set; }
        public int CopyNumber { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual LibraryFund LibraryFund { get; set; }
        public virtual LiteratureType LiteratureType1 { get; set; }
    }
}
