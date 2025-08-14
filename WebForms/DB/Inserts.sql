USE Comercio_DB

GO
INSERT INTO Categorias (Nombre) VALUES
('Electrodomésticos'),   -- 1
('Audio'),               -- 2
('Informática'),         -- 3
('Gaming'),              -- 4
('Cocina'),              -- 5
('Telefonía'),           -- 6
('Televisores'),         -- 7
('Accesorios'),          -- 8
('Climatización'),       -- 9
('Hogar Inteligente'),   -- 10
('Cuidado Personal');    -- 11

GO
INSERT INTO TiposProducto (Nombre, IdCategoria) VALUES
-- ELECTRODOMÉSTICOS (1)
('Microondas', 1), -- 1
('Heladeras', 1), -- 2
('Lavarropas', 1), -- 3
('Calefactores', 1), -- 4
('Planchas a Vapor', 1), -- 5

-- AUDIO (2)
('Auriculares', 2), -- 6
('Home Theater', 2), -- 7
('Barras de Sonido', 2), -- 8
('Parlantes', 2), -- 9

-- INFORMÁTICA (3)
('Notebooks', 3), -- 10
('PC de Escritorio', 3), -- 11
('Monitores', 3), -- 12
('Impresoras', 3), -- 13
('Discos Externos', 3), -- 14
('Accesorios para PC', 3), -- 15
('Tablets', 3), -- 16

-- GAMING (4)
('Consolas', 4), -- 17
('Controles de Consola', 4), -- 18
('Sillas Gamer', 4), -- 19
('Auriculares Gamer', 4), -- 20
('Teclados Gamer', 4), -- 21
('Mouse Gamer', 4), -- 22

-- COCINA (5)
('Cafeteras', 5), -- 23
('Pavas Eléctricas', 5), -- 24
('Tostadoras', 5), -- 25
('Sandwicheras', 5), -- 26
('Procesadoras de Alimentos', 5), -- 27
('Batidoras', 5), -- 28
('Freidoras Eléctricas', 5), -- 29
('Hornos Eléctricos', 5), -- 30
('Cocinas Eléctricas', 5), -- 31
('Extractores de Jugo', 5), -- 32

-- TELEFONÍA (6)
('Celulares', 6), -- 33
('Smartwatches', 6), -- 34
('Tablets con 4G/5G', 6), -- 35
('Auriculares Bluetooth', 6), -- 36
('Cargadores', 6), -- 37
('Fundas para Celulares', 6), -- 38

-- TELEVISIÓN (7)
('Smart TV 4K', 7), -- 39
('Soportes para TV', 7), -- 40
('TV Box Android', 7), -- 41

-- ACCESORIOS (8)
('Cables HDMI', 8), -- 42
('Adaptadores', 8), -- 43
('Soportes para Celular', 8), -- 44
('Cables USB', 8), -- 45
('Memorias MicroSD', 8), -- 46

-- CLIMATIZACIÓN (9)
('Aire Acondicionado', 9), -- 47
('Ventiladores', 9), -- 48
('Estufas Eléctricas', 9), -- 49

-- HOGAR INTELIGENTE (10)
('Lámparas WiFi', 10), -- 50
('Enchufes Inteligentes', 10), -- 51
('Cámaras de Seguridad IP', 10), -- 52
('Asistentes Virtuales', 10), -- 53

-- CUIDADO PERSONAL (11)
('Afeitadoras Eléctricas', 11), -- 54
('Planchitas para el Pelo', 11), -- 55
('Secadores de Pelo', 11), -- 59
('Cepillos de Dientes Eléctricos', 11), -- 57
('Cortadoras de Pelo', 11), -- 58
('Balanzas Digitales', 11); -- 59

GO
INSERT INTO Marcas (Nombre) VALUES
('Samsung'),
('LG'),
('Sony'),
('Philips'),
('Lenovo'),
('HP'),
('Xiaomi'),
('Whirlpool'),
('Electrolux'),
('Asus');

GO
INSERT INTO Usuario (NombreUsuario, Nombre, Apellido, Email, Contrasena, FechaAlta, Admin) VALUES
('Ale', 'Alejandro', 'Olguera', 'ale@gmail.com', 'admin', '2025-05-30', 1),
('Fede', 'Federico', 'Fogliatto', 'fede@gmail.com', 'vendedor', '2025-05-29', 0);

----------------------- INSERTS 54 PRODUCTOS -----------------------
GO
INSERT INTO Productos (CodigoArticulo, Nombre, Descripcion, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, ImagenUrl, IdMarca, IdTipoProducto) VALUES
('SAMS-MWO23L', 'Microondas ME731K', 'Capacidad 23L, 800W, 6 niveles de potencia', 95000.00, 35.00, 10, 3, 'https://http2.mlstatic.com/D_NQ_NP_832347-MLA84549796072_052025-O.webp', 1, 1), -- Microondas
('WHIRL-LAV8K', 'Lavarropas WLF800', 'Automático, 8kg, carga frontal, 1200 rpm', 215000.00, 40.00, 7, 2, 'https://http2.mlstatic.com/D_NQ_NP_779808-MLU79129490555_092024-O.webp', 8, 3), -- Lavarropas
('SONY-PS5STD', 'PlayStation 5 Standard', '825GB SSD, control DualSense, 4K HDR', 550000.00, 25.00, 5, 1, 'https://http2.mlstatic.com/D_878229-MLA82556653763_022025-C.jpg', 3, 17), -- Consolas
('XIA-REDMI12', 'Redmi Note 12', '6.6", 128GB, 4GB RAM, cámara 50MP', 185000.00, 30.00, 15, 5, 'https://http2.mlstatic.com/D_Q_NP_775760-MLU78805224085_082024-O.webp', 7, 33), -- Celulares
('HP-LAP15D', 'Laptop 15-dw3000la', 'Intel i5, 8GB RAM, 512GB SSD, 15.6"', 350000.00, 28.00, 6, 2, 'https://pisces.bbystatic.com/image2/BestBuy_US/images/products/8c9f99d6-9214-46b5-935b-37005a6f696b.jpg', 6, 10), -- Notebooks
('LG-TV55UQ', 'Smart TV 55UQ7500', '55", 4K UHD, WebOS, HDR10 Pro', 385000.00, 30.00, 8, 3, 'https://www.lg.com/content/dam/channel/wcms/my/images/tvs/55uq7050psa_atsq_eaml_my_c/gallery/DZ-1.jpg', 2, 39), -- Smart TV 4K
('LEN-IDEA3', 'Notebook IdeaPad 3', 'Ryzen 5, 8GB, 512GB SSD, FHD', 330000.00, 30.00, 10, 4, 'https://http2.mlstatic.com/D_931147-MLA84766832402_052025-C.jpg', 5, 10), -- Notebooks
('PHIL-SPKBT', 'Parlante Bluetooth BT100', '20W, batería 8h, resistente al agua', 42000.00, 35.00, 12, 4, 'https://http2.mlstatic.com/D_941249-MLA50211827182_062022-O.jpg', 4, 9), -- Parlantes
('ASUS-ROGALLY', 'ROG Ally Z1 Extreme', 'Consola portátil, Ryzen Z1, 512GB SSD', 680000.00, 20.00, 3, 1, 'https://celularesindustriales.com.ar/wp-content/uploads/ally_ryzen_z101_l.jpg', 10, 17), -- Consolas
('LG-MIC20L', 'Microondas MS2042D', 'Capacidad 20L, 700W, manual', 82000.00, 35.00, 9, 2, 'https://www.lg.com/content/dam/channel/wcms/cl/images/microondas/ms2042d/gallery/MS2042DS%20door%20open.jpg', 2, 1); -- Microondas

GO
INSERT INTO Productos (CodigoArticulo, Nombre, Descripcion, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, ImagenUrl, IdMarca, IdTipoProducto) VALUES
('SAMS-GALTAB-A9', 'Tablet Galaxy Tab A9 64GB', 'Pantalla 8.7", Octa-Core, RAM 4GB, Android 13', 115000, 30.00, 8, 3, 'https://http2.mlstatic.com/D_NQ_NP_892038-MLU74328290469_012024-O.webp', 1, 16), -- Tablets
('HP-M24FHD', 'Monitor HP M24f', '23.8", FHD, IPS, HDMI/VGA, sin bordes', 75000, 35.00, 6, 2, 'https://www.hp.com/fr-fr/shop/Html/Merch/Images/c07056663_1750x1285.jpg', 6, 12), -- Monitores
('LG-AAC12W', 'Aire Acondicionado LG Inverter 3000W', 'Frío/Calor, eficiencia A+, split', 345000, 25.00, 4, 1, 'https://www.lg.com/ar/images/aire-acondicionado/us-h126eft0/gallery/large-02.jpg', 2, 47), -- Aire Acondicionado
('PHIL-PLANCHA4200', 'Plancha a vapor Philips 4200', 'Potencia 2400W, suela cerámica, vapor continuo', 35000, 30.00, 10, 3, 'https://images.fravega.com/f300/7d2f096dbc33f3bdb19b8e9c97a09e85.jpg', 4, 5), -- Planchas a Vapor
('SONY-WHCH520', 'Auriculares Bluetooth Sony WH-CH520', 'Con micrófono, batería 50h, compatible con asistentes', 52000, 35.00, 15, 4, 'https://mall.icbc.com.ar/33596004-large_default/auriculares-sony-bluetooth-inalambricos-wh-ch-520-negro.jpg', 3, 36), -- Auriculares Bluetooth
('XIA-11T-256', 'Smartphone Xiaomi 11T 256GB', '6.67", 8GB RAM, Cámara 108MP, 5G', 285000, 30.00, 7, 2, 'https://www.megatone.net/images/Articulos/zoom2x/209/MKT0867SEN-2.jpg', 7, 33), -- Celulares
('LEN-TAB-M10', 'Tablet Lenovo Tab M10 HD 64GB', '10.1", Android 11, Wi-Fi, cámara 8MP', 89000, 28.00, 9, 3, 'https://p4-ofp.static.pub/fes/cms/2023/02/22/6tq47f1v0lxg2bhvbqefq7wgngope9281905.png', 5, 16), -- Tablets
('ASUS-MON27VG', 'Monitor ASUS TUF 27" 165Hz', 'Resolución FHD, 1ms, HDMI/DP, G-Sync Compatible', 140000, 25.00, 4, 1, 'https://www.asus.com/media/global/products/vu6dtkhyjqxf93km/P_setting_xxx_0_90_end_500.png', 10, 12), -- Monitores
('WHIRL-MICGRILL20L', 'Microondas Whirlpool Grill 20L', '700W + grill, 6 niveles de potencia, blanco', 95000, 30.00, 5, 2, 'https://whirlpoolarg.vtexassets.com/arquivos/ids/165738/frente_cerrado.jpg', 8, 1), -- Microondas
('SAMS-TV32T4300', 'Smart TV Samsung 32” T4300', 'HD, HDR, Tizen OS, HDMI/USB, Wi-Fi', 165000, 32.00, 6, 2, 'https://d2pr1pn9ywx3vo.cloudfront.net/spree/products/20500/large/sam32t4300_primera_con_logo.jpg', 1, 39); -- Smart TV 4K

GO
INSERT INTO Productos (CodigoArticulo, Nombre, Descripcion, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, ImagenUrl, IdMarca, IdTipoProducto) VALUES
('ELECT-MIC23L', 'Microondas Electrolux MEF23', '23L, 800W, 6 niveles de potencia, blanco', 88000, 32.00, 8, 2, 'https://http2.mlstatic.com/D_NQ_NP_896968-MLA52389830543_112022-O.webp', 9, 1),
('WHIRL-HEL340', 'Heladera Whirlpool WRM34', '340L, No Frost, freezer superior, blanca', 390000, 28.00, 5, 1, 'https://http2.mlstatic.com/D_609066-MLU77451603578_072024-C.jpg', 8, 2),
('SONY-MDRZX110', 'Auriculares Sony MDR-ZX110', 'Diadema, cable 1.2m, con micrófono', 18000, 35.00, 20, 5, 'https://http2.mlstatic.com/D_Q_NP_601841-MLU78228664220_082024-O.webp', 3, 6),
('LG-HT805', 'Home Theater LG HT805', '5.1 canales, Bluetooth, HDMI ARC', 145000, 30.00, 4, 1, 'https://www.lg.com/ae/images/home-theater-systems/ht805st/gallery/medium01.jpg', 2, 7),
('HP-X24C', 'Monitor Curvo HP X24c', '23.6", FHD, 144Hz, FreeSync', 130000, 25.00, 6, 2, 'https://thumb.pccomponentes.com/w-530-530/articles/32/324659/3296-hp-x24c-236-led-fullhd-144hz-freesync-curvo-mejor-precio.jpg', 6, 12),
('ASUS-ROGCHAIR', 'Silla Gamer ASUS ROG Chariot', 'Reposabrazos 4D, respaldo reclinable, color negro', 220000, 30.00, 3, 1, 'https://core.gamerfactory.com.ar/static/images/d3595970-a428-11ef-a1b9-3bf8c9f3636f.webp', 10, 19),
('PHIL-CAFHD7432', 'Cafetera Philips HD7432', 'Filtro permanente, capacidad 1.2L, 1000W', 35000, 28.00, 12, 3, 'https://gw.alicdn.com/imgextra/O1CN010ohM9G1w82u0KsL01_!!6000000006262-0-yinhe.jpg_540x540.jpg', 4, 23),
('SAMS-GALW5', 'Galaxy Watch5 44mm', 'AMOLED, GPS, resistencia 5ATM', 165000, 30.00, 8, 2, 'https://multipoint.com.ar/Image/0/750_750-SM-R910NZBAARO-2.jpg', 1, 34),
('LG-AAC18INV', 'Aire Acondicionado LG Dual Inverter 4500W', 'Frío/Calor, eficiencia A++, WiFi', 480000, 25.00, 4, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSgFKqT10a8leFFV8HnRBnlBlOMWvao43B2o0eRs7WDTGLikwe3-DAozoQnBFZELYcM-ok&usqp=CAU', 2, 47),
('PHIL-AFET3000', 'Afeitadora Philips Series 3000', 'Cuchillas PowerCut, batería 60 min', 55000, 28.00, 10, 3, 'https://http2.mlstatic.com/D_659533-MLA75704626288_042024-C.jpg', 4, 54),
('LG-HEL420', 'Heladera LG GN-B420', '394L, No Frost, freezer superior, color silver', 450000, 28.00, 4, 1, 'https://www.lg.com/ar/images/heladeras/md06051016/gallery/D01.jpg', 2, 2),
('SAMS-LAV9K', 'Lavarropas Samsung WW90J5455FW', '9kg, carga frontal, 1400 rpm, Inverter', 380000, 30.00, 6, 2, 'https://http2.mlstatic.com/D_NQ_NP_624791-MLU70065154747_062023-O.webp', 1, 3),
('WHIRL-MIC25L', 'Microondas Whirlpool Magicook 25L', '25L, 900W, grill, display digital', 112000, 32.00, 6, 2, 'https://www.megatone.net/Images/Articulos/zoom2x/32/COC2520PHI.jpg', 8, 1),
('ELECT-PLANCHA3500', 'Plancha a Vapor Electrolux Easyline', 'Potencia 2200W, vapor continuo, suela cerámica', 32000, 30.00, 9, 2, 'https://http2.mlstatic.com/D_NQ_NP_640872-MLB41286286198_032020-O.webp', 9, 5),
('PHIL-CALEF3000', 'Calefactor Philips HeatFlow', '1500W, silencioso, doble potencia', 58000, 30.00, 8, 2, 'https://images.philips.com/is/image/philipsconsumer/vrs_42611a905931f5cb25f971a049d449358d6e5d05?$pnglarge$&wid=1250', 4, 4),
('SAMS-SOUNDBAR-T420', 'Barra de Sonido Samsung HW-T420', 'Potencia 150W, Bluetooth, Subwoofer inalámbrico', 110000, 30.00, 5, 2, 'https://www.todo-vision.com.ar/Image/0/500_500-descarga%20(3).jpeg', 1, 8),
('LEN-PCM90', 'Lenovo IdeaCentre 3', 'Ryzen 5, 8GB RAM, 1TB HDD, Windows 11', 280000, 28.00, 4, 1, 'https://http2.mlstatic.com/D_NQ_NP_699949-MLA50696477595_072022-O.webp', 5, 11),
('HP-DESK3775', 'Impresora HP DeskJet 3775', 'Multifunción WiFi, impresión móvil, inyección térmica', 75000, 30.00, 6, 2, 'https://ar-media.hptiendaenlinea.com/wysiwyg/landings/deskjet-3775/hp-deskjet-3700-innovaciones.jpg', 6, 13),
('SEG-EXT1TB', 'Seagate Expansion 1TB', 'USB 3.0, formato portátil, color negro', 60000, 28.00, 10, 3, 'https://www.cordobadigital.net/wp-content/uploads/2020/07/disco-duro-1tb-externo-seagate-expansion-25-usb-30-compatible-pc-mac-pn3eeap1-570.jpg', 10, 14),
('ASUS-GAMHEADSET', 'Auriculares Gamer ASUS TUF H3', 'Sonido envolvente, micrófono retráctil, compatibles PC/PS4', 42000, 30.00, 8, 2, 'https://http2.mlstatic.com/D_NQ_NP_935638-MLU54958609885_042023-O.webp', 10, 20),
('LOGI-GAMKEYG213', 'Teclado Logitech G213 Prodigy', 'RGB, resistencia salpicaduras, teclas multimedia', 38000, 30.00, 9, 3, 'https://http2.mlstatic.com/D_NQ_NP_801830-MLA81204444560_122024-O.webp', 4, 21),
('PHIL-AIRFRYERHD9200', 'Freidora de aire Philips HD9200', 'Capacidad 4.1L, tecnología Rapid Air', 105000, 28.00, 6, 2, 'https://tccommercear.vtexassets.com/arquivos/ids/158014-800-auto?v=638749891634800000&width=800&height=auto&aspect=true', 4, 29),
('ELECT-HOR60L', 'Horno Eléctrico Electrolux 60L', 'Temporizador, convección, grill superior', 145000, 28.00, 3, 1, 'https://http2.mlstatic.com/D_NQ_NP_951476-MLA49632731441_042022-O.webp', 9, 30),
('AMAZ-ECHODOT5', 'Amazon Echo Dot 5ta Gen', 'Alexa, WiFi, Bluetooth, control por voz', 68000, 28.00, 10, 3, 'https://http2.mlstatic.com/D_NQ_NP_746297-MLA79401219670_092024-O.webp', 4, 53),
('LG-SOUNDBAR-SN4', 'Barra de Sonido LG SN4', '300W, subwoofer inalámbrico, Bluetooth', 120000, 28.00, 5, 2, 'https://dcdn-us.mitiendanube.com/stores/004/616/743/products/barra-de-sonido-sn4-lg-3webp-b1c755b78db3aa559217316914006313-1024-1024.jpg', 2, 8),
('SONY-INZONEH3', 'Auriculares Sony INZONE H3', 'Sonido envolvente 360, micrófono, PC/PS5', 95000, 30.00, 6, 2, 'https://http2.mlstatic.com/D_NQ_NP_998584-MLA84838335465_052025-O.webp', 3, 20),
('ASUS-STRIXFLARE', 'Teclado ASUS ROG Strix Flare', 'Switches mecánicos Cherry MX, RGB', 135000, 28.00, 4, 1, 'https://http2.mlstatic.com/D_NQ_NP_618951-MLA79639062838_102024-O.webp', 10, 21),
('ASUS-ROGGLADIUS', 'Mouse Gamer ASUS ROG Gladius III', '19000 DPI, RGB, cable desmontable', 88000, 30.00, 8, 2, 'https://http2.mlstatic.com/D_NQ_NP_837594-MLU54982228569_052023-O.webp', 10, 22),
('PHIL-FOODPRO700W', 'Procesadora Philips HR7630', '700W, 2 velocidades, 1.5L', 75000, 28.00, 5, 2, 'https://http2.mlstatic.com/D_NQ_NP_646905-MLA25084337461_102016-O.webp', 4, 27),
('PHIL-BAT700W', 'Batidora Philips Viva HR3745', '700W, 5 velocidades, bowl giratorio', 56000, 28.00, 6, 2, 'https://images.tiendavolar.com.uy/medium/P501738-8.jpg?20221122171552,Batidora-PHILIPS-HR3745-en-Itau', 4, 28),
('PHIL-JUICER500W', 'Extractor de jugo Philips HR1855', '700W, tecnología QuickClean', 89000, 28.00, 4, 1, 'https://jumboargentina.vtexassets.com/arquivos/ids/585145/Juguera-Philips-Hr1855-00-1-4398.jpg?v=637251960300330000', 4, 32),
('XIA-TVBOXS', 'Xiaomi Mi Box S', 'Android TV, 4K HDR, Chromecast integrado', 80000, 28.00, 10, 3, 'https://http2.mlstatic.com/D_807700-MLA84320554212_052025-C.jpg', 7, 41),
('XIA-CAM360', 'Xiaomi Mi 360 Camera 2K', 'Visión 360°, detección de movimiento, WiFi', 49000, 30.00, 7, 2, 'https://acdn-us.mitiendanube.com/stores/001/844/364/products/mi-360-31-723e565f2af83357ef16875294517429-1024-1024.png', 7, 52),
('XIA-PLUGSMART', 'Xiaomi Smart Plug', 'Control remoto, temporizador, compatible Alexa', 25000, 28.00, 12, 3, 'https://http2.mlstatic.com/D_NQ_NP_618175-MLA50745040104_072022-O.webp', 7, 51);

----------------------- FIN PRODUCTOS -----------------------

GO
INSERT INTO Proveedores (RazonSocial, CUIT, Direccion, Telefono, Email) VALUES
('Tech Global S.A.', '20-34218594-7', 'Av. Rivadavia 4530', '011-4555-1234', 'contacto@techglobal.com'),
('ElectroHouse', '30-28574910-3', 'Ruta 8 Km 45', '02320-478911', 'ventas@electrohouse.com'),
('Distribuidora Gama', '23-94837210-5', 'Calle Falsa 123', '0341-4234567', 'info@dgama.com'),
('Nova Distribución', '27-10293847-2', 'San Martín 550', '011-4789-3344', 'contacto@nova.com.ar'),
('Supreme Tech', '20-38475629-1', 'Av. Santa Fe 3456', '011-4000-9999', 'ventas@supremetech.com'),
('Industrias Celta', '30-92837465-0', 'Roca 980', '0341-4567890', 'info@celta.com'),
('Grupo Andes', '27-91837462-6', 'Av. Las Heras 3300', '011-4300-5678', 'ventas@grupoandes.com'),
('MegaDigital', '20-93746183-3', 'Boulevard Oroño 147', '0341-4892371', 'info@megadigital.com'),
('NorteSur S.A.', '30-11223344-9', 'Alberdi 2876', '011-4875-1233', 'contacto@nortesur.com'),
('Litoral Distribuciones', '23-81937465-5', 'Av. Pellegrini 1010', '0341-4000123', 'ventas@litoral.com'),
('ElectroDelta', '27-82736455-1', 'Córdoba 789', '0342-4761111', 'info@electrodelta.com'),
('Tecno Mundo', '20-74829475-0', 'Ituzaingó 984', '011-47778987', 'ventas@tecnomundo.com'),
('Distribuidora del Sur', '30-11198273-4', 'Av. Calchaquí 3001', '011-42001233', 'info@surdistribucion.com'),
('FullTech', '27-39284756-9', 'Ruta 3 Km 60', '011-43009876', 'ventas@fulltech.com'),
('Galaxy Proveedores', '23-84736291-2', 'Santa Fe 2340', '0341-4210987', 'galaxy@proveedores.com'),
('Red Zona S.A.', '20-77788899-1', 'Av. Mitre 4450', '011-44004400', 'info@redzona.com'),
('TecnoRed', '30-93847561-7', 'Cuyo 1234', '011-48947812', 'ventas@tecnored.com'),
('ElectroMax', '27-82736519-2', 'Castelli 1500', '0341-4321987', 'ventas@emax.com'),
('Digitronix', '23-29384756-8', 'San Juan 6700', '011-43009000', 'info@digitronix.com'),
('Proveedores Argentinos', '20-99988877-0', 'Av. Corrientes 900', '011-47881234', 'contacto@pa.com.ar');

GO
INSERT INTO Compras (Fecha, IdProveedor, IdUsuario, Total) VALUES
('2024-08-14 02:03:01', 1, 1, 1326000.00),
('2024-10-13 14:48:11', 2, 1, 2245000.00),
('2024-09-15 17:45:45', 3, 1, 4153000.00);
--('2024-12-07 11:33:28', 10, 438404.18),
--('2025-01-23 03:25:33', 7, 903514.71),
--('2024-11-09 07:16:24', 9, 1616769.95),
--('2024-08-07 14:34:41', 6, 1673307.59),
--('2024-10-17 18:00:44', 11, 244355.71),
--('2024-06-14 20:37:52', 13, 1022092.87),
--('2024-11-25 10:15:47', 12, 1433852.58),
--('2025-05-12 15:51:50', 20, 994707.84),
--('2024-12-14 04:43:24', 17, 1950941.98),
--('2024-07-01 02:41:36', 15, 1417598.52),
--('2024-07-19 06:55:14', 4, 1011162.43),
--('2024-06-30 21:11:48', 14, 1012055.47),
--('2024-09-18 12:22:34', 19, 621874.16),
--('2025-01-09 23:18:39', 18, 1273981.03),
--('2024-08-21 00:02:17', 8, 522503.88),
--('2024-11-16 17:21:22', 5, 725237.27),
--('2024-10-04 22:38:36', 16, 1564219.34);

GO
INSERT INTO CompraDetalle (IdCompra, IdProducto, Cantidad, PrecioUnit) VALUES
(1, 8, 8, 42000.00),
(1, 20, 6, 165000.00),
(2, 10, 7, 82000.00),
(2, 3, 5, 550000.00),
(2, 1, 9, 95000.00),
(3, 20, 5, 165000.00),
(3, 4, 8, 185000.00);
--(4, 1, 5, 95000.00),  -- Microondas ME731K
--(4, 2, 3, 215000.00), -- Lavarropas WLF800
--(5, 3, 2, 275000.00), -- PlayStation 5 Standard
--(5, 4, 4, 185000.00), -- Redmi Note 12
--(6, 5, 1, 350000.00), -- Laptop 15-dw3000la
--(6, 6, 2, 385000.00), -- Smart TV 55UQ7500
--(7, 7, 6, 42000.00),  -- Parlante Bluetooth BT100
--(7, 8, 3, 680000.00), -- ROG Ally Z1 Extreme
--(8, 9, 5, 82000.00),  -- Microondas MS2042D
--(8, 10, 2, 115000.00), -- Tablet Galaxy Tab A9 64GB
--(9, 11, 4, 75000.00),  -- Monitor HP M24f
--(9, 12, 1, 345000.00), -- Aire Acondicionado LG Inverter 3000W
--(10, 13, 3, 35000.00),  -- Plancha a vapor Philips 4200
--(10, 14, 2, 52000.00),  -- Auriculares Bluetooth Sony WH-CH520
--(11, 15, 7, 285000.00), -- Smartphone Xiaomi 11T 256GB
--(11, 16, 4, 89000.00),  -- Tablet Lenovo Tab M10 HD 64GB
--(12, 17, 5, 140000.00), -- Monitor ASUS TUF 27" 165Hz
--(12, 18, 2, 95000.00),  -- Microondas Whirlpool Grill 20L
--(13, 19, 6, 165000.00), -- Smart TV Samsung 32” T4300
--(13, 20, 1, 215000.00), -- Lavarropas WLF800
--(14, 1, 3, 95000.00);   -- Microondas ME731K

GO
INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion) VALUES
('Juan', 'Pérez', '30111222', '1122334455', 'juan.perez@mail.com', 'Av. Siempre Viva 123'),
('María', 'Gómez', '29333444', '1133445566', 'maria.gomez@mail.com', 'Calle Falsa 456'),
('Carlos', 'Lopez', '28444555', '1144556677', 'carlos.lopez@mail.com', 'Ruta 9 Km 23');

GO
INSERT INTO Ventas (Fecha, IdCliente, IdUsuario, Total) VALUES
(GETDATE(), 1, 1, 268650.00),  -- Juan
(GETDATE(), 2, 1, 1006550.00),  -- María
(GETDATE(), 3, 2, 1297000.00);  -- Carlos

GO
-- Venta 1: Juan compró 1 microondas y 2 auriculares
INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnit) VALUES
(1, 1, 1, 128250.00),  -- Microondas ME731K (95000 + 35%)
(1, 15, 2, 70200.00),  -- Auriculares WH-CH520 (52000 + 35%)
-- Venta 2: María compró 1 PS5, 1 TV Samsung 32” y 1 monitor HP
(2, 3, 1, 687500.00), -- PS5 Standard (550000 + 25%)
(2, 20, 1, 217800.00), -- Samsung TV 32” (165000 + 32%)
(2, 13, 1, 101250.00), -- Monitor HP M24f (75000 + 35%)
-- Venta 3: Carlos compró 1 ROG Ally y 2 celulares Redmi Note 12
(3, 9, 1, 816000.00), -- ROG Ally Z1 Extreme (680000 + 20%)
(3, 4, 2, 240500.00); -- Redmi Note 12 (185000 + 30%)

GO

INSERT into Comisiones (IdVenta,IdUsuario,PorcentajeAplicado,MontoComision,Fecha) VALUES 
(1,1,5.00,13432.50,'2025-07-06'),
(2,1,5.00,50327.50,'2025-07-06'),
(3,2,10.00,129700.00,'2025-07-06')
