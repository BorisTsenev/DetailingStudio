using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DetailingStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailingOrders_Vehicles_VehicleId",
                table: "DetailingOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrices_Services_ServiceId",
                table: "ServicePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrices_VehicleCategories_VehicleCategoryId",
                table: "ServicePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ServicePrices_ServiceId",
                table: "ServicePrices");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServicePrices",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DetailingOrderServices",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrices_ServiceId_VehicleCategoryId",
                table: "ServicePrices",
                columns: new[] { "ServiceId", "VehicleCategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailingOrderServices_ServiceId",
                table: "DetailingOrderServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailingOrders_Vehicles_VehicleId",
                table: "DetailingOrders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailingOrderServices_DetailingOrders_DetailingOrderId",
                table: "DetailingOrderServices",
                column: "DetailingOrderId",
                principalTable: "DetailingOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailingOrderServices_Services_ServiceId",
                table: "DetailingOrderServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrices_Services_ServiceId",
                table: "ServicePrices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrices_VehicleCategories_VehicleCategoryId",
                table: "ServicePrices",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryId",
                table: "Vehicles",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailingOrders_Vehicles_VehicleId",
                table: "DetailingOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailingOrderServices_DetailingOrders_DetailingOrderId",
                table: "DetailingOrderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailingOrderServices_Services_ServiceId",
                table: "DetailingOrderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrices_Services_ServiceId",
                table: "ServicePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrices_VehicleCategories_VehicleCategoryId",
                table: "ServicePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ServicePrices_ServiceId_VehicleCategoryId",
                table: "ServicePrices");

            migrationBuilder.DropIndex(
                name: "IX_DetailingOrderServices_ServiceId",
                table: "DetailingOrderServices");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Year",
                table: "Vehicles",
                type: "date",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServicePrices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DetailingOrderServices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrices_ServiceId",
                table: "ServicePrices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailingOrders_Vehicles_VehicleId",
                table: "DetailingOrders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrices_Services_ServiceId",
                table: "ServicePrices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrices_VehicleCategories_VehicleCategoryId",
                table: "ServicePrices",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryId",
                table: "Vehicles",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
