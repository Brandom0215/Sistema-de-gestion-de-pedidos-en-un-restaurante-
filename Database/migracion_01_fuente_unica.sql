-- =============================================================================
-- RESTAURANTE EL BUEN SAZÓN — MIGRACIÓN 01: POSTGRESQL ÚNICA FUENTE DE VERDAD
-- =============================================================================
-- Este script es IDEMPOTENTE y NO borra datos existentes.
-- Habilita la secuencia de facturas y actualiza las contraseñas con hash BCrypt.
-- =============================================================================

-- 1. SECUENCIA DE FACTURACIÓN ELECTRÓNICA MULTIUSUARIO
CREATE SEQUENCE IF NOT EXISTS seq_factura START WITH 1001;

-- 2. PARCHES COMPATIBILIDAD DE ESTRUCTURA EN DETALLE DE PEDIDOS
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'detalle_pedidos' AND column_name = 'id_plato' AND is_nullable = 'NO') THEN
        ALTER TABLE detalle_pedidos ALTER COLUMN id_plato DROP NOT NULL;
    END IF;
END $$;

ALTER TABLE detalle_pedidos ADD COLUMN IF NOT EXISTS nombre_plato VARCHAR(150);

-- 3. ACTUALIZACIÓN DE CONTRASEÑAS CON HASH BCRYPT PARA USUARIOS SEMILLA
UPDATE usuarios SET password_hash = '$2a$11$xEbBe/VLhwp0lxBsHt8oNO.fqQocPfAYL34Xlxv3TajBycUei0YD.' WHERE nombre_usuario = 'admin';
UPDATE usuarios SET password_hash = '$2a$11$kxFXz7knnzvLj8IKxRd3wujIJgfnYzyepRr48agnbjr.FVMx0V6QW' WHERE nombre_usuario = 'cajero';
UPDATE usuarios SET password_hash = '$2a$11$LHt4rSFwBCFqPUtLxWBq0OwWTevpuZRaIz5kQbfQSo0CRqaeeg9nS' WHERE nombre_usuario = 'cocina';
UPDATE usuarios SET password_hash = '$2a$11$Wu1y54G1CYwdmwpITH.3xOsQ71BRbiF0Zewkb5.8Hu8P/BrJK.BVm' WHERE nombre_usuario = 'cliente';
