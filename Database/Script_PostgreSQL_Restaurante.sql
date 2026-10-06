-- =============================================================================
-- RESTAURANTE EL BUEN SAZÓN — SCRIPT DE BASE DE DATOS POSTGRESQL (TÍPICO PANAMEÑO)
-- =============================================================================
-- Este script crea la estructura de tablas y datos iniciales con gastronomía panameña
-- para el servidor de PostgreSQL de la Universidad.
-- =============================================================================

-- 1. ELIMINACIÓN DE TABLAS SI YA EXISTEN (MODO LIMPIO)
DROP TABLE IF EXISTS detalle_pedidos CASCADE;
DROP TABLE IF EXISTS pedidos CASCADE;
DROP TABLE IF EXISTS platos CASCADE;
DROP TABLE IF EXISTS categorias CASCADE;
DROP TABLE IF EXISTS usuarios CASCADE;

-- 2. TABLA DE USUARIOS Y ROLES (ADMIN, CAJERO, COCINA, CLIENTE)
CREATE TABLE usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(100) NOT NULL,
    nombre_completo VARCHAR(100) NOT NULL,
    rol VARCHAR(30) NOT NULL CHECK (rol IN ('Administrador', 'Cajero', 'Cocina', 'Cliente')),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. TABLA DE CATEGORÍAS DEL MENÚ
CREATE TABLE categorias (
    id_categoria SERIAL PRIMARY KEY,
    nombre_categoria VARCHAR(50) NOT NULL UNIQUE,
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

-- 4. TABLA DE PLATOS Y PRODUCTOS
CREATE TABLE platos (
    id_plato SERIAL PRIMARY KEY,
    nombre_plato VARCHAR(100) NOT NULL,
    id_categoria INT NOT NULL REFERENCES categorias(id_categoria) ON DELETE RESTRICT,
    precio NUMERIC(10, 2) NOT NULL CHECK (precio >= 0),
    tiempo_preparacion VARCHAR(20) DEFAULT '15 min',
    disponible BOOLEAN DEFAULT TRUE,
    descripcion TEXT,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 5. TABLA DE PEDIDOS (CABECERA - CAJA, COCINA Y FACTURACIÓN)
CREATE TABLE pedidos (
    id_pedido SERIAL PRIMARY KEY,
    nombre_cliente VARCHAR(100) NOT NULL,
    mesa_o_servicio VARCHAR(30) NOT NULL,
    tipo_servicio VARCHAR(30) NOT NULL CHECK (tipo_servicio IN ('En Mesa', 'Para Llevar', 'Delivery')),
    estado VARCHAR(30) DEFAULT 'Pendiente' CHECK (estado IN ('Pendiente', 'En Preparacion', 'Listo', 'Pagado', 'Cancelado')),
    total NUMERIC(10, 2) DEFAULT 0.00,
    monto_recibido NUMERIC(10, 2) DEFAULT 0.00,
    cambio NUMERIC(10, 2) DEFAULT 0.00,
    metodo_pago VARCHAR(30) DEFAULT 'Efectivo',
    fecha_hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_cobro TIMESTAMP,
    facturado BOOLEAN DEFAULT FALSE,
    numero_factura VARCHAR(50),
    ruc_cedula VARCHAR(30),
    razon_social VARCHAR(100),
    direccion_fiscal TEXT,
    telefono_cliente VARCHAR(30),
    correo_cliente VARCHAR(100)
);

-- 6. TABLA DE DETALLE DE PEDIDOS (ITEMS DEL PEDIDO)
CREATE TABLE detalle_pedidos (
    id_detalle SERIAL PRIMARY KEY,
    id_pedido INT NOT NULL REFERENCES pedidos(id_pedido) ON DELETE CASCADE,
    id_plato INT NOT NULL REFERENCES platos(id_plato) ON DELETE RESTRICT,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario NUMERIC(10, 2) NOT NULL,
    acompanamientos TEXT,
    subtotal NUMERIC(10, 2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED
);

-- =============================================================================
-- INSERCIÓN DE DATOS INICIALES (GASTRONOMÍA PANAMEÑA AUTÉNTICA)
-- =============================================================================

-- 1. USUARIOS DEL SISTEMA
INSERT INTO usuarios (nombre_usuario, password_hash, nombre_completo, rol) VALUES
('admin', 'admin123', 'Administrador General', 'Administrador'),
('cajero', 'cajero123', 'Cajero de Turno', 'Cajero'),
('cocina', 'cocina123', 'Jefe de Cocina KDS', 'Cocina'),
('cliente', 'cliente123', 'Carlos Mendoza', 'Cliente');

-- 2. CATEGORÍAS AUTÉNTICAS
INSERT INTO categorias (nombre_categoria, descripcion) VALUES
('Desayunos', 'Frituras típicas panameñas, hojaldres, carimañolas y tortillas'),
('Almuerzos', 'Platos tradicionales panameños, sancocho, pescado frito y ropa vieja'),
('Cenas', 'Bistec picado, corvina a la tipileña y especialidades del istmo');

-- 3. CATÁLOGO INICIAL DE PLATOS PANAMEÑOS
INSERT INTO platos (nombre_plato, id_categoria, precio, tiempo_preparacion, disponible, descripcion) VALUES
('Hojaldre con Queso Blanco y Salchicha', 1, 3.50, '10 min', TRUE, 'Hojaldre frita crujiente acompañada de queso blanco artesanal y salchichas guisadas.'),
('Carimañola de Carne Molida', 1, 2.50, '8 min', TRUE, 'Fritura tradicional de yuca rellena de carne molida sazonada al estilo panameño.'),
('Tortilla de Maíz con Chicharrón', 1, 3.00, '10 min', TRUE, 'Tortilla de maíz amarillo asada a la leña servida con chicharrón crujiente.'),
('Tamal Panameño en Hoja de Bijao', 1, 4.00, '12 min', TRUE, 'Tamal de maíz pilado relleno de pollo guisado, aceitunas, alcaparras y pasas.'),
('Pescado Frito con Patacones', 2, 10.50, '18 min', TRUE, 'Pescado entero frito al punto dorado servido con patacones crujientes y ensalada de feria.'),
('Sancocho Panameño de Gallina Criolla', 2, 7.50, '15 min', TRUE, 'Sancocho tradicional de gallina de patio con yuca, ñame, culantro y arroz blanco.'),
('Ropa Vieja con Arroz con Guandú', 2, 8.50, '15 min', TRUE, 'Carne desmechada en salsa criolla acompañada de arroz con guandú de olor y plátano tentación.'),
('Arroz con Pollo y Ensalada de Feria', 2, 6.50, '12 min', TRUE, 'Arroz con pollo sazonado con vegetales frescos y ensalada roja de remolacha.'),
('Bistec Picado con Hojaldres Calientes', 3, 7.00, '12 min', TRUE, 'Tiras de carne de res salteadas con cebolla y pimentón servidas con hojaldres recién fritas.'),
('Corvina a la Tipileña con Patacones', 3, 11.00, '20 min', TRUE, 'Filete de corvina en salsa de tomate criollo, ají chombo y especias panameñas.'),
('Lengua Guisada con Arroz y Tajadas', 3, 8.00, '15 min', TRUE, 'Lengua de res tierna guisada en vino y vegetales con tajadas de plátano maduro.'),
('Saao de Cerdo con Yuca al Mojo', 3, 6.00, '12 min', TRUE, 'Cerdo frito en trozos sazonado con ajo y limón servido con yuca suave al mojo.');

-- 4. PEDIDOS DEMOSTRATIVOS DE PRUEBA (PARA CAJA, FACTURACIÓN Y COCINA)
INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, total, monto_recibido, cambio, metodo_pago, facturado, numero_factura, ruc_cedula, razon_social, direccion_fiscal, telefono_cliente, correo_cliente) VALUES
('Carlos Mendoza', 'Mesa 04', 'En Mesa', 'Pagado', 10.50, 20.00, 9.50, 'Efectivo', TRUE, 'FAC-2026-1001', '8-800-1234', 'Carlos Mendoza', 'Panamá, Bella Vista', '+507 6200-1122', 'carlos.mendoza@email.com'),
('María Fernández', 'Mesa 02', 'En Mesa', 'Pendiente', 7.50, 0.00, 0.00, 'Pendiente', FALSE, NULL, NULL, NULL, NULL, NULL, NULL),
('Roberto Gómez', 'Mesa 09', 'En Mesa', 'Pendiente', 8.50, 0.00, 0.00, 'Pendiente', FALSE, NULL, NULL, NULL, NULL, NULL, NULL),
('Ana Lucía Torres', 'Llevar', 'Para Llevar', 'Pagado', 7.00, 10.00, 3.00, 'Efectivo', TRUE, 'FAC-2026-1002', '8-750-5678', 'Ana Lucía Torres', 'Penonomé, Coclé', '+507 6555-8899', 'ana.torres@email.com');

INSERT INTO detalle_pedidos (id_pedido, id_plato, cantidad, precio_unitario, acompanamientos) VALUES
(1, 5, 1, 10.50, 'Patacones Extra, Chicha de Nance'),
(2, 6, 1, 7.50, 'Chicha de Limón con Raspadura'),
(3, 7, 1, 8.50, 'Plátano tentación'),
(4, 1, 2, 3.50, 'Empacado térmico');
