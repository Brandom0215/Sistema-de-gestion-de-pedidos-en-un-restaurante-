-- =============================================================================
-- RESTAURANTE EL BUEN SAZÓN — SCRIPT DE BASE DE DATOS POSTGRESQL
-- =============================================================================
-- Este script crea la estructura de tablas y datos iniciales para el servidor
-- de PostgreSQL de la Universidad.
-- =============================================================================

-- 1. CREACIÓN DE LA BASE DE DATOS (Ejecutar independientemente si se requiere)
-- CREATE DATABASE restaurante_db WITH OWNER = postgres ENCODING = 'UTF8';
-- \c restaurante_db;

-- 2. ELIMINACIÓN DE TABLAS SI YA EXISTEN (MODO LIMPIO)
DROP TABLE IF EXISTS detalle_pedidos CASCADE;
DROP TABLE IF EXISTS pedidos CASCADE;
DROP TABLE IF EXISTS platos CASCADE;
DROP TABLE IF EXISTS categorias CASCADE;
DROP TABLE IF EXISTS usuarios CASCADE;

-- 3. TABLA DE USUARIOS Y ROLES (ADMIN, CAJERO, COCINA, CLIENTE)
CREATE TABLE usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(100) NOT NULL,
    nombre_completo VARCHAR(100) NOT NULL,
    rol VARCHAR(30) NOT NULL CHECK (rol IN ('Administrador', 'Cajero', 'Cocina', 'Cliente')),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 4. TABLA DE CATEGORÍAS DEL MENÚ
CREATE TABLE categorias (
    id_categoria SERIAL PRIMARY KEY,
    nombre_categoria VARCHAR(50) NOT NULL UNIQUE,
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

-- 5. TABLA DE PLATOS Y PRODUCTOS
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

-- 6. TABLA DE PEDIDOS (CABECERA)
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
    fecha_hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 7. TABLA DE DETALLE DE PEDIDOS (ITEMS DEL PEDIDO)
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
-- INSERCIÓN DE DATOS INICIALES (DEMO Y AUTENTICACIÓN)
-- =============================================================================

-- 1. USUARIOS DEL SISTEMA
INSERT INTO usuarios (nombre_usuario, password_hash, nombre_completo, rol) VALUES
('admin', 'admin123', 'Administrador General', 'Administrador'),
('cajero', 'cajero123', 'Cajero de Turno', 'Cajero'),
('cocina', 'cocina123', 'Jefe de Cocina KDS', 'Cocina'),
('cliente', 'cliente123', 'Carlos Mendoza', 'Cliente');

-- 2. CATEGORÍAS PRINCIPALES DEL RESTAURANTE
INSERT INTO categorias (nombre_categoria, descripcion) VALUES
('Desayunos', 'Platos tradicionales para iniciar el día'),
('Almuerzos', 'Especialidades criollas y platos fuertes del día'),
('Cenas', 'Platos gourmet, pastas y carnes a la parrilla'),
('Platos Armados', 'Combos y platos combinados especiales'),
('Bebidas y Sodas', 'Jugos naturales, refrescos, sodas y bebidas de la casa');

-- 3. CATÁLOGO INICIAL DE PLATOS
INSERT INTO platos (nombre_plato, id_categoria, precio, tiempo_preparacion, disponible, descripcion) VALUES
('Mangú Tres Golpes Tradicional', 1, 350.00, '10 min', TRUE, 'Plátano verde majado con queso frito, salami induveca y huevo.'),
('Sancocho Criollo Gourmet', 2, 450.00, '15 min', TRUE, 'Sancocho dominicano de 7 carnes con víveres y arroz blanco.'),
('Chivo Liniero Guisado', 2, 650.00, '20 min', TRUE, 'Chivo tierno sazonado con orégano silvestre y yuca al mojo.'),
('Mofongo Especial El Buen Sazón', 2, 550.00, '12 min', TRUE, 'Mofongo de plátano con chicharrón crujiente y caldo de la casa.'),
('Fettuccine a la Huancaína con Lomo', 3, 580.00, '15 min', TRUE, 'Pastas artesanales en salsa huancaína con tiras de lomo salteado.'),
('Combo Familiar Platos Armados', 4, 1200.00, '25 min', TRUE, 'Pollo horneado, arroz moro, papas fritas, ensalada y jarra de jugo.'),
('Jarra de Jugo Natural de Chinola', 5, 200.00, '5 min', TRUE, 'Jarra de 1 litro de jugo natural de chinola recién hecho.'),
('Soda Artesanal de la Casa', 5, 120.00, '3 min', TRUE, 'Refresco sabor a frutos rojos con agua con gas y menta.');

-- 4. PEDIDOS DEMOSTRATIVOS DE PRUEBA
INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, total, monto_recibido, cambio, metodo_pago) VALUES
('Carlos Mendoza', 'Mesa 04', 'En Mesa', 'Pagado', 450.00, 500.00, 50.00, 'Efectivo'),
('María Fernández', 'Mesa 02', 'En Mesa', 'Pendiente', 550.00, 0.00, 0.00, 'Pendiente'),
('Roberto Gómez', 'Delivery', 'Delivery', 'En Preparacion', 650.00, 0.00, 0.00, 'Pendiente');

INSERT INTO detalle_pedidos (id_pedido, id_plato, cantidad, precio_unitario, acompanamientos) VALUES
(1, 2, 1, 450.00, 'Arroz blanco, Aguacate'),
(2, 4, 1, 550.00, 'Salsas de la casa'),
(3, 3, 1, 650.00, 'Yuca al mojo');
