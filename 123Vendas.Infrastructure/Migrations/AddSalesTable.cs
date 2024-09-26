using FluentMigrator;

namespace _123Vendas.Infrastructure.Migrations
{
    [Migration(202409261129)]
    public class AddSalesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Sales")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("SaleNumber").AsInt32().NotNullable()
                .WithColumn("SaleDate").AsDateTime().NotNullable()
                .WithColumn("Customer").AsString(255).NotNullable()
                .WithColumn("TotalValue").AsDecimal().NotNullable()
                .WithColumn("Branch").AsString(255).NotNullable()
                .WithColumn("IsCancelled").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Sales");
        }
    }
}
