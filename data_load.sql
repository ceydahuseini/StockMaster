TRUNCATE TABLE users, customer, category, supplier, product, warehouse, 
               sale, purchase_order, sale_item, purchase_order_item, warehouse_stock 
RESTART IDENTITY CASCADE;

INSERT INTO users (username, password, full_name, email, role, is_active) VALUES
('ceyda_admin', 'test123', 'Ceyda Huseini', 'ceyda@gmail.com', 'Admin', true),
('test_admin', 'admin123', 'Admin User', 'admin@gmail.com', 'Admin', true),
('gjorgji_adm', 'pass123', 'Gjorgji Naumov', 'gjorgji@system.mk', 'Admin', true),
('elena_inv', 'pass456', 'Elena Stojanovska', 'elena@system.mk', 'Inventory Manager', true),
('zoran_sales', 'pass789', 'Zoran Milevski', 'zoran@system.mk', 'Sales Personnel', true),
('stefan_wh', 'pass000', 'Stefan Trajkovski', 'stefan@system.mk', 'Warehouse Staff', true);

INSERT INTO customer (name, email, phone, address) VALUES
('Petar Naumovski', 'petar@email.mk', '+389 70 123 456', 'ul. Partizanski Odredi br. 10, Skopje'),
('Marija Jovanovska', 'marija@email.mk', '+389 71 987 654', 'ul. Shirok Sokak br. 45, Bitola'),
('Igor Angelov', 'igor@email.mk', '+389 75 555 444', 'ul. Goce Delchev br. 22, Kumanovo');

INSERT INTO category (name, description) VALUES
('IT Oprema', 'Kompjuteri, laptopi i mrezna oprema'),
('Kancelariska Oprema', 'Mebeli i materijali za kancelarija');

INSERT INTO supplier (name, contact_person, phone, email, address) VALUES
('Makedonski Telekom', 'Darko Kolev', '+389 2 3100 000', 'contact@telekom.mk', 'Kej 13-ti Noemvri, Skopje'),
('Anhoch PC Market', 'Bojan Janev', '+389 2 3111 888', 'prodazba@anhoch.com', 'Skopje City Mall'),
('Prosvetno Delo', 'Ana Ristovska', '+389 2 3222 111', 'info@prosvetno.mk', 'Industriska Zona Vizbegovo');

INSERT INTO warehouse (name, location, capacity) VALUES
('Skopje Centralen Magacin', 'Ind. Zona Vizbegovo, Skopje', 10000),
('Bitola Distributiven Centar', 'Kravarski Pat, Bitola', 4000);

INSERT INTO product (name, description, sku, unit_price, reorder_level, category_id, supplier_id) VALUES
('Monitor Dell 24"', 'Full HD IPS Monitor', 'DELL-24-IPS', 9500.00, 10, 1, 2),
('Laptop HP 250 G8', 'Core i5, 8GB RAM, 256GB SSD', 'HP-250-G8', 32000.00, 5, 1, 2),
('Kancelariski Stol - Ergonomska', 'Crna koza, podesiva visina', 'CHAIR-ERG-01', 6500.00, 15, 2, 3);

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
('2025-02-01', 'Pending', 2, 1),
('2025-02-05', 'Confirmed', 3, 2);

INSERT INTO purchase_order_item (po_id, product_id, quantity, unit_cost) VALUES
(1, 2, 10, 28000.00),
(2, 3, 20, 5000.00);