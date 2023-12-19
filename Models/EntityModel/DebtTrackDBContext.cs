using System;
using System.Collections.Generic;
using debtTrackingApp.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace debtTrackingApp.Models.EntityModel;

public partial class DebtTrackDBContext : DbContext
{
    public DebtTrackDBContext()
    {
    }

    public DebtTrackDBContext(DbContextOptions<DebtTrackDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Creditor> Creditors { get; set; }

    public virtual DbSet<Debitor> Debitors { get; set; }

    public virtual DbSet<DebtCallDetail> DebtCallDetails { get; set; }

    public virtual DbSet<DebtRecord> DebtRecords { get; set; }

    public virtual DbSet<Guarantor> Guarantors { get; set; }

    public virtual DbSet<ResultCode> ResultCodes { get; set; }
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:constring");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("Agent");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(10);
            entity.Property(e => e.ProjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<Creditor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Creditor__3214EC27FC5681D2");

            entity.ToTable("Creditor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreditorName).HasMaxLength(150);
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.PayorBank).HasMaxLength(150);
            entity.Property(e => e.PayorBankBranch).HasMaxLength(150);
        });

        modelBuilder.Entity<Debitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Debitor__3214EC275E45AD37");

            entity.ToTable("Debitor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppointDate).HasColumnType("datetime");
            entity.Property(e => e.DebitorName).HasMaxLength(40);
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NationalIdnumber)
                .HasMaxLength(11)
                .HasColumnName("NationalIDNumber");
            entity.Property(e => e.TelephoneNum).HasMaxLength(10);
        });

        modelBuilder.Entity<DebtCallDetail>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__DebtCall__5180CF8A2D89BB17");

            entity.ToTable("DebtCallDetail");

            entity.Property(e => e.CallId).HasColumnName("CallID");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.CallDate).HasColumnType("datetime");
            entity.Property(e => e.CallStatus).HasMaxLength(50);
            entity.Property(e => e.CreditorId).HasColumnName("CreditorID");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PromisedPayDate).HasColumnType("datetime");
            entity.Property(e => e.Rcid).HasColumnName("RCID");

            entity.HasOne(d => d.Agent).WithMany(p => p.DebtCallDetails)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_DebtCallDetail_Agent");

            entity.HasOne(d => d.Creditor).WithMany(p => p.DebtCallDetails)
                .HasForeignKey(d => d.CreditorId)
                .HasConstraintName("FK_DebtCallDetail_Creditor");

            entity.HasOne(d => d.Rc).WithMany(p => p.DebtCallDetails)
                .HasForeignKey(d => d.Rcid)
                .HasConstraintName("FK_DebtCallDetail_ResultCode");

            entity.HasMany(d => d.Debts).WithMany(p => p.Calls)
                .UsingEntity<Dictionary<string, object>>(
                    "LkpCalledFile",
                    r => r.HasOne<DebtRecord>().WithMany()
                        .HasForeignKey("DebtId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LkpCalledFile_DebtRecord"),
                    l => l.HasOne<DebtCallDetail>().WithMany()
                        .HasForeignKey("CallId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LkpCalledFile_DebtCallDetail"),
                    j =>
                    {
                        j.HasKey("CallId", "DebtId").HasName("CK_LkpCalledFile");
                        j.ToTable("LkpCalledFile");
                        j.IndexerProperty<int>("CallId").HasColumnName("CallID");
                        j.IndexerProperty<int>("DebtId").HasColumnName("DebtID");
                    });
        });

        modelBuilder.Entity<DebtRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DebtReco__3214EC2734A6CD0B");

            entity.ToTable("DebtRecord");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreditorId).HasColumnName("CreditorID");
            entity.Property(e => e.DebitorId).HasColumnName("DebitorID");
            entity.Property(e => e.DelayDaysCount).HasMaxLength(5);
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.FileNo).HasMaxLength(10);
            entity.Property(e => e.GrossDebt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GuarantorId).HasColumnName("GuarantorID");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastCallId).HasColumnName("LastCallID");
            entity.Property(e => e.ListId)
                .HasMaxLength(20)
                .HasColumnName("ListID");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PaymentStatus).HasMaxLength(30);
            entity.Property(e => e.PayorBank).HasMaxLength(150);
            entity.Property(e => e.PayorBankBranch).HasMaxLength(150);
            entity.Property(e => e.SalesChannel).HasMaxLength(20);
            entity.Property(e => e.UnpaidAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Creditor).WithMany(p => p.DebtRecords)
                .HasForeignKey(d => d.CreditorId)
                .HasConstraintName("FK_DebtRecord_Creditor");

            entity.HasOne(d => d.Debitor).WithMany(p => p.DebtRecords)
                .HasForeignKey(d => d.DebitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DebtRecord_Debitor");

            entity.HasOne(d => d.Guarantor).WithMany(p => p.DebtRecords)
                .HasForeignKey(d => d.GuarantorId)
                .HasConstraintName("FK_DebtRecord_Guarantor");

            entity.HasOne(d => d.LastCall).WithMany(p => p.DebtRecords)
                .HasForeignKey(d => d.LastCallId)
                .HasConstraintName("FK_DebtRecord_DebtCallDetail");
        });

        modelBuilder.Entity<Guarantor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guaranto__3214EC27C89050A8");

            entity.ToTable("Guarantor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GuarantorName).HasMaxLength(40);
            entity.Property(e => e.GuarantorPhoneNum).HasMaxLength(10);
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ResultCode>(entity =>
        {
            entity.ToTable("ResultCode");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccessStatus).HasMaxLength(30);
            entity.Property(e => e.MainCategory).HasMaxLength(100);
            entity.Property(e => e.SubCategory).HasMaxLength(100);
            entity.Property(e => e.WorkGroup).HasMaxLength(15);
        });               

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
