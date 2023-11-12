
begin
-- EMPLOYEES
CREATE TABLE EMPLOYEES
(
    ID           INT PRIMARY KEY,
    FIRST_NAME   VARCHAR(50),
    LAST_NAME    VARCHAR(50),
    LOGIN        VARCHAR(50),
    PASSWORD     VARCHAR(50),
    POSITION     VARCHAR(50),
    EMAIL        VARCHAR(100),
    PHONE_NUMBER VARCHAR(20),
    SALARY       DECIMAL(10, 2)
);

-- CUSTOMERS
CREATE TABLE CUSTOMERS
(
    ID           INT PRIMARY KEY,
    FIRST_NAME   VARCHAR(50),
    LAST_NAME    VARCHAR(50),
    LOGIN        VARCHAR(50),
    PASSWORD     VARCHAR(50),
    EMAIL        VARCHAR(100),
    PHONE_NUMBER VARCHAR(20)
);

-- TABLES
CREATE TABLE TABLES
(
    ID           INT PRIMARY KEY,
    TABLE_NUMBER VARCHAR(10),
    TABLE_SIZE   VARCHAR(20),
    TABLE_STATUS VARCHAR(20)
);

-- MENU
CREATE TABLE MENU
(
    ID           INT PRIMARY KEY,
    NAME         VARCHAR(50),
    DESCRIPTION  VARCHAR(200),
    PRICE        DECIMAL(10, 2),
    MENU_TYPE    VARCHAR(50),
    NOTES        VARCHAR(200)
);

-- ORDERS
CREATE TABLE ORDERS
(
    ID          INT PRIMARY KEY,
    ORDER_DATE  DATETIME2(6),
    TABLE_ID    INT FOREIGN KEY REFERENCES TABLES(ID),
    CUSTOMER_ID INT FOREIGN KEY REFERENCES CUSTOMERS(ID),
    WAITER_ID   INT FOREIGN KEY REFERENCES EMPLOYEES(ID),
    STATUS      VARCHAR(50),
    NOTES       VARCHAR(200)
);

-- ORDERITEMS
CREATE TABLE ORDERITEMS
(
    ID       INT PRIMARY KEY,
    ORDER_ID INT FOREIGN KEY REFERENCES ORDERS(ID),
    MENU_ID  INT FOREIGN KEY REFERENCES MENU(ID),
    QUANTITY INT,
    NOTES    VARCHAR(200)
);

-- REVIEWS
CREATE TABLE REVIEWS
(
    ID       INT PRIMARY KEY,
    ORDER_ID INT FOREIGN KEY REFERENCES ORDERS(ID),
    RATING   INT,
    NOTES    VARCHAR(200)
);
end;