<<<<<<< HEAD
﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MANAGER",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_employee = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password_manager = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGER", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TYPE_EMPLOYEE",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPE_EMPLOYEE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MUNICIPALITY",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    municipality = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPALITY", x => x.id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_MUNICIPALITY",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_type_employee = table.Column<int>(type: "int", nullable: true),
                    id_manager = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.id);
                    table.ForeignKey(
                        name: "FK_MANAGER_EMPLOYEE",
                        column: x => x.id_manager,
                        principalTable: "MANAGER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TYPE_EMPLOYEE_EMPLOYEE",
                        column: x => x.id_type_employee,
                        principalTable: "TYPE_EMPLOYEE",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CABIN",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    manager_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    addres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true),
                    id_municipality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CABIN", x => x.id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_CABIN",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MUNICIPALITY_CABIN",
                        column: x => x.id_municipality,
                        principalTable: "MUNICIPALITY",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CITIZEN",
                columns: table => new
                {
                    dui = table.Column<int>(type: "int", nullable: false),
                    fullname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    addres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true),
                    id_municipality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CITIZEN__D876F1BEE69D5D5F", x => x.dui);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_CITIZEN",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MUNICIPALITY_CITIZEN",
                        column: x => x.id_municipality,
                        principalTable: "MUNICIPALITY",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MANAGERXCABIN",
                columns: table => new
                {
                    id_manager = table.Column<int>(type: "int", nullable: false),
                    id_cabin = table.Column<int>(type: "int", nullable: false),
                    date_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGERXCABIN", x => new { x.id_manager, x.id_cabin });
                    table.ForeignKey(
                        name: "FK_CABIN_MANAGERXCABIN",
                        column: x => x.id_cabin,
                        principalTable: "CABIN",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MANAGER_MANAGERXCABIN",
                        column: x => x.id_manager,
                        principalTable: "MANAGER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENT",
                columns: table => new
                {
                    hour_appointment = table.Column<TimeSpan>(type: "time", nullable: true),
                    date_appointment = table.Column<DateTime>(type: "date", nullable: true),
                    id_cabin = table.Column<int>(type: "int", nullable: true),
                    id_citizen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CABIN_APPOINTMENT",
                        column: x => x.id_cabin,
                        principalTable: "CABIN",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CITIZEN_APPOINTMENT",
                        column: x => x.id_citizen,
                        principalTable: "CITIZEN",
                        principalColumn: "dui",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DISEASES",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseases = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dui_citizen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISEASES", x => x.id);
                    table.ForeignKey(
                        name: "FK_CITIZEN_DISEASES",
                        column: x => x.dui_citizen,
                        principalTable: "CITIZEN",
                        principalColumn: "dui",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_id_cabin",
                table: "APPOINTMENT",
                column: "id_cabin");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_id_citizen",
                table: "APPOINTMENT",
                column: "id_citizen");

            migrationBuilder.CreateIndex(
                name: "IX_CABIN_id_department",
                table: "CABIN",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_CABIN_id_municipality",
                table: "CABIN",
                column: "id_municipality");

            migrationBuilder.CreateIndex(
                name: "IX_CITIZEN_id_department",
                table: "CITIZEN",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_CITIZEN_id_municipality",
                table: "CITIZEN",
                column: "id_municipality");

            migrationBuilder.CreateIndex(
                name: "IX_DISEASES_dui_citizen",
                table: "DISEASES",
                column: "dui_citizen");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_id_manager",
                table: "EMPLOYEE",
                column: "id_manager");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_id_type_employee",
                table: "EMPLOYEE",
                column: "id_type_employee");

            migrationBuilder.CreateIndex(
                name: "IX_MANAGERXCABIN_id_cabin",
                table: "MANAGERXCABIN",
                column: "id_cabin");

            migrationBuilder.CreateIndex(
                name: "IX_MUNICIPALITY_id_department",
                table: "MUNICIPALITY",
                column: "id_department");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "DISEASES");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "MANAGERXCABIN");

            migrationBuilder.DropTable(
                name: "CITIZEN");

            migrationBuilder.DropTable(
                name: "TYPE_EMPLOYEE");

            migrationBuilder.DropTable(
                name: "CABIN");

            migrationBuilder.DropTable(
                name: "MANAGER");

            migrationBuilder.DropTable(
                name: "MUNICIPALITY");

            migrationBuilder.DropTable(
                name: "DEPARTMENT");
        }
    }
}
=======
﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MANAGER",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_employee = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password_manager = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGER", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TYPE_EMPLOYEE",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPE_EMPLOYEE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MUNICIPALITY",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    municipality = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPALITY", x => x.id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_MUNICIPALITY",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_type_employee = table.Column<int>(type: "int", nullable: true),
                    id_manager = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.id);
                    table.ForeignKey(
                        name: "FK_MANAGER_EMPLOYEE",
                        column: x => x.id_manager,
                        principalTable: "MANAGER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TYPE_EMPLOYEE_EMPLOYEE",
                        column: x => x.id_type_employee,
                        principalTable: "TYPE_EMPLOYEE",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CABIN",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    manager_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    addres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true),
                    id_municipality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CABIN", x => x.id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_CABIN",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MUNICIPALITY_CABIN",
                        column: x => x.id_municipality,
                        principalTable: "MUNICIPALITY",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CITIZEN",
                columns: table => new
                {
                    dui = table.Column<int>(type: "int", nullable: false),
                    fullname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    addres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_department = table.Column<int>(type: "int", nullable: true),
                    id_municipality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CITIZEN__D876F1BEE69D5D5F", x => x.dui);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_CITIZEN",
                        column: x => x.id_department,
                        principalTable: "DEPARTMENT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MUNICIPALITY_CITIZEN",
                        column: x => x.id_municipality,
                        principalTable: "MUNICIPALITY",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MANAGERXCABIN",
                columns: table => new
                {
                    id_manager = table.Column<int>(type: "int", nullable: false),
                    id_cabin = table.Column<int>(type: "int", nullable: false),
                    date_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGERXCABIN", x => new { x.id_manager, x.id_cabin });
                    table.ForeignKey(
                        name: "FK_CABIN_MANAGERXCABIN",
                        column: x => x.id_cabin,
                        principalTable: "CABIN",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MANAGER_MANAGERXCABIN",
                        column: x => x.id_manager,
                        principalTable: "MANAGER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENT",
                columns: table => new
                {
                    hour_appointment = table.Column<TimeSpan>(type: "time", nullable: true),
                    date_appointment = table.Column<DateTime>(type: "date", nullable: true),
                    id_cabin = table.Column<int>(type: "int", nullable: true),
                    id_citizen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CABIN_APPOINTMENT",
                        column: x => x.id_cabin,
                        principalTable: "CABIN",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CITIZEN_APPOINTMENT",
                        column: x => x.id_citizen,
                        principalTable: "CITIZEN",
                        principalColumn: "dui",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DISEASES",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseases = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dui_citizen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISEASES", x => x.id);
                    table.ForeignKey(
                        name: "FK_CITIZEN_DISEASES",
                        column: x => x.dui_citizen,
                        principalTable: "CITIZEN",
                        principalColumn: "dui",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_id_cabin",
                table: "APPOINTMENT",
                column: "id_cabin");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_id_citizen",
                table: "APPOINTMENT",
                column: "id_citizen");

            migrationBuilder.CreateIndex(
                name: "IX_CABIN_id_department",
                table: "CABIN",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_CABIN_id_municipality",
                table: "CABIN",
                column: "id_municipality");

            migrationBuilder.CreateIndex(
                name: "IX_CITIZEN_id_department",
                table: "CITIZEN",
                column: "id_department");

            migrationBuilder.CreateIndex(
                name: "IX_CITIZEN_id_municipality",
                table: "CITIZEN",
                column: "id_municipality");

            migrationBuilder.CreateIndex(
                name: "IX_DISEASES_dui_citizen",
                table: "DISEASES",
                column: "dui_citizen");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_id_manager",
                table: "EMPLOYEE",
                column: "id_manager");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_id_type_employee",
                table: "EMPLOYEE",
                column: "id_type_employee");

            migrationBuilder.CreateIndex(
                name: "IX_MANAGERXCABIN_id_cabin",
                table: "MANAGERXCABIN",
                column: "id_cabin");

            migrationBuilder.CreateIndex(
                name: "IX_MUNICIPALITY_id_department",
                table: "MUNICIPALITY",
                column: "id_department");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "DISEASES");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "MANAGERXCABIN");

            migrationBuilder.DropTable(
                name: "CITIZEN");

            migrationBuilder.DropTable(
                name: "TYPE_EMPLOYEE");

            migrationBuilder.DropTable(
                name: "CABIN");

            migrationBuilder.DropTable(
                name: "MANAGER");

            migrationBuilder.DropTable(
                name: "MUNICIPALITY");

            migrationBuilder.DropTable(
                name: "DEPARTMENT");
        }
    }
}
>>>>>>> e196b46f1e90476656035405f5aaf321c39d98dc
