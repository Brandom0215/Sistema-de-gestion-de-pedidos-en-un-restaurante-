# Sistema de Gestión de Pedidos — Restaurante "El Buen Sazón"

Plataforma de software desarrollada en Visual Basic .NET para la digitalización, automatización y administración eficiente del flujo operativo en restaurantes con PostgreSQL como única fuente de verdad.

---

## 1. Descripción General del Proyecto

El sistema transforma la experiencia gastronómica tradicional mediante la automatización de la toma de comandas, el procesamiento de pagos, la transmisión en tiempo real hacia la cocina (KDS) y la emisión de facturas/comprobantes digitales.

- **Nombre Comercial:** Restaurante "El Buen Sazón".
- **Tecnologías:** Visual Basic .NET (WinForms), .NET 10.0, Npgsql, PostgreSQL.
- **Arquitectura:** Contenedor principal desacoplado (FrmHome) con carga dinámica de módulos, almacenamiento desacoplado directamente en PostgreSQL (sin memoria local como respaldo) y sistema de diseño centralizado (ThemeConfig).

---

## 2. Flujo Operativo y Arquitectura de Usuarios

El sistema opera bajo una arquitectura multi-rol con validación de credenciales contra PostgreSQL encriptadas con hashes BCrypt.

### 2.1. Flujo de Inicio y Autenticación
- La aplicación inicia en la pantalla de inicio de sesión (FrmLogin.vb).
- Los campos inician limpios sin credenciales expuestas en pantalla.
- Al autenticarse el personal o cliente, el sistema conmuta sus permisos y paneles según su rol.

### 2.2. Roles y Permisos en el Sistema (FrmHome)
- **1. Módulo Cliente:** Acceso a Carta & Menú Digital y Toma de Pedidos.
- **2. Módulo Cocina:** Acceso al Monitor de Cocina KDS en Tiempo Real con colas FIFO y transiciones de estado.
- **3. Módulo Caja / Facturación:** Procesa pagos en efectivo, tarjetas POS y QR, y genera facturas con secuencia PostgreSQL.
- **4. Módulo Administrador:** Acceso completo a métricas del negocio y gestión del catálogo de platos.

---

## 3. Base de Datos PostgreSQL y Migración

PostgreSQL es la ÚNICA fuente de verdad del sistema.

### 3.1. Migración de Base de Datos
- **Script Principal:** `Database/Script_PostgreSQL_Restaurante.sql` (creación inicial de tablas y estructura).
- **Script de Migración Idempotente:** `Database/migracion_01_fuente_unica.sql` (habilita la secuencia `seq_factura`, parches de estructura en `detalle_pedidos` y actualiza las contraseñas semilla a hashes BCrypt).

---

## 4. Guía de Ejecución y Compilación

### Requisitos Previos
- Visual Studio 2022 o VS Code con extensión de .NET.
- SDK de .NET 10.0 instalado.
- Servidor remoto o local de PostgreSQL.

### Variables de Entorno (.env)
Configurar las credenciales en el archivo `.env` tomando como plantilla `.env.example`:
```env
DB_HOST=10.196.68.15
DB_PORT=5432
DB_NAME=restaurante_db
DB_USER=usuario_restaurante
DB_PASS=tu_contraseña_aqui
```

### Compilación y Ejecución
1. Abrir la solución en la carpeta del proyecto.
2. Compilar con el comando:
   ```bash
   dotnet build
   ```

### Credenciales Unificadas de Prueba
- **Administrador:** `admin` / `admin123`
- **Cajero:** `cajero` / `cajero123`
- **Cocina:** `cocina` / `cocina123`
- **Cliente:** `cliente` / `cliente123`
