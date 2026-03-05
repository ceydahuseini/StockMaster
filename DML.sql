TRUNCATE TABLE users, customer, category, supplier, product, warehouse, 
               sale, purchase_order, sale_item, purchase_order_item, warehouse_stock 
RESTART IDENTITY CASCADE;

INSERT INTO users (username, password, full_name, email, role, is_active) VALUES
('ceyda_admin', 'test123', 'Ceyda Huseini', 'ceyda@gmail.com', 'Admin', true),
('test_admin', 'admin123', 'Admin User', 'admin@gmail.com', 'Admin', true);

INSERT INTO customer (name, email, phone, address) VALUES
('Ana Anevska', 'ana@email.com', '+1 555 123 456', '10 Liberty Ave, New York'),
('Elena Dimitrievska', 'elena@email.com', '+1 555 987 654', '45 Broad Street, Chicago'),
('Marko Markoski', 'marko@email.com', '+1 555 555 444', '22 Oak Drive, Los Angeles');

INSERT INTO category (name, description) VALUES
('IT Equipment', 'Computers, laptops and networking equipment'),
('Office Furniture', 'Office furniture and supplies');

INSERT INTO supplier (name, contact_person, phone, email, address) VALUES
('Global Telecom', 'David Cole', '+1 555 100 000', 'contact@globaltelekom.com', '13 Commerce Blvd, New York'),
('TechMart PC Store', 'Brian Jones', '+1 555 111 888', 'sales@techmart.com', 'Central Shopping Mall, Chicago'),
('Office Pro Supplies', 'Anna Roberts', '+1 555 222 111', 'info@officepro.com', 'Industrial Zone, Los Angeles');

INSERT INTO warehouse (name, location, capacity) VALUES
('New York Central Warehouse', 'Industrial Zone, New York', 10000),
('Chicago Distribution Center', 'West Road, Chicago', 4000);

INSERT INTO product (name, description, sku, unit_price, reorder_level, category_id, supplier_id) VALUES
('Dell 24" Monitor', 'Full HD IPS Monitor', 'DELL-24-IPS', 9500.00, 10, 1, 2),
('HP 250 G8 Laptop', 'Core i5, 8GB RAM, 256GB SSD', 'HP-250-G8', 32000.00, 5, 1, 2),
('Ergonomic Office Chair', 'Black leather, adjustable height', 'CHAIR-ERG-01', 6500.00, 15, 2, 3);

INSERT INTO warehouse_stock (warehouse_id, product_id, quantity_on_hand) VALUES
(1, 1, 45),
(1, 2, 12),
(2, 2, 5),
(2, 3, 30);

INSERT INTO sale (total_amount, user_id, customer_id, warehouse_id) VALUES
(41500.00, 3, 1, 1),
(6500.00, 3, 2, 2);

INSERT INTO sale_item (sale_id, product_id, quantity, unit_price_at_sale) VALUES
(1, 2, 1, 32000.00),
(1, 1, 1, 9500.00),
(2, 3, 1, 6500.00);

INSERT INTO purchase_order (expected_delivery_date, status, supplier_id, warehouse_id) VALUES
('2026-03-08', 'Pending', 2, 1),
('2026-03-05', 'Confirmed', 3, 2);

INSERT INTO purchase_order_item (po_id, product_id, quantity, unit_cost) VALUES
(1, 2, 10, 28000.00),
(2, 3, 20, 5000.00);