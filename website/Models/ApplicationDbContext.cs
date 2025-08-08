using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace website.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cartitem> Cartitems { get; set; }

   

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerorder> Customerorders { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

 

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

 

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Prescriptiondetail> Prescriptiondetails { get; set; }

 
    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Purchasedetail> Purchasedetails { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Saledetail> Saledetails { get; set; }

 

    public virtual DbSet<Supplier> Suppliers { get; set; }
 

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=C##MHMD;Password=MHMD;Data Source=localhost:1521/orcl");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MHMD")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Cartid).HasName("SYS_C007504");

            entity.ToTable("CART");

            entity.Property(e => e.Cartid)
                .HasColumnType("NUMBER")
                .HasColumnName("CARTID");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("FK_CART_CUSTOMER");
        });

        modelBuilder.Entity<Cartitem>(entity =>
        {
            entity.HasKey(e => e.Cartitemid).HasName("SYS_C007506");

            entity.ToTable("CARTITEM");

            entity.Property(e => e.Cartitemid)
                .HasColumnType("NUMBER")
                .HasColumnName("CARTITEMID");
            entity.Property(e => e.Cartid)
                .HasColumnType("NUMBER")
                .HasColumnName("CARTID");
            entity.Property(e => e.Medicineid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Cart).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Cartid)
                .HasConstraintName("FK_CARTITEM_CART");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Medicineid)
                .HasConstraintName("FK_CARTITEM_MEDICINE");
        });
 

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("SYS_C007482");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("DATE")
                .HasColumnName("DATEOFBIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Customerorder>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("SYS_C007509");

            entity.ToTable("CUSTOMERORDER");

            entity.Property(e => e.Orderid)
                .HasColumnType("NUMBER")
                .HasColumnName("ORDERID");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ORDERDATE");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TOTALAMOUNT");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerorders)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("FK_ORDER_CUSTOMER");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Medicineid).HasName("SYS_C007483");

            entity.ToTable("MEDICINE");

            entity.Property(e => e.Medicineid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Discount)
                .HasColumnType("NUMBER(4,2)")
                .HasColumnName("DISCOUNT");
            entity.Property(e => e.Expirydate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRYDATE");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MANUFACTURER");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Stockquantity)
                .HasColumnType("NUMBER")
                .HasColumnName("STOCKQUANTITY");
        });

         

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Orderdetailid).HasName("SYS_C007511");

            entity.ToTable("ORDERDETAILS");

            entity.Property(e => e.Orderdetailid)
                .HasColumnType("NUMBER")
                .HasColumnName("ORDERDETAILID");
            entity.Property(e => e.Medicineid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Orderid)
                .HasColumnType("NUMBER")
                .HasColumnName("ORDERID");
            entity.Property(e => e.Priceperunit)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICEPERUNIT");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Medicineid)
                .HasConstraintName("FK_ORDERDETAILS_MEDICINE");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("FK_ORDERDETAILS_ORDER");
        });

         

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Prescriptionid).HasName("SYS_C007484");

            entity.ToTable("PRESCRIPTION");

            entity.Property(e => e.Prescriptionid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRESCRIPTIONID");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Dateissued)
                .HasColumnType("DATE")
                .HasColumnName("DATEISSUED");
            entity.Property(e => e.Doctorname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOCTORNAME");
            entity.Property(e => e.Validuntil)
                .HasColumnType("DATE")
                .HasColumnName("VALIDUNTIL");

            entity.HasOne(d => d.Customer).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("FK_PRESCRIPTION_CUSTOMER");
        });

        modelBuilder.Entity<Prescriptiondetail>(entity =>
        {
            entity.HasKey(e => e.Prescriptiondetailid).HasName("SYS_C007486");

            entity.ToTable("PRESCRIPTIONDETAILS");

            entity.Property(e => e.Prescriptiondetailid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRESCRIPTIONDETAILID");
            entity.Property(e => e.Dosage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOSAGE");
            entity.Property(e => e.Medicineid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Prescriptionid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRESCRIPTIONID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Prescriptiondetails)
                .HasForeignKey(d => d.Medicineid)
                .HasConstraintName("FK_PRESCRIPTIONDETAILS_MEDICINE");

            entity.HasOne(d => d.Prescription).WithMany(p => p.Prescriptiondetails)
                .HasForeignKey(d => d.Prescriptionid)
                .HasConstraintName("FK_PRESCRIPTIONDETAILS_PRESCRIPTION");
        });

    

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Purchaseid).HasName("SYS_C007490");

            entity.ToTable("PURCHASE");

            entity.Property(e => e.Purchaseid)
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEID");
            entity.Property(e => e.Purchasedate)
                .HasColumnType("DATE")
                .HasColumnName("PURCHASEDATE");
            entity.Property(e => e.Supplierid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUPPLIERID");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TOTALAMOUNT");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("FK_PURCHASE_SUPPLIER");
        });

        modelBuilder.Entity<Purchasedetail>(entity =>
        {
            entity.HasKey(e => e.Purchasedetailid).HasName("SYS_C007492");

            entity.ToTable("PURCHASEDETAILS");

            entity.Property(e => e.Purchasedetailid)
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEDETAILID");
            entity.Property(e => e.Medicineid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Purchaseid)
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.Unitprice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("UNITPRICE");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Purchasedetails)
                .HasForeignKey(d => d.Medicineid)
                .HasConstraintName("FK_PURCHASEDETAILS_MEDICINE");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Purchasedetails)
                .HasForeignKey(d => d.Purchaseid)
                .HasConstraintName("FK_PURCHASEDETAILS_PURCHASE");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Saleid).HasName("SYS_C007495");

            entity.ToTable("SALE");

            entity.Property(e => e.Saleid)
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Saledate)
                .HasColumnType("DATE")
                .HasColumnName("SALEDATE");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TOTALAMOUNT");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("FK_SALE_CUSTOMER");
        });

        modelBuilder.Entity<Saledetail>(entity =>
        {
            entity.HasKey(e => e.Saledetailid).HasName("SYS_C007497");

            entity.ToTable("SALEDETAILS");

            entity.Property(e => e.Saledetailid)
                .HasColumnType("NUMBER")
                .HasColumnName("SALEDETAILID");
            entity.Property(e => e.Medicineid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICINEID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.Saleid)
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.Unitprice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("UNITPRICE");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Saledetails)
                .HasForeignKey(d => d.Medicineid)
                .HasConstraintName("FK_SALEDETAILS_MEDICINE");

            entity.HasOne(d => d.Sale).WithMany(p => p.Saledetails)
                .HasForeignKey(d => d.Saleid)
                .HasConstraintName("FK_SALEDETAILS_SALE");
        });

        

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("SYS_C007489");

            entity.ToTable("SUPPLIER");

            entity.Property(e => e.Supplierid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUPPLIERID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Contactperson)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTACTPERSON");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Suppliername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUPPLIERNAME");
        });

      

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C007502");

            entity.ToTable("USERACCOUNT");

            entity.HasIndex(e => e.Username, "SYS_C007503").IsUnique();

            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORDHASH");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });
        modelBuilder.HasSequence("courses_id_seq");
        modelBuilder.HasSequence("COURSES_ID_SEQ");
        modelBuilder.HasSequence("MEDICINE_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
