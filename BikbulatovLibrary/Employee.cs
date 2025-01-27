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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.FundReplenishment = new HashSet<FundReplenishment>();
        }
    
        public int EmployeeID { get; set; }
        public string EmployeeSurname { get; set; }
        public int LibraryID { get; set; }
        public string Post { get; set; }
        public int AdmissionYear { get; set; }
        public string Education { get; set; }
        public int Salary { get; set; }
        public string Experience { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePassport { get; set; }
        public string EmployeeINN { get; set; }
        public string EmployeeSNILS { get; set; }
        public Nullable<System.DateTime> EmployeeBirthday { get; set; }
        public string EmployeePhoto { get; set; }
    
        public virtual Library Library { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FundReplenishment> FundReplenishment { get; set; }

        public string EmployeeLibrary
        {
            get
            {
                return Library.LibraryName;
            }
        }
    }
}
