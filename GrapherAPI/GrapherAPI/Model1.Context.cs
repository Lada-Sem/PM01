﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrapherAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Численные_методыEntities : DbContext
    {
        public Численные_методыEntities()
            : base("name=Численные_методыEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Графики> Графики { get; set; }
        public virtual DbSet<Методы> Методы { get; set; }
        public virtual DbSet<Ответы_на_тесты> Ответы_на_тесты { get; set; }
        public virtual DbSet<Примеры> Примеры { get; set; }
        public virtual DbSet<Тесты> Тесты { get; set; }
    }
}
