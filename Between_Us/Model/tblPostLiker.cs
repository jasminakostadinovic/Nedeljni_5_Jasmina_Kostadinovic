//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Between_Us.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPostLiker
    {
        public int PostLikerID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
    
        public virtual tblPost tblPost { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
