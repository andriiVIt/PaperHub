
INSERT INTO customers (name, address, phone, email)
VALUES
    ('Andrii Savchenko', 'Elmegade 6,1 th', '52706690', 'acepol111@gmail.com'),
    ('William Johnson', '12 Baker Street, London, UK', '+447711223344', 'william.johnson@gmail.com'),
    ('Emma Nielsen', '45 Strandgade, Copenhagen, Denmark', '+4522334455', 'emma.nielsen@outlook.dk'),
    ('Oliver Smith', '78 High Road, Manchester, UK', '+447912345678', 'oliver.smith@yahoo.co.uk'),
    ('Amalie Jensen', '32 Østerbrogade, Aarhus, Denmark', '+4521233344', 'amalie.jensen@live.dk'),
    ('Lucas Brown', '25 Kingsway, Edinburgh, UK', '+447788990011', 'lucas.brown@hotmail.com'),
    ('Sophie Hansen', '19 Nyhavn, Copenhagen, Denmark', '+45333445566', 'sophie.hansen@mail.dk'),
    ('James Thompson', '56 Elm Street, Birmingham, UK', '+447811223344', 'james.thompson@btinternet.com');



INSERT INTO order_entries (quantity, product_id, order_id)
VALUES
    (1, 1, 55),
    (1, 1, 58),
    (15, 1, 59),
    (15, 1, 60),
    (1, 1, 61),
    (1, 1, 62),
    (1, 1, 63),
    (24, 3, 63),
    (2, 1, 63),
    (18, 1, 63),
    (15, 1, 64),
    (15, 1, 64);



INSERT INTO orders (order_date, delivery_date, status, total_amount, customer_id)
VALUES
    ('2024-10-10 16:23:17.380903', NULL, 'Pending', 100, 2),
    ('2024-10-10 16:24:11.900355', NULL, 'Pending', 100, 2),
    (NULL, NULL, 'Shipped', 0, NULL),
    (NULL, NULL, 'Delivered', 0, 2),
    (NULL, NULL, 'Shipped', 0, 2),
    ('2024-10-10 21:15:39.008950', NULL, 'Pending', 8.99, 2),
    ('2024-10-10 21:16:01.624493', NULL, 'Pending', 44.98, 2),
    (NULL, NULL, 'Shipped', 0, 2);


INSERT INTO paper (name, discontinued, stock, price)
VALUES
    ('Perforated Invoice Paper', true, 5, 8.99),
    ('EcoRecycled Paper', true, 0, 5.99),
    ('High-Gloss Coated Paper', false, 0, 13.99),
    ('Fine Art Cotton Paper', false, 0, 19.99),
    ('Double-Sided Gloss Paper', false, 0, 12.99),
    ('Translucent Vellum Paper', true, 0, 12.99),
    ('Smooth Ivory Writing Paper', false, 0, 8.75),
    ('Kraft Brown Wrapping Paper', false, 0, 6.25),
    ('Matte Finish Presentation Paper', true, 0, 10.99),
    ('High-Bulk Cardstock Paper', false, 0, 17.25),
    ('UltraSmooth White Paper', true, 0, 7.75),
    ('Acid-Free Archival Paper', true, 0, 14.75),
    ('Heavyweight Bond Paper', true, 0, 11.25),
    ('Carbonless Copy Paper (NCR)', false, 2, 12.5),
    ('Bright Neon Colored Paper', false, 0, 9.5),
    ('Silk Touch Printing Paper', true, 0, 14.25),
    ('Water-Resistant Art Paper', true, 0, 13.5),
    ('Textured Linen Paper', true, 0, 16),
    ('Premium Glossy Photo Paper', true, 0, 15.99),
    ('Recycled Fiber Copier Paper', false, 2, 6.5);




INSERT INTO paper_properties (paper_id, property_id)
VALUES
    (1, 2),
    (1, 1),
    (2, 1),
    (19, 2),
    (24, 6),
    (35, 21),
    (35, 22),
    (36, 18),
    (37, 19);




INSERT INTO properties (property_name)
VALUES
    ('Printer-compatible'),
    ('Double-sided coating'),
    ('Textured'),
    ('High opacity'),
    ('UV-resistant'),
    ('Tear-resistant'),
    ('Archival quality'),
    ('Water-resistant'),
    ('Enhanced bend resistance'),
    ('Smooth surface'),
    ('Acid-free'),
    ('Matte finish'),
    ('Quick-drying'),
    ('Glossy finish'),
    ('Ultra-white'),
    ('High density'),
    ('Laser print compatible'),
    ('Perforated'),
    ('Recyclable'),
    ('Increased thickness'),
    ('Eco-friendly');