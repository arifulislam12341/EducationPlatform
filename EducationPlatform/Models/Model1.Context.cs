﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationPlatform.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EducationPlatformEntities : DbContext
    {
        public EducationPlatformEntities()
            : base("name=EducationPlatformEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Counseling> Counselings { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<Cours> Courses { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
