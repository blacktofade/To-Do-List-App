﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TODOEntities : DbContext
    {
        public TODOEntities()
            : base("name=TODOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ITEM_INFO> ITEM_INFO { get; set; }
        public virtual DbSet<LIST_INFO> LIST_INFO { get; set; }
        public virtual DbSet<USER_INFO> USER_INFO { get; set; }
    }
}
