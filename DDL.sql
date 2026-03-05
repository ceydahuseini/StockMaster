CREATE SCHEMA IF NOT EXISTS stock_management;
SET search_path TO stock_management;

DROP TABLE IF EXISTS warehouse_stock CASCADE;
DROP TABLE IF EXISTS purchase_order_item CASCADE;
DROP TABLE IF EXISTS sale_item CASCADE;
DROP TABLE IF EXISTS purchase_order CASCADE;
DROP TABLE IF EXISTS sale CASCADE;
DROP TABLE IF EXISTS warehouse CASCADE;
DROP TABLE IF EXISTS product CASCADE;
DROP TABLE IF EXISTS supplier CASCADE;
DROP TABLE IF EXISTS category CASCADE;
DROP TABLE IF EXISTS customer CASCADE;
DROP TABLE IF EXISTS users CASCADE;

CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    role VARCHAR(20) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE customer (
    customer_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    address TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE category (
    category_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE supplier (
    supplier_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    contact_person VARCHAR(100) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(100) NOT NULL,
    address TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE product (
    product_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    sku VARCHAR(50) NOT NULL UNIQUE,
    unit_price DECIMAL(12, 2) NOT NULL,
    reorder_level INT NOT NULL DEFAULT 10,
    category_id INT,
    supplier_id INT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_product_category FOREIGN KEY (category_id)
        REFERENCES category(category_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    CONSTRAINT fk_product_supplier FOREIGN KEY (supplier_id)
        REFERENCES supplier(supplier_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE warehouse (
    warehouse_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    location VARCHAR(255) NOT NULL,
    capacity INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE sale (
    sale_id SERIAL PRIMARY KEY,
    date_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(15, 2) NOT NULL,
    user_id INT,
    customer_id INT,
    warehouse_id INT NOT NULL,
    CONSTRAINT fk_sale_user FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE SET NULL,
    CONSTRAINT fk_sale_customer FOREIGN KEY (customer_id)
        REFERENCES customer(customer_id)
        ON DELETE SET NULL,
    CONSTRAINT fk_sale_warehouse FOREIGN KEY (warehouse_id)
        REFERENCES warehouse(warehouse_id)
        ON DELETE RESTRICT
);

CREATE TABLE purchase_order (
    po_id SERIAL PRIMARY KEY,
    order_date DATE NOT NULL DEFAULT CURRENT_DATE,
    expected_delivery_date DATE NOT NULL,
    actual_delivery_date DATE,
    status VARCHAR(20) NOT NULL,
    supplier_id INT,
    warehouse_id INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_po_supplier FOREIGN KEY (supplier_id)
        REFERENCES supplier(supplier_id)
        ON DELETE SET NULL,
    CONSTRAINT fk_po_warehouse FOREIGN KEY (warehouse_id)
        REFERENCES warehouse(warehouse_id)
        ON DELETE RESTRICT
);

CREATE TABLE sale_item (
    sale_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    unit_price_at_sale DECIMAL(12, 2) NOT NULL,
    PRIMARY KEY (sale_id, product_id),
    CONSTRAINT fk_saleitem_sale FOREIGN KEY (sale_id)
        REFERENCES sale(sale_id)
        ON DELETE CASCADE,
    CONSTRAINT fk_saleitem_product FOREIGN KEY (product_id)
        REFERENCES product(product_id)
        ON DELETE RESTRICT
);

CREATE TABLE purchase_order_item (
    po_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    unit_cost DECIMAL(12, 2) NOT NULL,
    received_quantity INT DEFAULT 0,
    PRIMARY KEY (po_id, product_id),
    CONSTRAINT fk_poitem_order FOREIGN KEY (po_id)
        REFERENCES purchase_order(po_id)
        ON DELETE CASCADE,
    CONSTRAINT fk_poitem_product FOREIGN KEY (product_id)
        REFERENCES product(product_id)
        ON DELETE RESTRICT
);

CREATE TABLE warehouse_stock (
    warehouse_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity_on_hand INT NOT NULL DEFAULT 0,
    last_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (warehouse_id, product_id),
    CONSTRAINT fk_stock_warehouse FOREIGN KEY (warehouse_id)
        REFERENCES warehouse(warehouse_id)
        ON DELETE CASCADE,
    CONSTRAINT fk_stock_product FOREIGN KEY (product_id)
        REFERENCES product(product_id)
        ON DELETE CASCADE
);
