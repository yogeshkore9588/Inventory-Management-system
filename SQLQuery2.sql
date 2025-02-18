select * from customers

alter table customers
drop column prod_id

INSERT INTO customers (customer_id, total_price, amount, change, order_date) 
VALUES (1, 2000, 500, 200, '2024-12-12');
 