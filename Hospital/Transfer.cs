//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transfer
    {
        public int id { get; set; }
        public System.DateTime transferDate { get; set; }
        public int wardId { get; set; }
        public int medicalCardId { get; set; }
    
        public virtual MedicalCard MedicalCard { get; set; }
        public virtual Ward Ward { get; set; }
    }
}
