//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Master_Remont
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int ID_Employee { get; set; }
        public int Position_ID { get; set; }
        public int Specialization_ID { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public string Middlename { get; set; }
    
        public virtual Positions Positions { get; set; }
        public virtual Specializations Specializations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
