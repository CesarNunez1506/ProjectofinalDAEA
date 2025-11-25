# 🚀 Plan de Sprints - Migración Backend

## Sprint 1: Módulo Usuarios y Roles (Base del Sistema)
**Prioridad:** CRÍTICA | **Duración:** 3-4 días | **7 commits**

### 📦 Entidades (6)
```
User, Role, Permission, RolesPermission, Module, TypePerson
```

### 📝 Commits
1. ✅ Interfaces repositorio + Configuraciones EF Core
2. ✅ Implementación repositorios
3. ✅ Commands CQRS (Create, Update, Delete, AssignRole, ChangePassword)
4. ✅ Queries CQRS (GetById, GetAll, GetByEmail, GetByRole)
5. ✅ Handlers MediatR + Validadores FluentValidation
6. ✅ DTOs + AutoMapper + Controllers (Users, Roles, Auth)
7. ✅ Unit of Work + Servicios (JWT, PasswordHash)

---

## Sprint 2: Módulo Inventario
**Prioridad:** ALTA | **Duración:** 4-5 días | **7 commits**

### 📦 Entidades (12)
```
Product, Category, Warehouse, WarehouseProduct, WarehouseResource,
BuysProduct, Supplier, Resource, ProductPurchased,
WarehouseMovementProduct, WarehouseMovementResource, WarehouseStore
```

### 📝 Commits
1. ✅ Repositorios Productos + Categorías
2. ✅ Repositorios Almacenes + Proveedores + Recursos
3. ✅ Commands Productos
4. ✅ Commands Almacenes + Movimientos
5. ✅ Queries Inventario
6. ✅ Handlers + Validadores
7. ✅ DTOs + Mappings + Controllers

---

## Sprint 3: Módulo Ventas
**Prioridad:** ALTA | **Duración:** 4-5 días | **7 commits**

### 📦 Entidades (7)
```
Sale, SaleDetail, Store, Customer, PaymentMethod,
CashSession, SalesChannel
```

### 📝 Commits
1. ✅ Repositorios Ventas + Tiendas
2. ✅ Repositorios Clientes + Métodos Pago + Sesiones Caja
3. ✅ Commands Ventas
4. ✅ Commands Sesiones Caja
5. ✅ Queries Ventas + Reportes
6. ✅ Handlers + Validadores
7. ✅ DTOs + Mappings + Controllers

---

## Sprint 4: Módulo Producción
**Prioridad:** MEDIA | **Duración:** 2-3 días | **5 commits**

### 📦 Entidades (3)
```
Production, Recipe, PlantProduction
```

### 📝 Commits
1. ✅ Repositorios Producción
2. ✅ Commands Producción + Recetas
3. ✅ Queries Producción
4. ✅ Handlers + Validadores
5. ✅ DTOs + Mappings + Controllers

---

## Sprint 5: Módulo Finanzas
**Prioridad:** MEDIA | **Duración:** 3-4 días | **6 commits**

### 📦 Entidades (5)
```
FinancialReport, GeneralIncome, GeneralExpense,
MonasteryExpense, Overhead
```

### 📝 Commits
1. ✅ Repositorios Reportes + Ingresos
2. ✅ Repositorios Gastos
3. ✅ Commands Finanzas
4. ✅ Queries Finanzas + Reportes
5. ✅ Handlers + Validadores
6. ✅ DTOs + Mappings + Controllers

---

## Sprint 6: Configuración Final
**Prioridad:** ALTA | **Duración:** 2-3 días | **6 commits**

### 🔧 Tareas
1. ✅ Configurar Program.cs + appsettings.json
2. ✅ JWT Authentication + Políticas Autorización
3. ✅ Middlewares Globales + Manejo Errores
4. ✅ Swagger + Documentación API
5. ✅ MediatR Pipeline Behaviors
6. ✅ Testing + Ajustes Finales

---

## 📊 Resumen General

| Sprint | Módulo | Entidades | Commits | Días |
|--------|--------|-----------|---------|------|
| 1 | Usuarios/Roles | 6 | 7 | 3-4 |
| 2 | Inventario | 12 | 7 | 4-5 |
| 3 | Ventas | 7 | 7 | 4-5 |
| 4 | Producción | 3 | 5 | 2-3 |
| 5 | Finanzas | 5 | 6 | 3-4 |
| 6 | Config Final | - | 6 | 2-3 |
| **TOTAL** | **6** | **33** | **38** | **18-24** |

---

## 🎯 Orden de Implementación

```
Sprint 1 (Usuarios) → OBLIGATORIO PRIMERO
    ↓
Sprint 2 (Inventario) → CORE DEL NEGOCIO
    ↓
Sprint 3 (Ventas) → DEPENDE DE INVENTARIO
    ↓
Sprint 4 (Producción) → DEPENDE DE INVENTARIO
    ↓
Sprint 5 (Finanzas) → INDEPENDIENTE (puede ir en paralelo)
    ↓
Sprint 6 (Config) → AL FINAL
```

---

## ✅ Checklist por Sprint

Cada sprint debe completar:

- [ ] Domain/Interfaces (Repositorios)
- [ ] Infrastructure/Persistence/Configurations (EF Core)
- [ ] Infrastructure/Persistence/Repositories (Implementaciones)
- [ ] Application/UseCases/Commands
- [ ] Application/UseCases/Queries
- [ ] Application/UseCases/Handlers
- [ ] Application/Validators
- [ ] Application/DTOs
- [ ] Application/Common/Mappings
- [ ] Proyecto Final/Controllers
- [ ] Pruebas en Swagger

---

## 🔄 Estado Actual

**Sprint Actual:** Ninguno (Scaffolding completado)
**Listo para comenzar:** Sprint 1 - Módulo Usuarios y Roles

**Siguiente Acción:** Comenzar Sprint 1, Commit 1
