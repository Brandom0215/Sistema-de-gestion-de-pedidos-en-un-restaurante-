# Sistema de Gestión de Pedidos — Restaurante "El Buen Sazón"

Plataforma de software desarrollada en Visual Basic .NET para la digitalización, automatización y administración eficiente del flujo operativo en restaurantes.

---

## 1. Descripción General del Proyecto

El sistema transforma la experiencia gastronómica tradicional mediante la automatización de la toma de comandas, el procesamiento de pagos, la transmisión en tiempo real hacia la cocina (KDS) y la emisión de facturas/comprobantes digitales. Funciona de manera autónoma en memoria sin requerir la instalación de servicios externos de base de datos.

- **Nombre Comercial:** Restaurante "El Buen Sazón".
- **Tecnologías:** Visual Basic .NET (WinForms), .NET 10.0.
- **Arquitectura:** Contenedor principal desacoplado (FrmHome) con carga dinámica de módulos, almacenamiento en memoria aislada (DAO Repository) y sistema de diseño centralizado (ThemeConfig).

---

## 2. Flujo Operativo y Arquitectura de Usuarios

El sistema opera bajo una arquitectura multi-rol con validación estricta de credenciales en memoria (UsuarioDAO).

### 2.1. Pantalla de Autenticación (FrmLogin)
- La aplicación se inicia en FrmLogin.vb.
- El usuario ingresa sus credenciales (usuario/correo y contraseña).
- El sistema autentica las credenciales en memoria e infiere automáticamente su rol de permisos asignado.
- Los clientes nuevos disponen de un enlace directo hacia el formulario de registro (FrmRegistroCliente.vb).

### 2.2. Roles y Permisos en el Sistema (FrmHome)
- **Cliente (Tótem / Autoatención):**
  - Al autenticarse, el menú lateral izquierdo oculta e inhabilitar completamente todos los accesos administrativos (Dashboard, Catálogo, Cocina, Caja, Facturación, Reportes).
  - El cliente accede exclusivamente a su módulo de Toma de Pedidos y Menú Digital (FrmPedidos.vb).
- **Personal de Cocina (KDS):**
  - Accede directamente al Monitor de Cocina en Tiempo Real (FrmCocinaKDS.vb) y a la consulta de órdenes activas.
- **Cajero / Personal de Sala:**
  - Accede a los módulos de Toma de Pedidos, Caja & Cobros y Facturación PDF.
- **Administrador:**
  - Posee acceso ilimitado a todos los módulos, reportes de ventas y métricas ejecutivas.

---

## 3. Módulo de Pedidos y Segunda Interfaz (Requisitos del Sistema)

El módulo de Gestión de Pedidos (FrmPedidos.vb) integra los requisitos de toma de orden con los estándares visuales del proyecto:

1. **Captura de Datos del Cliente:** TextBox para Nombre del Cliente y Número de Mesa.
2. **Plato Principal:** ComboBox con el catálogo interactivo de platos y precios.
3. **Acompañamientos:** CheckBoxes para personalización (Papas Fritas, Ensalada Fresca, Arroz con Choclo, Salsas).
4. **Tipo de Servicio:** OptionButtons / RadioButtons (En Mesa, Para Llevar, Delivery).
5. **Operaciones CRUD:** Botones funcionales de Guardar, Actualizar, Eliminar, Buscar y Mostrar Todos.
6. **Segunda Interfaz de Confirmación (FrmPedidoConfirmado.vb):**
   - Modal emergente que despliega el resumen del pedido confirmado junto con la previsualización del plato principal en un control PictureBox.

---

## 4. Guía de Ejecución y Compilación

### Requisitos Previos
- Visual Studio 2022 o VS Code con extensión de .NET.
- SDK de .NET 10.0 instalado.

### Compilación y Ejecución
1. Abrir la solución `Sistema de gestion de pedidos para un restaurante.slnx`.
2. Restaurar dependencias mediante el comando:
   ```bash
   dotnet build
   ```
3. Ejecutar la aplicación desde Visual Studio (F5) o mediante la línea de comandos:
   ```bash
   dotnet run --project "Sistema de gestion de pedidos para un restaurante/Sistema de gestion de pedidos para un restaurante/Sistema de gestion de pedidos para un restaurante.vbproj"
   ```

### Credenciales de Prueba Rápida
En la pantalla de inicio de sesión (FrmLogin) se disponen de botones de autocompletado rápido para verificar cada rol:
- **Administrador:** `admin` / `1234`
- **Cajero:** `cajero` / `1234`
- **Cocina:** `cocina` / `1234`
- **Cliente:** `cliente` / `1234`
