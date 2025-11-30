using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDockerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:enum_cash_sessions_status", "open,closed")
                .Annotation("Npgsql:Enum:enum_financial_reports_status", "activo,inactivo,finalizado,proceso")
                .Annotation("Npgsql:Enum:enum_recipes_unit", "g,kg,ml,l,unidades");

            migrationBuilder.CreateTable(
                name: "cash_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    store_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    start_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    end_amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    total_sales = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    total_returns = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cash_sessions_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "churches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    state = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("churches_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dni = table.Column<int>(type: "integer", nullable: false),
                    phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "financial_reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_income = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    total_expenses = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    net_profit = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    observations = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("financial_reports_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("locations_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("modules_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "overheads",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("overheads_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_methods_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plant_production",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("plant_production_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_channels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_channels_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    store_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    observations = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("stores_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ruc = table.Column<long>(type: "bigint", nullable: false),
                    suplier_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contact_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    base_price = table.Column<double>(type: "double precision", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_persons_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    observation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    imagen_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    producible = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "products_category_id_fkey",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "INCOME_CHURCH",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    date = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    id_church = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("INCOME_CHURCH_pkey", x => x.id);
                    table.ForeignKey(
                        name: "INCOME_CHURCH_id_church_fkey",
                        column: x => x.id_church,
                        principalTable: "churches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rent_church",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    startTime = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    endTime = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false, defaultValueSql: "'0'::double precision"),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    idChurch = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rent_church_pkey", x => x.id);
                    table.ForeignKey(
                        name: "rent_church_idChurch_fkey",
                        column: x => x.idChurch,
                        principalTable: "churches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "places",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    area = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    imagen_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("places_pkey", x => x.id);
                    table.ForeignKey(
                        name: "places_location_id_fkey",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "general_expenses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    module_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expense_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    report_id = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("general_expenses_pkey", x => x.id);
                    table.ForeignKey(
                        name: "general_expenses_module_id_fkey",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "general_expenses_report_id_fkey",
                        column: x => x.report_id,
                        principalTable: "financial_reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "general_incomes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    module_id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    report_id = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("general_incomes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "general_incomes_module_id_fkey",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "general_incomes_report_id_fkey",
                        column: x => x.report_id,
                        principalTable: "financial_reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    canRead = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    canWrite = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    canEdit = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    canDelete = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("permissions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "permissions_moduleId_fkey",
                        column: x => x.moduleId,
                        principalTable: "modules",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "monastery_expenses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descripción = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    overheadsId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("monastery_expenses_pkey", x => x.id);
                    table.ForeignKey(
                        name: "monastery_expenses_overheadsId_fkey",
                        column: x => x.overheadsId,
                        principalTable: "overheads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dni = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    roleId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                    table.ForeignKey(
                        name: "users_roleId_fkey",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_date = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    store_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_income = table.Column<double>(type: "double precision", nullable: false),
                    observations = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_pkey", x => x.id);
                    table.ForeignKey(
                        name: "sales_store_id_fkey",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    observation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("resources_pkey", x => x.id);
                    table.ForeignKey(
                        name: "resources_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "buys_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    unit_price = table.Column<double>(type: "double precision", nullable: false),
                    total_cost = table.Column<double>(type: "double precision", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("buys_products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "buys_products_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "buys_products_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "buys_products_warehouse_id_fkey",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "production",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    quantityProduced = table.Column<int>(type: "integer", nullable: false),
                    productionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    observation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    plant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("production_pkey", x => x.id);
                    table.ForeignKey(
                        name: "production_plant_id_fkey",
                        column: x => x.plant_id,
                        principalTable: "plant_production",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "production_productId_fkey",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse_movement_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    store_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movement_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    movement_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    observations = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_movement_products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "warehouse_movement_products_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_movement_products_warehouse_id_fkey",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "warehouse_products_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_products_warehouse_id_fkey",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse_stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    storeId = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_stores_pkey", x => x.id);
                    table.ForeignKey(
                        name: "warehouse_stores_productId_fkey",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_stores_storeId_fkey",
                        column: x => x.storeId,
                        principalTable: "stores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "roles_permissions",
                columns: table => new
                {
                    roleId = table.Column<Guid>(type: "uuid", nullable: false),
                    permissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_permissions_pkey", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "roles_permissions_permissionId_fkey",
                        column: x => x.permissionId,
                        principalTable: "permissions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "roles_permissions_roleId_fkey",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "entrances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type_person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_date = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    sale_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    sale_channel = table.Column<Guid>(type: "uuid", nullable: false),
                    total_sale = table.Column<double>(type: "double precision", nullable: false),
                    payment_method = table.Column<Guid>(type: "uuid", nullable: false),
                    free = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("entrances_pkey", x => x.id);
                    table.ForeignKey(
                        name: "entrances_payment_method_fkey",
                        column: x => x.payment_method,
                        principalTable: "payment_methods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "entrances_sale_channel_fkey",
                        column: x => x.sale_channel,
                        principalTable: "sales_channels",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "entrances_type_person_id_fkey",
                        column: x => x.type_person_id,
                        principalTable: "type_persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "entrances_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    place_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rentals_pkey", x => x.id);
                    table.ForeignKey(
                        name: "rentals_customer_id_fkey",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rentals_place_id_fkey",
                        column: x => x.place_id,
                        principalTable: "places",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rentals_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "returns",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: true),
                    salesId = table.Column<Guid>(type: "uuid", nullable: true),
                    storeId = table.Column<Guid>(type: "uuid", nullable: true),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    observations = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("returns_pkey", x => x.id);
                    table.ForeignKey(
                        name: "returns_productId_fkey",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "returns_salesId_fkey",
                        column: x => x.salesId,
                        principalTable: "sales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "returns_storeId_fkey",
                        column: x => x.storeId,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "saleDetails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    saleId = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    mount = table.Column<double>(type: "double precision", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("saleDetails_pkey", x => x.id);
                    table.ForeignKey(
                        name: "saleDetails_productId_fkey",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "saleDetails_saleId_fkey",
                        column: x => x.saleId,
                        principalTable: "sales",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    resourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "recipes_productId_fkey",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "recipes_resourceId_fkey",
                        column: x => x.resourceId,
                        principalTable: "resources",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse_movement_resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resource_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movement_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    movement_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    observations = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_movement_resources_pkey", x => x.id);
                    table.ForeignKey(
                        name: "warehouse_movement_resources_resource_id_fkey",
                        column: x => x.resource_id,
                        principalTable: "resources",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_movement_resources_warehouse_id_fkey",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse_resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resource_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    type_unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    unit_price = table.Column<double>(type: "double precision", nullable: false),
                    total_cost = table.Column<double>(type: "double precision", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_resources_pkey", x => x.id);
                    table.ForeignKey(
                        name: "warehouse_resources_resource_id_fkey",
                        column: x => x.resource_id,
                        principalTable: "resources",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_resources_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "warehouse_resources_warehouse_id_fkey",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_purchased",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    buysproduct_id = table.Column<Guid>(type: "uuid", nullable: true),
                    productPurchased_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_purchased_pkey", x => x.id);
                    table.ForeignKey(
                        name: "product_purchased_buysproduct_id_fkey",
                        column: x => x.buysproduct_id,
                        principalTable: "buys_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "product_purchased_productPurchased_id_fkey",
                        column: x => x.productPurchased_id,
                        principalTable: "buys_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "lost",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    lost_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    observations = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lost_pkey", x => x.id);
                    table.ForeignKey(
                        name: "lost_production_id_fkey",
                        column: x => x.production_id,
                        principalTable: "production",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_buys_products_product_id",
                table: "buys_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_buys_products_supplier_id",
                table: "buys_products",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_buys_products_warehouse_id",
                table: "buys_products",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "customers_dni_key",
                table: "customers",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "customers_email_key",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "customers_phone_key",
                table: "customers",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entrances_payment_method",
                table: "entrances",
                column: "payment_method");

            migrationBuilder.CreateIndex(
                name: "IX_entrances_sale_channel",
                table: "entrances",
                column: "sale_channel");

            migrationBuilder.CreateIndex(
                name: "IX_entrances_type_person_id",
                table: "entrances",
                column: "type_person_id");

            migrationBuilder.CreateIndex(
                name: "IX_entrances_user_id",
                table: "entrances",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_expenses_module_id",
                table: "general_expenses",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_expenses_report_id",
                table: "general_expenses",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_incomes_module_id",
                table: "general_incomes",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_incomes_report_id",
                table: "general_incomes",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_INCOME_CHURCH_id_church",
                table: "INCOME_CHURCH",
                column: "id_church");

            migrationBuilder.CreateIndex(
                name: "IX_lost_production_id",
                table: "lost",
                column: "production_id");

            migrationBuilder.CreateIndex(
                name: "IX_monastery_expenses_overheadsId",
                table: "monastery_expenses",
                column: "overheadsId");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_moduleId",
                table: "permissions",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "IX_places_location_id",
                table: "places",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_purchased_buysproduct_id",
                table: "product_purchased",
                column: "buysproduct_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_purchased_productPurchased_id",
                table: "product_purchased",
                column: "productPurchased_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_plant_id",
                table: "production",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_productId",
                table: "production",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_productId",
                table: "recipes",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_resourceId",
                table: "recipes",
                column: "resourceId");

            migrationBuilder.CreateIndex(
                name: "IX_rent_church_idChurch",
                table: "rent_church",
                column: "idChurch");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_customer_id",
                table: "rentals",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_place_id",
                table: "rentals",
                column: "place_id");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_user_id",
                table: "rentals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_resources_supplier_id",
                table: "resources",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_returns_productId",
                table: "returns",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_returns_salesId",
                table: "returns",
                column: "salesId");

            migrationBuilder.CreateIndex(
                name: "IX_returns_storeId",
                table: "returns",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_roles_permissions_permissionId",
                table: "roles_permissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_saleDetails_productId",
                table: "saleDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_saleDetails_saleId",
                table: "saleDetails",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_store_id",
                table: "sales",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_roleId",
                table: "users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "users_dni_key",
                table: "users",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_products_product_id",
                table: "warehouse_movement_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_products_warehouse_id",
                table: "warehouse_movement_products",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_resources_resource_id",
                table: "warehouse_movement_resources",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_resources_warehouse_id",
                table: "warehouse_movement_resources",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_products_product_id",
                table: "warehouse_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_products_warehouse_id",
                table: "warehouse_products",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_resources_resource_id",
                table: "warehouse_resources",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_resources_supplier_id",
                table: "warehouse_resources",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_resources_warehouse_id",
                table: "warehouse_resources",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_stores_productId",
                table: "warehouse_stores",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_stores_storeId",
                table: "warehouse_stores",
                column: "storeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_sessions");

            migrationBuilder.DropTable(
                name: "entrances");

            migrationBuilder.DropTable(
                name: "general_expenses");

            migrationBuilder.DropTable(
                name: "general_incomes");

            migrationBuilder.DropTable(
                name: "INCOME_CHURCH");

            migrationBuilder.DropTable(
                name: "lost");

            migrationBuilder.DropTable(
                name: "monastery_expenses");

            migrationBuilder.DropTable(
                name: "product_purchased");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "rent_church");

            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "returns");

            migrationBuilder.DropTable(
                name: "roles_permissions");

            migrationBuilder.DropTable(
                name: "saleDetails");

            migrationBuilder.DropTable(
                name: "warehouse_movement_products");

            migrationBuilder.DropTable(
                name: "warehouse_movement_resources");

            migrationBuilder.DropTable(
                name: "warehouse_products");

            migrationBuilder.DropTable(
                name: "warehouse_resources");

            migrationBuilder.DropTable(
                name: "warehouse_stores");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "sales_channels");

            migrationBuilder.DropTable(
                name: "type_persons");

            migrationBuilder.DropTable(
                name: "financial_reports");

            migrationBuilder.DropTable(
                name: "production");

            migrationBuilder.DropTable(
                name: "overheads");

            migrationBuilder.DropTable(
                name: "buys_products");

            migrationBuilder.DropTable(
                name: "churches");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "places");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "resources");

            migrationBuilder.DropTable(
                name: "plant_production");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
