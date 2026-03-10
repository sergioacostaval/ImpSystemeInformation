using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksInvesthink.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorTypes",
                columns: table => new
                {
                    IndicatorTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorTypes", x => x.IndicatorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ticker = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    UserPreferenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultIndicatorTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultPeriod = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.UserPreferenceId);
                    table.ForeignKey(
                        name: "FK_UserPreferences_IndicatorTypes_DefaultIndicatorTypeId",
                        column: x => x.DefaultIndicatorTypeId,
                        principalTable: "IndicatorTypes",
                        principalColumn: "IndicatorTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStocks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStocks", x => new { x.UserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_UserStocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalPrices",
                columns: table => new
                {
                    HistoricalPriceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    HighPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    LowPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Volume = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalPrices", x => x.HistoricalPriceId);
                    table.ForeignKey(
                        name: "FK_HistoricalPrices_UserStocks_UserId_StockId",
                        columns: x => new { x.UserId, x.StockId },
                        principalTable: "UserStocks",
                        principalColumns: new[] { "UserId", "StockId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorInstances",
                columns: table => new
                {
                    IndicatorInstanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Period = table.Column<int>(type: "INTEGER", nullable: false),
                    IndicatorTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorInstances", x => x.IndicatorInstanceId);
                    table.ForeignKey(
                        name: "FK_IndicatorInstances_IndicatorTypes_IndicatorTypeId",
                        column: x => x.IndicatorTypeId,
                        principalTable: "IndicatorTypes",
                        principalColumn: "IndicatorTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorInstances_UserStocks_UserId_StockId",
                        columns: x => new { x.UserId, x.StockId },
                        principalTable: "UserStocks",
                        principalColumns: new[] { "UserId", "StockId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorValues",
                columns: table => new
                {
                    IndicatorValueId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    IndicatorInstanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    HistoricalPriceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorValues", x => x.IndicatorValueId);
                    table.ForeignKey(
                        name: "FK_IndicatorValues_HistoricalPrices_HistoricalPriceId",
                        column: x => x.HistoricalPriceId,
                        principalTable: "HistoricalPrices",
                        principalColumn: "HistoricalPriceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorValues_IndicatorInstances_IndicatorInstanceId",
                        column: x => x.IndicatorInstanceId,
                        principalTable: "IndicatorInstances",
                        principalColumn: "IndicatorInstanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signals",
                columns: table => new
                {
                    SignalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    IndicatorValueId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signals", x => x.SignalId);
                    table.ForeignKey(
                        name: "FK_Signals_IndicatorValues_IndicatorValueId",
                        column: x => x.IndicatorValueId,
                        principalTable: "IndicatorValues",
                        principalColumn: "IndicatorValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalPrices_UserId_StockId",
                table: "HistoricalPrices",
                columns: new[] { "UserId", "StockId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorInstances_IndicatorTypeId",
                table: "IndicatorInstances",
                column: "IndicatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorInstances_UserId_StockId",
                table: "IndicatorInstances",
                columns: new[] { "UserId", "StockId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorValues_HistoricalPriceId",
                table: "IndicatorValues",
                column: "HistoricalPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorValues_IndicatorInstanceId",
                table: "IndicatorValues",
                column: "IndicatorInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Signals_IndicatorValueId",
                table: "Signals",
                column: "IndicatorValueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_DefaultIndicatorTypeId",
                table: "UserPreferences",
                column: "DefaultIndicatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStocks_StockId",
                table: "UserStocks",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Signals");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "IndicatorValues");

            migrationBuilder.DropTable(
                name: "HistoricalPrices");

            migrationBuilder.DropTable(
                name: "IndicatorInstances");

            migrationBuilder.DropTable(
                name: "IndicatorTypes");

            migrationBuilder.DropTable(
                name: "UserStocks");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
