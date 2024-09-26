using FluentMigrator;

namespace _123Vendas.Infrastructure.Migrations
{
    [Migration(202409261130)]
    public class AddSaleItemTable : Migration
    {
        public override void Up()
        {
            Create.Table("SaleItem")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Product").AsString().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("UnitPrice").AsDecimal().NotNullable()
                .WithColumn("Discount").AsDecimal().NotNullable()
                .WithColumn("IsCancelled").AsBoolean().NotNullable()
                .WithColumn("SaleId").AsGuid().NotNullable();

            Create.ForeignKey("FK_SaleItem_Sales")
                .FromTable("SaleItem").ForeignColumn("SaleId")
                .ToTable("Sales").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("SaleItem");
        }
    }
}
