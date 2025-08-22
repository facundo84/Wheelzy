IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Wheelzy')
BEGIN
    USE Wheelzy;
END
GO
-- Insertar datos en la tabla brands
INSERT INTO brands (name) VALUES
('Audi'),('Bentley'),('BMW'),
('Buick'),('Cadillac'),('Chevrolet'),
('Chrysler'),('Dodge'),('Ferrari'),
('Fiat'),('Ford'),('Genesis'),
('GMC'),('Honda'),('Hyundai'),
('Infiniti'),('Jaguar'),('Jeep'),
('Karma Automotive LLC'),('Kia'),('Lamborghini'),
('Land Rover'),('Lexus'),('Lincoln'),
('Lotus'),('Maserati'),('Mazda'),
('McLaren Automotive'),('Mercedes-Benz'),('Mini'),
('Mitsubishi'),('Nissan'),('Porsche'),
('Ram'),('Rolls-Royce'),('Smart'),
('Subaru'),('Suzuki'),('Tesla'),
('Toyota'),('Volkswagen'),('Volvo');

-- Insertar datos en la tabla models
INSERT INTO models (name, brandId) VALUES
--AUDI
('A3',1),
('A4 allroad',1),
('A4 Sedan',1),
('A4 Wagon',1),
('A5',1),
('A5 Sportback',1),
('A6',1),
('A7',1),
('A8',1),
('E-Tron',1),
('Q3',1),
('Q5',1),
('Q7',1),
('R8 Convertible',1),
('R8 Coupe',1),
--BENTLEY
('Bentayga',2),
('Continental Convertible',2),
('Continental Coupe',2),
('Flying Spur',2),
('Mulsanne',2),
--BMW
('230',3),
('230xi',3),
('320',3),
('328',3),
('330',3),
('330e',3),
('340',3),
--CHEVROLET
('Blazer',6),
('Bolt EV',6),
('Camaro Convertible',6),
('Camaro Coupe',6),
('Captiva',6),
--FERRARI
('458 Italia',9),
('458 Speciale',9),
('458 Spider',9),
('488 GTB',9),
('488 Spider',9),
('California',9),
('F12 Berlinetta',9)

-- Insertar datos en la tabla submodels
INSERT INTO submodels (name, modelId) VALUES
--AUDI A3
('Base', 1),
('Premium', 1),
('Premium Plus', 1),
('Prestige S-Line', 1),

--AUDI A4 allroad
('Base', 2),
('Premium', 2),
('Premium Plus', 2),
('Prestige', 2),
--AUDI A4 SEDAN
('Base', 3),
('Premium', 3),
('Premium Plus', 3),
('Premium Plus S-Line', 3),
('Prestige', 3),
('Technik', 3),
('Ultra Premium', 3),

--AUDI A4 Wagon
('Base', 4),
('Premium', 4),
('Premium Plus', 4),
('Prestige', 4),

--AUDI A5
('Base', 5),
('Premium', 5),
('Premium Plus', 5),
('Premium Plus S-Line', 5),
('Premium S-Line', 5),
('Prestige', 5),
('Prestige S-Line', 5),

--A5 Sportback
('Base', 6),
('Premium', 6),
--A6
('Base', 7),
('Premium', 7),
('Premium Plus', 7),
('Prestige', 7),
--A7
('Base', 8),
('Premium', 8),
('Premium Plus', 8),
('Prestige', 8),
('Quattro', 8),
('Technik', 8),
--A8
('Base', 9),
('L Quattro', 9),
('L TDI', 9),
('Quattro', 9),
('TDI', 9),

--E-Tron
('Base', 10),
('Prestige', 10),
--Q3
('Base', 11),
('Premium', 11),
('Premium Plus', 11),
('Sport Premium', 11),
('Sport Premium Plus', 11),
('Technik', 11),
--Q5
('Base', 12),
('Premium', 12),
('Premium Hybrid', 12),
('Premium Plus', 12),
('Prestige', 12),
('Progressiv', 12),
('TDI', 12),
('Technik', 12),
--Q7
('Base', 13),
('Premium', 13),
('Premium Plus', 13),
('Prestige', 13),
('Progressiv S-Line', 13),
('Technik S-Line', 13),
--R8 Convertible
('Base', 14),
('4.2 Quattro', 14),
('5.2 Quattro', 14),
--R8 Coupe
('Base', 15),
('5.2 Plus Quattro', 15),
('5.2 Quattro', 15),
('RWS', 15),
--Bentayga
('Base', 16),
--Continental Convertible
('Base', 17),
('GT Speed', 17),
('GT V8', 17),
('GTC', 17),
('GTC Speed', 17),
('GTC V8', 17),
('S', 17),

--Continental Coupe
('Base', 18),
('GT', 18),
('GT Speed', 18),
('GT V8', 18),
('S', 18),
--Flying Spur
('Base', 19),
--Mulsanne
('Base', 20),
--BMW 
--230
('Base', 21),
('I', 21),
--230xi
('Base', 22),
--320
('Base', 23),
('I', 23),
('xDrive', 23),
--328
('Base', 24),
('D XDrive', 24),
('xi SULEV', 24),
--330
('Base', 25),
('I', 25),
('xDrive', 25),
('xi', 25),
--330e
('Base', 26),
--340
('Base', 27),
('I', 27),
('xi', 27),

--Blazer
('Base', 28),
('RS', 28),
--Bolt EV
('Base', 29),
('LT', 29),
('Premier', 29),
--Camaro Convertible
('Base', 30),
('2SS', 30),
('LT', 30),
('SS', 30),
('ZL1', 30),
--Camaro Coupe
('Base', 31),
('2LT', 31),
('2SS', 31),
('LS', 31),
('LT', 31),
('SS', 31),
('ZL1', 31),
--Captiva
('Base', 32),
('LS', 32),
('LT', 32),
('LTZ', 32),
--458 Italia
('Base', 33),
--458 Speciale
('Base', 34),
--458 Spider
('Base', 35),
--488 GTB
('Base', 36),
--488 Spider
('Base', 37),
--California
('Base', 38),
--F12 Berlinetta
('Base', 39);


-- Insertar datos en la tabla zipCodes
INSERT INTO zipCodes (zipCode) VALUES
('35004'),
('99501'),
('85001'),
('71601'),
('90001'),
('80001'),
('06001'),
('19701'),
('32003'),
('30002'),
('96701'),
('83201'),
('60001'),
('46001'),
('50001'),
('66002'),
('40003'),
('70001'),
('03901'),
('20588'),
('01001'),
('48001'),
('55001'),
('38601'),
('63001'),
('59001'),
('68001'),
('88901'),
('03031'),
('07001'),
('87001'),
('00501'),
('27006'),
('58001'),
('43001'),
('73001'),
('97001'),
('15001'),
('02801'),
('29001'),
('57001'),
('37010'),
('73301'),
('84001'),
('05001'),
('20101'),
('98001'),
('24701'),
('53001'),
('82001');

-- Insertar datos en la tabla buyers
INSERT INTO buyers (name, contact) VALUES
('Comprador A', 'compradorA@gmail.com'),
('Comprador B', 'compradorB@gmail.com');

-- Insertar datos en la tabla quoteStatus
INSERT INTO quoteStatus (name, isRequiredDate) VALUES
('Pending Acceptance', 0),
('Accepted', 0),
('Picked Up', 1);

-- Insertar datos en la tabla cars
INSERT INTO cars (year, brandId, modelId, submodelId) VALUES
(2018, 1, 1, 1),--AUDI -A3 - BASE
(2022, 3, 21, 88), --BMW - 230 - BASE
(2023, 3, 23, 93),--BMW -320 - xDrive
(2023, 3, 21, 89); --BMW - 230 - BASE

-- Insertar cotizaciones para un mismo auto, simulando varias ofertas
INSERT INTO quotes (carId, buyerId, zipCodeId, amount, isCurrent, quoteStatusId) VALUES
-- Cotizaciones para el AUDI -A3 - BASE - 1
	(1,1,1,45000.00,1,1),
---- Cotizaciones para el BMW - 230 - BASE - 2
	(2,2,12,62000.00,0,2),
-- Cotizaciones para el BMW - 230 - BASE - 3
	(3,2,11,47000.00,1,1);

-- Insertar historial de estados para una cotizacion (simulando un cambio de estado)
INSERT INTO quoteStatusHistory (quoteId, quoteStatusId, modifyBy, quoteStatusDate) VALUES
(3,1,1,NULL),-- Estado Pending acceptance
(3,2,1,NULL), --Estado Accepted
(3,3,1,'2025-08-22');-- Estado Picked Up con fecha (es requerida)