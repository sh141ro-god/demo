-- ============================================================
--  Магазин обуви — схема БД (3НФ) + тестовые данные
--  СУБД: MS SQL Server
-- ============================================================
-- Раскомментируй при необходимости создать базу:
-- CREATE DATABASE shoe_store;
-- GO
-- USE shoe_store;
-- GO

-- ---------- СПРАВОЧНИКИ ----------

CREATE TABLE Role (
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE [User] (                      -- User — служебное слово, поэтому в скобках
    Id       INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Login    NVARCHAR(50)  NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,        -- для учебной БД — открытым текстом
    IdRole   INT NOT NULL FOREIGN KEY REFERENCES Role(Id)
);

CREATE TABLE Category (
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Manufacturer (
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Supplier (
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Unit (                        -- единица измерения
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE OrderStatus (
    Id   INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- ---------- ОСНОВНЫЕ ТАБЛИЦЫ ----------

CREATE TABLE Product (
    Id             INT IDENTITY PRIMARY KEY,
    Name           NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(500),
    IdCategory     INT NOT NULL FOREIGN KEY REFERENCES Category(Id),
    IdManufacturer INT NOT NULL FOREIGN KEY REFERENCES Manufacturer(Id),
    IdSupplier     INT NOT NULL FOREIGN KEY REFERENCES Supplier(Id),
    IdUnit         INT NOT NULL FOREIGN KEY REFERENCES Unit(Id),
    Price          DECIMAL(10,2) NOT NULL CHECK (Price >= 0),     -- цена: сотые, не отриц.
    Quantity       INT NOT NULL CHECK (Quantity >= 0),            -- кол-во не отриц.
    Discount       INT NOT NULL DEFAULT 0 CHECK (Discount BETWEEN 0 AND 100),
    PhotoPath      NVARCHAR(255)                                  -- путь к фото
);

CREATE TABLE [Order] (                      -- Order — служебное слово
    Id            INT IDENTITY PRIMARY KEY,
    Article       NVARCHAR(50) NOT NULL,                          -- артикул заказа
    IdStatus      INT NOT NULL FOREIGN KEY REFERENCES OrderStatus(Id),
    PickupAddress NVARCHAR(200),                                  -- адрес пункта выдачи
    OrderDate     DATE NOT NULL,                                  -- дата заказа
    DeliveryDate  DATE                                            -- дата выдачи/доставки
);

-- строки заказа: связь заказ <-> товар (нужна для проверки удаления товара)
CREATE TABLE OrderItem (
    Id        INT IDENTITY PRIMARY KEY,
    IdOrder   INT NOT NULL FOREIGN KEY REFERENCES [Order](Id),
    IdProduct INT NOT NULL FOREIGN KEY REFERENCES Product(Id),
    Quantity  INT NOT NULL CHECK (Quantity > 0)
);
GO

-- ============================================================
--  ТЕСТОВЫЕ ДАННЫЕ
-- ============================================================

INSERT INTO Role (Name) VALUES ('Клиент'), ('Менеджер'), ('Администратор');

INSERT INTO [User] (FullName, Login, Password, IdRole) VALUES
    ('Иванов Иван Иванович',     'admin',   'admin',   3),
    ('Петров Пётр Петрович',     'manager', 'manager', 2),
    ('Сидоров Сидор Сидорович',  'client',  'client',  1);

INSERT INTO Category (Name)     VALUES ('Кроссовки'), ('Ботинки'), ('Туфли');
INSERT INTO Manufacturer (Name) VALUES ('Nike'), ('Adidas'), ('Ecco');
INSERT INTO Supplier (Name)     VALUES ('ООО «ОбувьОпт»'), ('ИП Смирнов');
INSERT INTO Unit (Name)         VALUES ('пара'), ('шт');
INSERT INTO OrderStatus (Name)  VALUES ('Новый'), ('В обработке'), ('Доставлен');

-- Товары специально подобраны под правила подсветки:
INSERT INTO Product (Name, Description, IdCategory, IdManufacturer, IdSupplier, IdUnit, Price, Quantity, Discount, PhotoPath) VALUES
    ('Кроссовки Air',     'Лёгкие беговые',      1, 1, 1, 1, 5990.00, 12,  0, 'picture.png'),  -- обычный
    ('Кроссовки Boost',   'Амортизация',         1, 2, 1, 1, 7490.00,  5, 20, 'picture.png'),  -- скидка >15% -> зелёный фон
    ('Ботинки Зима',      'Утеплённые',          2, 3, 2, 1, 8990.00,  0, 10, 'picture.png'),  -- нет на складе -> голубой
    ('Туфли Классик',     'Кожаные',             3, 3, 2, 1, 6490.00,  8, 10, 'picture.png'),  -- скидка 10% (цена снижена, но не >15%)
    ('Кроссовки Run',     'Для города',          1, 1, 1, 1, 4990.00, 20,  0, 'picture.png');  -- обычный

INSERT INTO [Order] (Article, IdStatus, PickupAddress, OrderDate, DeliveryDate) VALUES
    ('ORD-001', 2, 'г. Дронтен, ул. Центральная, 1', '2026-06-01', '2026-06-05'),
    ('ORD-002', 1, 'г. Дронтен, ул. Лесная, 7',       '2026-06-10', NULL);

-- В заказ ORD-001 входит товар Id=1 -> его удалить будет НЕЛЬЗЯ (проверка работает)
INSERT INTO OrderItem (IdOrder, IdProduct, Quantity) VALUES
    (1, 1, 2),
    (1, 4, 1),
    (2, 5, 3);
GO
