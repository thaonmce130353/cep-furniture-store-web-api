﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.ReceiveEndPoint.Masstransit.StateMachine.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "OrderState",
            //    columns: table => new
            //    {
            //        CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CurrentState = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderState", x => x.CorrelationId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "OrderState");
        }
    }
}
