using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockMaster.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stock_management");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "stock_management",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "stock_management",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "product_price_log",
                schema: "stock_management",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    old_price = table.Column<decimal>(type: "numeric", nullable: false),
                    new_price = table.Column<decimal>(type: "numeric", nullable: false),
                    changed_by = table.Column<string>(type: "text", nullable: false),
                    changed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_price_log", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                schema: "stock_management",
                columns: table => new
                {
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contact_person = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.supplier_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "stock_management",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                schema: "stock_management",
                columns: table => new
                {
                    warehouse_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.warehouse_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "stock_management",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    reorder_level = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    supplier_id = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "stock_management",
                        principalTable: "category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK_product_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "stock_management",
                        principalTable: "supplier",
                        principalColumn: "supplier_id");
                });

            migrationBuilder.CreateTable(
                name: "purchase_order",
                schema: "stock_management",
                columns: table => new
                {
                    po_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expected_delivery_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    actual_delivery_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: true),
                    warehouse_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order", x => x.po_id);
                    table.ForeignKey(
                        name: "FK_purchase_order_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "stock_management",
                        principalTable: "supplier",
                        principalColumn: "supplier_id");
                    table.ForeignKey(
                        name: "FK_purchase_order_warehouse_warehouse_id",
                        column: x => x.warehouse_id,
                        principalSchema: "stock_management",
                        principalTable: "warehouse",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale",
                schema: "stock_management",
                columns: table => new
                {
                    sale_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    customer_id = table.Column<int>(type: "integer", nullable: true),
                    warehouse_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale", x => x.sale_id);
                    table.ForeignKey(
                        name: "FK_sale_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "stock_management",
                        principalTable: "customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_sale_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "stock_management",
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_sale_warehouse_warehouse_id",
                        column: x => x.warehouse_id,
                        principalSchema: "stock_management",
                        principalTable: "warehouse",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_stock",
                schema: "stock_management",
                columns: table => new
                {
                    warehouse_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity_on_hand = table.Column<int>(type: "integer", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_stock", x => new { x.warehouse_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_warehouse_stock_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "stock_management",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_warehouse_stock_warehouse_warehouse_id",
                        column: x => x.warehouse_id,
                        principalSchema: "stock_management",
                        principalTable: "warehouse",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_item",
                schema: "stock_management",
                columns: table => new
                {
                    po_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_cost = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    received_quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_item", x => new { x.po_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_purchase_order_item_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "stock_management",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_item_purchase_order_po_id",
                        column: x => x.po_id,
                        principalSchema: "stock_management",
                        principalTable: "purchase_order",
                        principalColumn: "po_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_item",
                schema: "stock_management",
                columns: table => new
                {
                    sale_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price_at_sale = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_item", x => new { x.sale_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_sale_item_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "stock_management",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_item_sale_sale_id",
                        column: x => x.sale_id,
                        principalSchema: "stock_management",
                        principalTable: "sale",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                schema: "stock_management",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_supplier_id",
                schema: "stock_management",
                table: "product",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_supplier_id",
                schema: "stock_management",
                table: "purchase_order",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_warehouse_id",
                schema: "stock_management",
                table: "purchase_order",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_item_product_id",
                schema: "stock_management",
                table: "purchase_order_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_customer_id",
                schema: "stock_management",
                table: "sale",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_user_id",
                schema: "stock_management",
                table: "sale",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_warehouse_id",
                schema: "stock_management",
                table: "sale",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_item_product_id",
                schema: "stock_management",
                table: "sale_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_stock_product_id",
                schema: "stock_management",
                table: "warehouse_stock",
                column: "product_id");

            // TABLOLAR OLUŞTUKTAN SONRA TRIGGER VE VIEWLARI OLUŞTURUYORUZ
            migrationBuilder.Sql(@"
                SET search_path TO stock_management, public;

                -- LOG TABLOSU
                CREATE TABLE IF NOT EXISTS product_price_log (
                    log_id       SERIAL PRIMARY KEY,
                    product_id   INT NOT NULL,
                    product_name VARCHAR(100),
                    old_price    DECIMAL(12,2),
                    new_price    DECIMAL(12,2),
                    changed_by   VARCHAR(50),
                    changed_at   TIMESTAMP DEFAULT NOW()
                );

                -- FONKSIYONLAR VE TRIGGERLAR
                CREATE OR REPLACE FUNCTION trg_fn_recalculate_sale_total() RETURNS TRIGGER LANGUAGE plpgsql AS $$
                DECLARE v_sale_id INT;
                BEGIN
                    IF TG_OP = 'DELETE' THEN v_sale_id := OLD.sale_id; ELSE v_sale_id := NEW.sale_id; END IF;
                    UPDATE sale SET total_amount = (SELECT COALESCE(SUM(quantity * unit_price_at_sale), 0) FROM sale_item WHERE sale_id = v_sale_id) WHERE sale_id = v_sale_id;
                    RETURN NULL;
                END; $$;

                CREATE OR REPLACE TRIGGER trg_recalculate_sale_total AFTER INSERT OR UPDATE OR DELETE ON sale_item FOR EACH ROW EXECUTE FUNCTION trg_fn_recalculate_sale_total();

                CREATE OR REPLACE FUNCTION trg_fn_prevent_negative_stock() RETURNS TRIGGER LANGUAGE plpgsql AS $$
                BEGIN
                    IF NEW.quantity_on_hand < 0 THEN RAISE EXCEPTION 'Stock cannot be negative!'; END IF;
                    RETURN NEW;
                END; $$;

                CREATE OR REPLACE TRIGGER trg_prevent_negative_stock BEFORE INSERT OR UPDATE ON warehouse_stock FOR EACH ROW EXECUTE FUNCTION trg_fn_prevent_negative_stock();

                CREATE OR REPLACE FUNCTION prevent_self_delete() RETURNS TRIGGER AS $$
                BEGIN
                    IF OLD.username = current_setting('app.current_user', true) THEN RAISE EXCEPTION 'You cannot delete your own account.'; END IF;
                    RETURN OLD;
                END; $$ LANGUAGE plpgsql;

                CREATE OR REPLACE TRIGGER trg_prevent_self_delete BEFORE DELETE ON users FOR EACH ROW EXECUTE FUNCTION prevent_self_delete();

                CREATE OR REPLACE FUNCTION log_price_change() RETURNS TRIGGER AS $$
                BEGIN
                    IF NEW.unit_price <> OLD.unit_price THEN
                        INSERT INTO product_price_log (product_id, product_name, old_price, new_price, changed_by)
                        VALUES (OLD.product_id, OLD.name, OLD.unit_price, NEW.unit_price, current_setting('app.current_user', true));
                    END IF;
                    RETURN NEW;
                END; $$ LANGUAGE plpgsql;

                CREATE OR REPLACE TRIGGER trg_price_change BEFORE UPDATE ON product FOR EACH ROW EXECUTE FUNCTION log_price_change();

                -- VIEWS
                CREATE OR REPLACE VIEW vw_sales_by_day_of_week AS
                SELECT EXTRACT(ISODOW FROM s.date_time)::INT AS day_number,
                       CASE EXTRACT(ISODOW FROM s.date_time)::INT WHEN 1 THEN 'Monday' WHEN 2 THEN 'Tuesday' WHEN 3 THEN 'Wednesday' WHEN 4 THEN 'Thursday' WHEN 5 THEN 'Friday' WHEN 6 THEN 'Saturday' WHEN 7 THEN 'Sunday' END AS day_name,
                       COUNT(DISTINCT s.sale_id) AS total_sales, COALESCE(SUM(s.total_amount), 0) AS total_revenue, ROUND(COALESCE(AVG(s.total_amount), 0), 2) AS avg_sale_value, COALESCE(SUM(si.quantity), 0) AS total_items_sold
                FROM sale s JOIN sale_item si ON s.sale_id = si.sale_id GROUP BY EXTRACT(ISODOW FROM s.date_time) ORDER BY day_number;

                CREATE OR REPLACE VIEW vw_employee_sales_ranking AS
                SELECT u.user_id, u.full_name, u.role, COUNT(DISTINCT s.sale_id) AS total_sales, COALESCE(SUM(s.total_amount), 0) AS total_revenue, ROUND(COALESCE(AVG(s.total_amount), 0), 2) AS avg_sale_value, COUNT(DISTINCT s.customer_id) AS unique_customers, RANK() OVER (ORDER BY COALESCE(SUM(s.total_amount), 0) DESC) AS revenue_rank
                FROM users u LEFT JOIN sale s ON u.user_id = s.user_id GROUP BY u.user_id, u.full_name, u.role HAVING COUNT(s.sale_id) > 0;

                CREATE OR REPLACE VIEW vw_todays_sales_summary AS
                SELECT COUNT(DISTINCT s.sale_id) AS total_transactions_today, COALESCE(SUM(s.total_amount), 0) AS total_revenue_today, COALESCE(SUM(si.quantity), 0) AS total_items_sold_today, COUNT(DISTINCT s.customer_id) AS unique_customers_today, COUNT(DISTINCT s.warehouse_id) AS active_warehouses_today
                FROM sale s JOIN sale_item si ON s.sale_id = si.sale_id WHERE s.date_time::DATE = CURRENT_DATE;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_price_log",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "purchase_order_item",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "sale_item",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "warehouse_stock",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "purchase_order",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "sale",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "product",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "users",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "warehouse",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "category",
                schema: "stock_management");

            migrationBuilder.DropTable(
                name: "supplier",
                schema: "stock_management");
        }
    }
}
