-- =============================================================================
-- RESTAURANTE EL BUEN SAZÓN — SCRIPT DE BASE DE DATOS POSTGRESQL (TÍPICO PANAMEÑO)
-- =============================================================================
-- Estructura de tablas unificada: Pedidos, Cocina KDS, Caja, Facturación
-- y Microservicio Contable & Fiscal para Panamá (DGI / NIIF PYMES - Monolocal).
-- =============================================================================

-- 1. ELIMINACIÓN DE TABLAS SI YA EXISTEN (MODO LIMPIO)
DROP TABLE IF EXISTS auditoria_contable CASCADE;
DROP TABLE IF EXISTS asiento_detalles CASCADE;
DROP TABLE IF EXISTS asientos_contables CASCADE;
DROP TABLE IF EXISTS movimientos_caja CASCADE;
DROP TABLE IF EXISTS empleados CASCADE;
DROP TABLE IF EXISTS catalogo_cuentas CASCADE;
DROP TABLE IF EXISTS periodos_contables CASCADE;
DROP TABLE IF EXISTS clientes_fiscales CASCADE;
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

-- 5. TABLA DE CLIENTES FISCALES UNIFICADOS (ANTI-DUPLICADOS DGI - RUC/CÉDULA)
CREATE TABLE clientes_fiscales (
    id_cliente_fiscal SERIAL PRIMARY KEY,
    ruc_cedula VARCHAR(30) NOT NULL UNIQUE,
    dv VARCHAR(5),
    tipo_persona VARCHAR(20) NOT NULL DEFAULT 'NATURAL' CHECK (tipo_persona IN ('NATURAL', 'JURIDICA', 'EXTRANJERO', 'CONSUMIDOR_FINAL')),
    razon_social VARCHAR(120) NOT NULL,
    direccion_fiscal VARCHAR(150),
    telefono VARCHAR(30),
    correo VARCHAR(100),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE
);

-- 6. TABLA DE PEDIDOS (CABECERA - CAJA, COCINA Y FACTURACION)
CREATE TABLE pedidos (
    id_pedido SERIAL PRIMARY KEY,
    id_cliente_fiscal INT REFERENCES clientes_fiscales(id_cliente_fiscal) ON DELETE SET NULL,
    nombre_cliente VARCHAR(100) NOT NULL,
    mesa_o_servicio VARCHAR(30) NOT NULL,
    tipo_servicio VARCHAR(30) NOT NULL CHECK (tipo_servicio IN ('En Mesa', 'Para Llevar', 'Delivery')),
    estado VARCHAR(30) DEFAULT 'Pendiente' CHECK (estado IN ('Pendiente', 'En Preparacion', 'Listo', 'Pagado', 'Cancelado')),
    estado_cocina VARCHAR(30) DEFAULT 'RECIBIDO' CHECK (estado_cocina IN ('RECIBIDO', 'EN_PREPARACION', 'LISTO', 'ENTREGADO')),
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

-- 7. TABLA DE DETALLE DE PEDIDOS (ITEMS DEL PEDIDO)
CREATE TABLE detalle_pedidos (
    id_detalle SERIAL PRIMARY KEY,
    id_pedido INT NOT NULL REFERENCES pedidos(id_pedido) ON DELETE CASCADE,
    id_plato INT REFERENCES platos(id_plato) ON DELETE RESTRICT,
    nombre_plato VARCHAR(150),
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario NUMERIC(10, 2) NOT NULL,
    acompanamientos TEXT,
    subtotal NUMERIC(10, 2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED
);

-- =============================================================================
-- 8. EXTENSIÓN CONTABLE & FISCAL (MICROSERVICIO MONOLOCAL PANAMÁ)
-- =============================================================================

-- 8.1 PERÍODOS CONTABLES (MENSUAL / ANUAL)
CREATE TABLE periodos_contables (
    id_periodo SERIAL PRIMARY KEY,
    anio INT NOT NULL CHECK (anio BETWEEN 2020 AND 2099),
    mes INT NOT NULL CHECK (mes BETWEEN 1 AND 12),
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    estado VARCHAR(20) DEFAULT 'ABIERTO' CHECK (estado IN ('ABIERTO', 'CERRADO', 'BLOQUEADO')),
    fecha_cierre TIMESTAMP NULL,
    usuario_cierre INT REFERENCES usuarios(id_usuario),
    CONSTRAINT uk_periodo_mes_anio UNIQUE (anio, mes)
);

-- 8.2 CATÁLOGO DE CUENTAS (PLAN CONTABLE PARA RESTAURANTE - VENTAS Y OPERACIÓN)
CREATE TABLE catalogo_cuentas (
    codigo_cuenta VARCHAR(25) PRIMARY KEY,
    nombre_cuenta VARCHAR(120) NOT NULL,
    tipo VARCHAR(20) NOT NULL CHECK (tipo IN ('ACTIVO', 'PASIVO', 'PATRIMONIO', 'INGRESOS', 'GASTOS')),
    naturaleza VARCHAR(10) NOT NULL CHECK (naturaleza IN ('DEUDORA', 'ACREEDORA')),
    nivel INT NOT NULL CHECK (nivel BETWEEN 1 AND 4),
    id_cuenta_padre VARCHAR(25) REFERENCES catalogo_cuentas(codigo_cuenta) ON DELETE RESTRICT,
    permite_movimiento BOOLEAN DEFAULT TRUE,
    activo BOOLEAN DEFAULT TRUE
);

-- 8.3 TABLA DE EMPLEADOS Y COLABORADORES DEL RESTAURANTE
CREATE TABLE empleados (
    id_empleado SERIAL PRIMARY KEY,
    nombre_completo VARCHAR(100) NOT NULL,
    cedula VARCHAR(30) NOT NULL UNIQUE,
    cargo VARCHAR(50) NOT NULL CHECK (cargo IN ('Cocinero', 'Mesero', 'Aseador', 'Cajero', 'Administrador')),
    salario_mensual NUMERIC(10, 2) NOT NULL CHECK (salario_mensual > 0),
    fecha_ingreso DATE DEFAULT CURRENT_DATE,
    activo BOOLEAN DEFAULT TRUE
);

-- 8.4 ASIENTOS CONTABLES (CABECERA - LIBRO DIARIO)
CREATE TABLE asientos_contables (
    id_asiento SERIAL PRIMARY KEY,
    numero_asiento VARCHAR(35) NOT NULL UNIQUE,
    id_periodo INT NOT NULL REFERENCES periodos_contables(id_periodo) ON DELETE RESTRICT,
    fecha_asiento DATE NOT NULL DEFAULT CURRENT_DATE,
    concepto VARCHAR(255) NOT NULL,
    origen_modulo VARCHAR(30) NOT NULL CHECK (origen_modulo IN ('VENTAS', 'DEVOLUCIONES', 'CAJA', 'GASTOS', 'NOMINA', 'AJUSTE', 'CIERRE')),
    origen_id INT NULL,
    total_debe NUMERIC(14, 4) NOT NULL DEFAULT 0.0000,
    total_haber NUMERIC(14, 4) NOT NULL DEFAULT 0.0000,
    estado VARCHAR(20) DEFAULT 'ASENTADO' CHECK (estado IN ('BORRADOR', 'ASENTADO', 'ANULADO')),
    creado_por INT REFERENCES usuarios(id_usuario),
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    anulado_por INT REFERENCES usuarios(id_usuario),
    fecha_anulacion TIMESTAMP NULL,
    motivo_anulacion VARCHAR(255) NULL,
    -- IDEMPOTENCIA CONTABLE: Cero asientos duplicados del mismo pedido o evento
    CONSTRAINT uk_asiento_origen UNIQUE (origen_modulo, origen_id)
);

-- 8.5 DETALLE DE ASIENTO (PARTIDA DOBLE: DEBE / HABER)
CREATE TABLE asiento_detalles (
    id_detalle SERIAL PRIMARY KEY,
    id_asiento INT NOT NULL REFERENCES asientos_contables(id_asiento) ON DELETE CASCADE,
    codigo_cuenta VARCHAR(25) NOT NULL REFERENCES catalogo_cuentas(codigo_cuenta) ON DELETE RESTRICT,
    descripcion_linea VARCHAR(150) NOT NULL,
    debe NUMERIC(14, 4) NOT NULL DEFAULT 0.0000 CHECK (debe >= 0),
    haber NUMERIC(14, 4) NOT NULL DEFAULT 0.0000 CHECK (haber >= 0),
    CONSTRAINT chk_detalle_monto_positivo CHECK (debe > 0 OR haber > 0)
);

-- 8.6 MOVIMIENTOS Y ARQUEOS DE CAJA
CREATE TABLE movimientos_caja (
    id_movimiento SERIAL PRIMARY KEY,
    tipo_movimiento VARCHAR(30) NOT NULL CHECK (tipo_movimiento IN ('APERTURA', 'INGRESO_VENTA', 'GASTO_MENOR', 'RETIRO', 'ARQUEO_SOBRANTE', 'ARQUEO_FALTANTE')),
    monto NUMERIC(12, 2) NOT NULL CHECK (monto > 0),
    referencia VARCHAR(150),
    id_usuario INT REFERENCES usuarios(id_usuario),
    fecha_hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 8.7 AUDITORÍA CONTABLE Y FISCAL (INMUTABLE)
CREATE TABLE auditoria_contable (
    id_auditoria BIGSERIAL PRIMARY KEY,
    tabla_afectada VARCHAR(50) NOT NULL,
    id_registro INT NOT NULL,
    accion VARCHAR(20) NOT NULL CHECK (accion IN ('INSERT', 'UPDATE', 'DELETE', 'ANULACION', 'CIERRE_MENSUAL')),
    usuario_id INT REFERENCES usuarios(id_usuario),
    nombre_usuario VARCHAR(50),
    fecha_evento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    datos_anteriores JSONB NULL,
    datos_nuevos JSONB NULL
);

-- ÍNDICES DE RENDIMIENTO Y CONSULTA
CREATE INDEX idx_pedidos_estado ON pedidos(estado);
CREATE INDEX idx_pedidos_estado_cocina ON pedidos(estado_cocina);
CREATE INDEX idx_pedidos_fecha_hora ON pedidos(fecha_hora);
CREATE INDEX idx_pedidos_tipo_servicio ON pedidos(tipo_servicio);
CREATE INDEX idx_pedidos_cliente ON pedidos(nombre_cliente);
CREATE INDEX idx_detalle_pedidos_id_pedido ON detalle_pedidos(id_pedido);
CREATE INDEX idx_detalle_pedidos_id_plato ON detalle_pedidos(id_plato);
CREATE INDEX idx_clientes_ruc ON clientes_fiscales(ruc_cedula);
CREATE INDEX idx_empleados_cedula ON empleados(cedula);
CREATE INDEX idx_asientos_fecha ON asientos_contables(fecha_asiento);
CREATE INDEX idx_asientos_periodo ON asientos_contables(id_periodo);
CREATE INDEX idx_detalles_asiento ON asiento_detalles(id_asiento);
CREATE INDEX idx_detalles_cuenta ON asiento_detalles(codigo_cuenta);

-- =============================================================================
-- INSERCIÓN DE DATOS INICIALES (SEMILLA RESTAURANTE Y CONTABILIDAD PANAMEÑA)
-- =============================================================================

-- 1. USUARIOS DEL SISTEMA
INSERT INTO usuarios (nombre_usuario, password_hash, nombre_completo, rol) VALUES
('admin', '$2a$11$xEbBe/VLhwp0lxBsHt8oNO.fqQocPfAYL34Xlxv3TajBycUei0YD.', 'Administrador General', 'Administrador'),
('cajero', '$2a$11$kxFXz7knnzvLj8IKxRd3wujIJgfnYzyepRr48agnbjr.FVMx0V6QW', 'Cajero de Turno', 'Cajero'),
('cocina', '$2a$11$LHt4rSFwBCFqPUtLxWBq0OwWTevpuZRaIz5kQbfQSo0CRqaeeg9nS', 'Jefe de Cocina KDS', 'Cocina'),
('cliente', '$2a$11$Wu1y54G1CYwdmwpITH.3xOsQ71BRbiF0Zewkb5.8Hu8P/BrJK.BVm', 'Carlos Mendoza', 'Cliente');

-- 2. CATEGORÍAS AUTÉNTICAS DEL MENÚ
INSERT INTO categorias (nombre_categoria, descripcion) VALUES
('Desayunos', 'Frituras típicas panameñas, hojaldres, carimañolas y tortillas'),
('Almuerzos', 'Platos tradicionales panameños, sancocho, pescado frito y ropa vieja'),
('Cenas', 'Bistec picado, corvina a la tipileña y especialidades del istmo');

-- 3. CATÁLOGO DE PLATOS
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

-- 4. CLIENTES FISCALES DEDICADOS (DGI PANAMÁ)
INSERT INTO clientes_fiscales (ruc_cedula, dv, tipo_persona, razon_social, direccion_fiscal, telefono, correo) VALUES
('8-800-1234', '55', 'NATURAL', 'Carlos Mendoza', 'Panamá, Bella Vista', '+507 6200-1122', 'carlos.mendoza@email.com'),
('8-750-5678', '12', 'NATURAL', 'Ana Lucía Torres', 'Penonomé, Coclé', '+507 6555-8899', 'ana.torres@email.com'),
('155698421-2-2024', '89', 'JURIDICA', 'Corporación Gastronómica S.A.', 'Calle 50, Plaza Morazán', '+507 264-5500', 'facturacion@corpgastro.pa'),
('8-912-3401', '33', 'NATURAL', 'David Moreno Castillo', 'San Francisco, Calle 74', '+507 6890-1122', 'david.moreno@gmail.com'),
('155700124-1-2023', '45', 'JURIDICA', 'Inversiones del Istmo S.A.', 'Costa del Este, Torre Financial', '+507 300-8800', 'pagos@inversionesistmo.pa'),
('8-605-4432', '78', 'NATURAL', 'Luis González', 'El Cangrejo, Vía Argentina', '+507 6344-9988', 'luis.gonzalez@outlook.com');

-- 5. PLANTILLA DE EMPLEADOS (3 COCINEROS, 2 MESEROS, 2 ASEADORES)
INSERT INTO empleados (nombre_completo, cedula, cargo, salario_mensual, fecha_ingreso) VALUES
-- 3 Cocineros
('Roberto Carlos Castillo', '8-712-1456', 'Cocinero', 750.00, '2025-03-15'),
('Marta Elena Ríos', '4-250-987', 'Cocinero', 700.00, '2025-06-01'),
('Juan José Batista', '6-708-3321', 'Cocinero', 680.00, '2025-08-10'),
-- 2 Meseros
('Luis Alberto Pimentel', '8-820-4455', 'Mesero', 600.00, '2025-04-12'),
('Gladys Isabel Morales', '8-901-2211', 'Mesero', 600.00, '2025-05-20'),
-- 2 Aseadores
('Pedro Antonio Vergara', '7-115-3420', 'Aseador', 550.00, '2025-02-01'),
('Carmen Rosa Quintero', '9-740-1199', 'Aseador', 550.00, '2025-07-15');

-- 6. PERÍODOS CONTABLES SEMILLA (AÑO 2026)
INSERT INTO periodos_contables (anio, mes, fecha_inicio, fecha_fin, estado) VALUES
(2026, 10, '2026-10-01', '2026-10-31', 'ABIERTO'),
(2026, 11, '2026-11-01', '2026-11-30', 'ABIERTO'),
(2026, 12, '2026-12-01', '2026-12-31', 'ABIERTO');

-- 7. CATÁLOGO DE CUENTAS (PLAN CONTABLE GASTRONÓMICO Y PLANILLA PANAMÁ)
INSERT INTO catalogo_cuentas (codigo_cuenta, nombre_cuenta, tipo, naturaleza, nivel, id_cuenta_padre, permite_movimiento) VALUES
-- 1. Activos
('1', 'ACTIVO', 'ACTIVO', 'DEUDORA', 1, NULL, FALSE),
('1.1', 'Activo Corriente', 'ACTIVO', 'DEUDORA', 2, '1', FALSE),
('1.1.01', 'Efectivo y Equivalentes de Efectivo', 'ACTIVO', 'DEUDORA', 3, '1.1', FALSE),
('1.1.01.01', 'Caja General (Caja de Turno)', 'ACTIVO', 'DEUDORA', 4, '1.1.01', TRUE),
('1.1.01.02', 'Caja Chica (Gastos Menores)', 'ACTIVO', 'DEUDORA', 4, '1.1.01', TRUE),
('1.1.01.03', 'Bancos Locales (Panamá - Banco General)', 'ACTIVO', 'DEUDORA', 4, '1.1.01', TRUE),
('1.1.01.04', 'Fondos en Tránsito POS (Tarjetas Débito/Crédito)', 'ACTIVO', 'DEUDORA', 4, '1.1.01', TRUE),
('1.1.01.05', 'Fondos en Tránsito (Yappy Comercial)', 'ACTIVO', 'DEUDORA', 4, '1.1.01', TRUE),
('1.1.02', 'Cuentas por Cobrar', 'ACTIVO', 'DEUDORA', 3, '1.1', FALSE),
('1.1.02.01', 'Clientes por Cobrar', 'ACTIVO', 'DEUDORA', 4, '1.1.02', TRUE),
('1.1.03', 'Impuestos por Recuperar', 'ACTIVO', 'DEUDORA', 3, '1.1', FALSE),
('1.1.03.01', 'Retención ITBMS Tarjetas POS (DGI)', 'ACTIVO', 'DEUDORA', 4, '1.1.03', TRUE),

-- 2. Pasivos
('2', 'PASIVO', 'PASIVO', 'ACREEDORA', 1, NULL, FALSE),
('2.1', 'Pasivo Corriente', 'PASIVO', 'ACREEDORA', 2, '2', FALSE),
('2.1.01', 'Impuestos Fiscales por Pagar', 'PASIVO', 'ACREEDORA', 3, '2.1', FALSE),
('2.1.01.01', 'ITBMS Débito Fiscal (7% Ventas)', 'PASIVO', 'ACREEDORA', 4, '2.1.01', TRUE),
('2.1.01.02', 'ITBMS Neto por Liquidar DGI', 'PASIVO', 'ACREEDORA', 4, '2.1.01', TRUE),
('2.1.02', 'Otras Cuentas por Pagar', 'PASIVO', 'ACREEDORA', 3, '2.1', FALSE),
('2.1.02.01', 'Propinas por Distribuir', 'PASIVO', 'ACREEDORA', 4, '2.1.02', TRUE),
('2.1.02.02', 'Acreedores Varios', 'PASIVO', 'ACREEDORA', 4, '2.1.02', TRUE),
('2.1.03', 'Obligaciones Laborales y Planilla por Pagar', 'PASIVO', 'ACREEDORA', 3, '2.1', FALSE),
('2.1.03.01', 'Sueldos y Salarios por Pagar', 'PASIVO', 'ACREEDORA', 4, '2.1.03', TRUE),
('2.1.03.02', 'Retenciones CSS (9.75%) y SE (1.25%) por Pagar', 'PASIVO', 'ACREEDORA', 4, '2.1.03', TRUE),
('2.1.03.03', 'Aportes Patronales CSS/SE por Pagar', 'PASIVO', 'ACREEDORA', 4, '2.1.03', TRUE),

-- 3. Patrimonio
('3', 'PATRIMONIO', 'PATRIMONIO', 'ACREEDORA', 1, NULL, FALSE),
('3.1', 'Capital y Reservas', 'PATRIMONIO', 'ACREEDORA', 2, '3', FALSE),
('3.1.01.01', 'Capital del Negocio', 'PATRIMONIO', 'ACREEDORA', 4, '3.1', TRUE),
('3.1.02.01', 'Utilidades Retenidas', 'PATRIMONIO', 'ACREEDORA', 4, '3.1', TRUE),
('3.1.03.01', 'Utilidad del Ejercicio Actual', 'PATRIMONIO', 'ACREEDORA', 4, '3.1', TRUE),

-- 4. Ingresos
('4', 'INGRESOS', 'INGRESOS', 'ACREEDORA', 1, NULL, FALSE),
('4.1', 'Ingresos Operacionales', 'INGRESOS', 'ACREEDORA', 2, '4', FALSE),
('4.1.01.01', 'Venta de Alimentos (Gravadas 7%)', 'INGRESOS', 'ACREEDORA', 4, '4.1', TRUE),
('4.1.01.02', 'Venta de Bebidas', 'INGRESOS', 'ACREEDORA', 4, '4.1', TRUE),
('4.1.01.03', 'Ingresos por Servicio de Delivery', 'INGRESOS', 'ACREEDORA', 4, '4.1', TRUE),
('4.1.02.01', '(-) Devoluciones en Ventas', 'INGRESOS', 'DEUDORA', 4, '4.1', TRUE),

-- 6. Gastos Operativos y de Personal
('6', 'GASTOS OPERATIVOS', 'GASTOS', 'DEUDORA', 1, NULL, FALSE),
('6.1', 'Gastos de Operación y Local', 'GASTOS', 'DEUDORA', 2, '6', FALSE),
('6.1.01.01', 'Servicios Básicos (Luz, Agua, Gas Cocina)', 'GASTOS', 'DEUDORA', 4, '6.1', TRUE),
('6.1.01.02', 'Alquiler del Local Comercial', 'GASTOS', 'DEUDORA', 4, '6.1', TRUE),
('6.1.01.03', 'Mantenimiento y Artículos de Limpieza', 'GASTOS', 'DEUDORA', 4, '6.1', TRUE),
('6.1.02.01', 'Comisiones POS y Plataformas de Cobro', 'GASTOS', 'DEUDORA', 4, '6.1', TRUE),
('6.1.02.02', 'Diferencias de Caja (Faltante Arqueo)', 'GASTOS', 'DEUDORA', 4, '6.1', TRUE),
('6.1.03', 'Gastos de Personal y Planilla', 'GASTOS', 'DEUDORA', 3, '6.1', FALSE),
('6.1.03.01', 'Sueldos y Salarios - Cocina (Cocineros)', 'GASTOS', 'DEUDORA', 4, '6.1.03', TRUE),
('6.1.03.02', 'Sueldos y Salarios - Salón (Meseros)', 'GASTOS', 'DEUDORA', 4, '6.1.03', TRUE),
('6.1.03.03', 'Sueldos y Salarios - Aseo y Mantenimiento', 'GASTOS', 'DEUDORA', 4, '6.1.03', TRUE),
('6.1.03.04', 'Cargas Sociales Patronales (CSS / SE / Riesgos)', 'GASTOS', 'DEUDORA', 4, '6.1.03', TRUE);

-- 8. PEDIDOS OPERATIVOS DE PRUEBA (CABECERAS)
INSERT INTO pedidos (id_pedido, id_cliente_fiscal, nombre_cliente, mesa_o_servicio, tipo_servicio, estado, total, monto_recibido, cambio, metodo_pago, facturado, numero_factura, ruc_cedula, razon_social, direccion_fiscal, telefono_cliente, correo_cliente) VALUES
(1, 1, 'Carlos Mendoza', 'Mesa 04', 'En Mesa', 'Pagado', 10.50, 20.00, 9.50, 'Efectivo', TRUE, 'FAC-2026-1001', '8-800-1234', 'Carlos Mendoza', 'Panamá, Bella Vista', '+507 6200-1122', 'carlos.mendoza@email.com'),
(2, NULL, 'María Fernández', 'Mesa 02', 'En Mesa', 'Pendiente', 7.50, 0.00, 0.00, 'Pendiente', FALSE, NULL, NULL, NULL, NULL, NULL, NULL),
(3, NULL, 'Roberto Gómez', 'Mesa 09', 'En Mesa', 'Pendiente', 8.50, 0.00, 0.00, 'Pendiente', FALSE, NULL, NULL, NULL, NULL, NULL, NULL),
(4, 2, 'Ana Lucía Torres', 'Llevar', 'Para Llevar', 'Pagado', 7.00, 10.00, 3.00, 'Tarjeta POS', TRUE, 'FAC-2026-1002', '8-750-5678', 'Ana Lucía Torres', 'Penonomé, Coclé', '+507 6555-8899', 'ana.torres@email.com'),
(5, 4, 'David Moreno Castillo', 'Mesa 01', 'En Mesa', 'Pagado', 18.50, 18.50, 0.00, 'Yappy Comercial', TRUE, 'FAC-2026-1003', '8-912-3401', 'David Moreno Castillo', 'San Francisco, Calle 74', '+507 6890-1122', 'david.moreno@gmail.com'),
(6, 5, 'Inversiones del Istmo S.A.', 'Mesa 05', 'En Mesa', 'Pagado', 42.80, 42.80, 0.00, 'Tarjeta POS', TRUE, 'FAC-2026-1004', '155700124-1-2023', 'Inversiones del Istmo S.A.', 'Costa del Este, Torre Financial', '+507 300-8800', 'pagos@inversionesistmo.pa'),
(7, 6, 'Luis González', 'Mesa 03', 'En Mesa', 'Pagado', 9.00, 10.00, 1.00, 'Efectivo', TRUE, 'FAC-2026-1005', '8-605-4432', 'Luis González', 'El Cangrejo, Vía Argentina', '+507 6344-9988', 'luis.gonzalez@outlook.com'),
(8, 1, 'Carlos Mendoza', 'Mesa 04', 'En Mesa', 'Cancelado', 8.50, 8.50, 0.00, 'Efectivo', TRUE, 'FAC-2026-1006', '8-800-1234', 'Carlos Mendoza', 'Panamá, Bella Vista', '+507 6200-1122', 'carlos.mendoza@email.com');

SELECT setval('pedidos_id_pedido_seq', 8, true);

-- DETALLE DE PEDIDOS
INSERT INTO detalle_pedidos (id_pedido, id_plato, cantidad, precio_unitario, acompanamientos) VALUES
(1, 5, 1, 10.50, 'Patacones Extra, Chicha de Nance'),
(2, 6, 1, 7.50, 'Chicha de Limón con Raspadura'),
(3, 7, 1, 8.50, 'Plátano tentación'),
(4, 1, 2, 3.50, 'Empacado térmico'),
(5, 6, 1, 7.50, 'Arroz blanco'),
(5, 10, 1, 11.00, 'Patacones crujientes'),
(6, 11, 2, 8.00, 'Tajadas maduras'),
(6, 7, 2, 8.50, 'Arroz con guandú'),
(6, 12, 1, 6.00, 'Yuca al mojo'),
(6, 3, 1, 3.00, 'Chicharrón extra'),
(6, 2, 1, 0.80, 'Carimañola individual'),
(7, 1, 2, 3.50, 'Queso blanco extra'),
(7, 2, 1, 2.00, 'Sin picante'),
(8, 7, 1, 8.50, 'Cancelado por retiro del cliente');

-- =============================================================================
-- 9. TRANSACCIONES OPERATIVAS BASE Y PLANILLA (DATOS SEMILLA RESUMIDOS)
-- =============================================================================

-- -----------------------------------------------------------------------------
-- TRANSACCIÓN 1: Apertura de Operaciones y Fondo de Caja General ($500.00)
-- -----------------------------------------------------------------------------
INSERT INTO movimientos_caja (id_movimiento, tipo_movimiento, monto, referencia, id_usuario, fecha_hora)
VALUES (1, 'APERTURA', 500.00, 'Fondo base de apertura de Caja General en efectivo', 1, '2026-10-01 07:00:00');

INSERT INTO asientos_contables (id_asiento, numero_asiento, id_periodo, fecha_asiento, concepto, origen_modulo, origen_id, total_debe, total_haber, creado_por)
VALUES (1, 'AS-2026-10-0001', 1, '2026-10-01', 'Apertura de operaciones - Fondo inicial en efectivo de Caja General', 'CAJA', 9991, 500.0000, 500.0000, 1);

INSERT INTO asiento_detalles (id_asiento, codigo_cuenta, descripcion_linea, debe, haber) VALUES
(1, '1.1.01.01', 'Fondo inicial de apertura en efectivo Caja General', 500.0000, 0.0000),
(1, '3.1.01.01', 'Capital social - Aporte inicial de socios', 0.0000, 500.0000);

-- -----------------------------------------------------------------------------
-- TRANSACCIÓN 2: Gasto Menor de Caja Chica - Compra de Tanque de Gas ($48.00)
-- -----------------------------------------------------------------------------
INSERT INTO movimientos_caja (id_movimiento, tipo_movimiento, monto, referencia, id_usuario, fecha_hora)
VALUES (2, 'GASTO_MENOR', 48.00, 'Recarga urgente de tanque de gas industrial para estufas', 2, '2026-10-03 14:15:00');

INSERT INTO asientos_contables (id_asiento, numero_asiento, id_periodo, fecha_asiento, concepto, origen_modulo, origen_id, total_debe, total_haber, creado_por)
VALUES (2, 'AS-2026-10-0002', 1, '2026-10-03', 'Gasto menor Caja Chica - Gas licuado para cocina', 'GASTOS', 9992, 48.0000, 48.0000, 2);

INSERT INTO asiento_detalles (id_asiento, codigo_cuenta, descripcion_linea, debe, haber) VALUES
(2, '6.1.01.01', 'Gasto de servicio de gas licuado para cocina', 48.0000, 0.0000),
(2, '1.1.01.02', 'Desembolso en efectivo desde Caja Chica', 0.0000, 48.0000);

-- -----------------------------------------------------------------------------
-- TRANSACCIÓN 3: Gasto Menor de Caja Chica - Artículos de Limpieza y Aseo ($25.50)
-- -----------------------------------------------------------------------------
INSERT INTO movimientos_caja (id_movimiento, tipo_movimiento, monto, referencia, id_usuario, fecha_hora)
VALUES (3, 'GASTO_MENOR', 25.50, 'Insumos de limpieza diaria para aseadores del local', 2, '2026-10-04 10:30:00');

INSERT INTO asientos_contables (id_asiento, numero_asiento, id_periodo, fecha_asiento, concepto, origen_modulo, origen_id, total_debe, total_haber, creado_por)
VALUES (3, 'AS-2026-10-0003', 1, '2026-10-04', 'Gasto menor - Insumos de limpieza y mantenimiento de salón', 'GASTOS', 9993, 25.5000, 25.5000, 2);

INSERT INTO asiento_detalles (id_asiento, codigo_cuenta, descripcion_linea, debe, haber) VALUES
(3, '6.1.01.03', 'Gasto en artículos de desinfección y aseo del local', 25.5000, 0.0000),
(3, '1.1.01.02', 'Desembolso en efectivo desde Caja Chica', 0.0000, 25.5000);

-- -----------------------------------------------------------------------------
-- TRANSACCIÓN 4: Planilla Mensual 7 Colaboradores ($5,094.50)
-- * 3 Cocineros: Roberto Castillo ($750) + Marta Ríos ($700) + Juan Batista ($680) = $2,130.00
-- * 2 Meseros: Luis Pimentel ($600) + Gladys Morales ($600) = $1,200.00
-- * 2 Aseadores: Pedro Vergara ($550) + Carmen Quintero ($550) = $1,100.00
-- Deducciones Obreras (11% CSS/SE): $487.30 | Salario Neto ACH: $3,942.70
-- Cargas Patronales (15% CSS/SE/Riesgos): $664.50
-- -----------------------------------------------------------------------------
INSERT INTO asientos_contables (id_asiento, numero_asiento, id_periodo, fecha_asiento, concepto, origen_modulo, origen_id, total_debe, total_haber, creado_por)
VALUES (4, 'AS-2026-10-0004', 1, '2026-10-15', 'Pago de nómina 7 colaboradores (3 cocineros, 2 meseros, 2 aseadores)', 'NOMINA', 9994, 5094.5000, 5094.5000, 1);

INSERT INTO asiento_detalles (id_asiento, codigo_cuenta, descripcion_linea, debe, haber) VALUES
-- Débitos: Gastos de Personal
(4, '6.1.03.01', 'Sueldos y Salarios - 3 Cocineros (Castillo, Ríos, Batista)', 2130.0000, 0.0000),
(4, '6.1.03.02', 'Sueldos y Salarios - 2 Meseros (Pimentel, Morales)', 1200.0000, 0.0000),
(4, '6.1.03.03', 'Sueldos y Salarios - 2 Aseadores (Vergara, Quintero)', 1100.0000, 0.0000),
(4, '6.1.03.04', 'Cargas Sociales Patronales (CSS 12.25% + SE 1.50% + Riesgos)', 664.5000, 0.0000),
-- Créditos: Desembolso Bancario y Pasivos de Retención
(4, '1.1.01.03', 'Transferencias bancarias ACH sueldo neto a 7 colaboradores', 0.0000, 3942.7000),
(4, '2.1.03.02', 'Retenciones obreras CSS (9.75%) y SE (1.25%) por liquidar', 0.0000, 487.3000),
(4, '2.1.03.03', 'Aportes patronales CSS/SE por liquidar a la Caja de Seguro Social', 0.0000, 664.5000);

-- Actualizar secuencias PostgreSQL
SELECT setval('asientos_contables_id_asiento_seq', 4, true);
SELECT setval('asiento_detalles_id_detalle_seq', (SELECT COALESCE(MAX(id_detalle), 1) FROM asiento_detalles), true);
SELECT setval('movimientos_caja_id_movimiento_seq', 3, true);
SELECT setval('empleados_id_empleado_seq', 7, true);
