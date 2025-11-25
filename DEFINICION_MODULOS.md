# Plan de Migración - Backend TypeScript a .NET

## 📋 Resumen General

**Objetivo:** Migrar 6 módulos principales del backend TypeScript a .NET con arquitectura hexagonal y CQRS

**Módulos a Migrar:**
1. Usuarios y Roles
2. Inventario
3. Ventas
4. Producción
5. Finanzas
6. Configuración Final

**Total de Entidades:** 34 entidades
**Sprints Planeados:** 6 sprints
**Patrón:** Clean Architecture + CQRS + Unit of Work

---

## 🎯 Sprint 1: Módulo Usuarios y Roles

**Duración Estimada:** 3-4 días

### Entidades (7)
- ✅ `User` - Usuarios del sistema
- ✅ `Role` - Roles de usuario
- ✅ `Permission` - Permisos del sistema
- ✅ `RolesPermission` - Relación muchos a muchos
- ✅ `Module` - Módulos del sistema
- ✅ `TypePerson` - Tipos de persona

### Commits Planeados

#### Commit 1: Base de Infraestructura
```
feat(users): agregar interfaces de repositorio y configuraciones EF Core

- Crear IUserRepository, IRoleRepository, IPermissionRepository
- Crear IModuleRepository, ITypePersonRepository
- Crear configuraciones EF Core para todas las entidades
- Configurar relaciones muchos a muchos
```

**Archivos:**
- `Domain/Interfaces/IUserRepository.cs`
- `Domain/Interfaces/IRoleRepository.cs`
- `Domain/Interfaces/IPermissionRepository.cs`
- `Domain/Interfaces/IModuleRepository.cs`
- `Domain/Interfaces/ITypePersonRepository.cs`
- `Infrastructure/Persistence/Configurations/UserConfiguration.cs`
- `Infrastructure/Persistence/Configurations/RoleConfiguration.cs`
- `Infrastructure/Persistence/Configurations/PermissionConfiguration.cs`
- `Infrastructure/Persistence/Configurations/ModuleConfiguration.cs`

#### Commit 2: Implementación de Repositorios
```
feat(users): implementar repositorios de usuarios y roles

- Implementar UserRepository con métodos base
- Implementar RoleRepository con permisos
- Implementar PermissionRepository
- Agregar métodos específicos del dominio
```

**Archivos:**
- `Infrastructure/Persistence/Repositories/UserRepository.cs`
- `Infrastructure/Persistence/Repositories/RoleRepository.cs`
- `Infrastructure/Persistence/Repositories/PermissionRepository.cs`
- `Infrastructure/Persistence/Repositories/ModuleRepository.cs`

#### Commit 3: Commands CQRS - Usuarios
```
feat(users): agregar commands CQRS para usuarios

- CreateUserCommand con validación
- UpdateUserCommand
- DeleteUserCommand
- AssignRoleCommand
- ChangePasswordCommand
```

**Archivos:**
- `Application/UseCases/Commands/Users/CreateUserCommand.cs`
- `Application/UseCases/Commands/Users/UpdateUserCommand.cs`
- `Application/UseCases/Commands/Users/DeleteUserCommand.cs`
- `Application/UseCases/Commands/Users/AssignRoleCommand.cs`
- `Application/UseCases/Commands/Users/ChangePasswordCommand.cs`

#### Commit 4: Queries CQRS - Usuarios
```
feat(users): agregar queries CQRS para usuarios

- GetUserByIdQuery
- GetAllUsersQuery
- GetUserByEmailQuery
- GetUsersByRoleQuery
```

**Archivos:**
- `Application/UseCases/Queries/Users/GetUserByIdQuery.cs`
- `Application/UseCases/Queries/Users/GetAllUsersQuery.cs`
- `Application/UseCases/Queries/Users/GetUserByEmailQuery.cs`
- `Application/UseCases/Queries/Users/GetUsersByRoleQuery.cs`

#### Commit 5: Handlers y Validadores
```
feat(users): implementar handlers MediatR y validadores FluentValidation

- Handlers para todos los commands y queries
- Validadores para CreateUser, UpdateUser
- Lógica de hash de contraseñas con BCrypt
```

**Archivos:**
- `Application/UseCases/Handlers/Users/CreateUserCommandHandler.cs`
- `Application/UseCases/Handlers/Users/GetUserByIdQueryHandler.cs`
- `Application/Validators/CreateUserCommandValidator.cs`
- `Application/Validators/UpdateUserCommandValidator.cs`

#### Commit 6: DTOs, Mappings y Controllers
```
feat(users): agregar DTOs, AutoMapper profiles y controllers

- UserDto, CreateUserDto, UpdateUserDto
- RoleDto, PermissionDto
- AutoMapper profiles
- UsersController con endpoints CRUD
- RolesController
- AuthController (Login, Register)
```

**Archivos:**
- `Application/DTOs/Users/UserDto.cs`
- `Application/DTOs/Users/CreateUserDto.cs`
- `Application/DTOs/Roles/RoleDto.cs`
- `Application/Common/Mappings/UserMappingProfile.cs`
- `Proyecto Final/Controllers/UsersController.cs`
- `Proyecto Final/Controllers/RolesController.cs`
- `Proyecto Final/Controllers/AuthController.cs`

#### Commit 7: Unit of Work y Servicios
```
feat(users): implementar Unit of Work y servicios de autenticación

- IUnitOfWork interface
- UnitOfWork implementation
- JwtTokenService
- PasswordHashService
- Configurar DI en Program.cs
```

**Archivos:**
- `Domain/Interfaces/IUnitOfWork.cs`
- `Infrastructure/Persistence/UnitOfWork/UnitOfWork.cs`
- `Infrastructure/Services/JwtTokenService.cs`
- `Infrastructure/Services/PasswordHashService.cs`

---

## 🎯 Sprint 2: Módulo Inventario

**Duración Estimada:** 4-5 días

### Entidades (12)
- ✅ `Product` - Productos
- ✅ `Category` - Categorías de productos
- ✅ `Warehouse` - Almacenes
- ✅ `WarehouseProduct` - Productos en almacén
- ✅ `WarehouseResource` - Recursos en almacén
- ✅ `BuysProduct` - Compras de productos
- ✅ `Supplier` - Proveedores
- ✅ `Resource` - Recursos
- ✅ `ProductPurchased` - Productos comprados
- ✅ `WarehouseMovementProduct` - Movimientos de productos
- ✅ `WarehouseMovementResource` - Movimientos de recursos
- ✅ `WarehouseStore` - Relación almacén-tienda

### Commits Planeados

#### Commit 1: Repositorios - Productos y Categorías
```
feat(inventory): agregar repositorios de productos y categorías

- IProductRepository con búsquedas avanzadas
- ICategoryRepository
- Configuraciones EF Core
- Implementaciones de repositorios
```

#### Commit 2: Repositorios - Almacenes y Proveedores
```
feat(inventory): agregar repositorios de almacenes, proveedores y recursos

- IWarehouseRepository
- ISupplierRepository
- IResourceRepository
- IWarehouseProductRepository
- Configuraciones y relaciones
```

#### Commit 3: Commands - Productos
```
feat(inventory): agregar commands CQRS para productos

- CreateProductCommand
- UpdateProductCommand
- DeleteProductCommand
- UpdateProductPriceCommand
- UpdateProductStockCommand
```

#### Commit 4: Commands - Almacenes y Movimientos
```
feat(inventory): agregar commands para almacenes y movimientos

- CreateWarehouseMovementCommand
- TransferProductBetweenWarehousesCommand
- AdjustStockCommand
- RecordPurchaseCommand
```

#### Commit 5: Queries - Inventario
```
feat(inventory): agregar queries para consultas de inventario

- GetProductByIdQuery
- GetAllProductsQuery
- GetProductsByCategoryQuery
- GetWarehouseStockQuery
- GetLowStockProductsQuery
- GetProductMovementsQuery
```

#### Commit 6: Handlers y Validadores
```
feat(inventory): implementar handlers y validadores de inventario

- Handlers para commands y queries
- Validadores para productos, almacenes
- Lógica de negocio de stock
```

#### Commit 7: DTOs, Mappings y Controllers
```
feat(inventory): agregar DTOs, mappings y controllers de inventario

- ProductDto, WarehouseDto, etc.
- AutoMapper profiles
- ProductsController
- WarehousesController
- SuppliersController
```

---

## 🎯 Sprint 3: Módulo Ventas

**Duración Estimada:** 4-5 días

### Entidades (7)
- ✅ `Sale` - Ventas
- ✅ `SaleDetail` - Detalles de venta
- ✅ `Store` - Tiendas
- ✅ `Customer` - Clientes
- ✅ `PaymentMethod` - Métodos de pago
- ✅ `CashSession` - Sesiones de caja
- ✅ `SalesChannel` - Canales de venta

### Commits Planeados

#### Commit 1: Repositorios - Ventas y Tiendas
```
feat(sales): agregar repositorios de ventas y tiendas

- ISaleRepository
- IStoreRepository
- ISaleDetailRepository
- Configuraciones EF Core
```

#### Commit 2: Repositorios - Clientes y Métodos de Pago
```
feat(sales): agregar repositorios de clientes y métodos de pago

- ICustomerRepository
- IPaymentMethodRepository
- ICashSessionRepository
- ISalesChannelRepository
```

#### Commit 3: Commands - Ventas
```
feat(sales): agregar commands para ventas

- CreateSaleCommand (con detalles)
- UpdateSaleCommand
- CancelSaleCommand
- ProcessRefundCommand
```

#### Commit 4: Commands - Sesiones de Caja
```
feat(sales): agregar commands para sesiones de caja

- OpenCashSessionCommand
- CloseCashSessionCommand
- AddCashMovementCommand
- ReconcileCashCommand
```

#### Commit 5: Queries - Ventas
```
feat(sales): agregar queries para consultas de ventas

- GetSaleByIdQuery
- GetSalesByDateRangeQuery
- GetSalesByStoreQuery
- GetDailySalesReportQuery
- GetCashSessionByIdQuery
```

#### Commit 6: Handlers y Validadores
```
feat(sales): implementar handlers y validadores de ventas

- Handlers para commands y queries
- Validadores para ventas, clientes
- Lógica de descuento de stock
- Cálculo de totales
```

#### Commit 7: DTOs, Mappings y Controllers
```
feat(sales): agregar DTOs, mappings y controllers de ventas

- SaleDto, SaleDetailDto, StoreDto
- AutoMapper profiles
- SalesController
- CustomersController
- CashSessionsController
```

---

## 🎯 Sprint 4: Módulo Producción

**Duración Estimada:** 2-3 días

### Entidades (3)
- ✅ `Production` - Producción
- ✅ `Recipe` - Recetas
- ✅ `PlantProduction` - Producción de plantas

### Commits Planeados

#### Commit 1: Repositorios - Producción
```
feat(production): agregar repositorios de producción

- IProductionRepository
- IRecipeRepository
- IPlantProductionRepository
- Configuraciones EF Core
```

#### Commit 2: Commands - Producción
```
feat(production): agregar commands para producción

- CreateProductionCommand
- UpdateProductionStatusCommand
- CreateRecipeCommand
- UpdateRecipeCommand
- AssignRecipeToProductCommand
```

#### Commit 3: Queries - Producción
```
feat(production): agregar queries para producción

- GetProductionByIdQuery
- GetProductionsByDateQuery
- GetRecipeByProductQuery
- GetAllRecipesQuery
```

#### Commit 4: Handlers y Validadores
```
feat(production): implementar handlers y validadores

- Handlers para commands y queries
- Validadores para producción y recetas
- Lógica de consumo de recursos
```

#### Commit 5: DTOs, Mappings y Controllers
```
feat(production): agregar DTOs, mappings y controllers

- ProductionDto, RecipeDto
- AutoMapper profiles
- ProductionsController
- RecipesController
```

---

## 🎯 Sprint 5: Módulo Finanzas

**Duración Estimada:** 3-4 días

### Entidades (5)
- ✅ `FinancialReport` - Reportes financieros
- ✅ `GeneralIncome` - Ingresos generales
- ✅ `GeneralExpense` - Gastos generales
- ✅ `MonasteryExpense` - Gastos del monasterio
- ✅ `Overhead` - Gastos overhead

### Commits Planeados

#### Commit 1: Repositorios - Reportes e Ingresos
```
feat(finance): agregar repositorios de reportes e ingresos

- IFinancialReportRepository
- IGeneralIncomeRepository
- Configuraciones EF Core
```

#### Commit 2: Repositorios - Gastos
```
feat(finance): agregar repositorios de gastos

- IGeneralExpenseRepository
- IMonasteryExpenseRepository
- IOverheadRepository
```

#### Commit 3: Commands - Reportes
```
feat(finance): agregar commands para reportes financieros

- GenerateFinancialReportCommand
- CreateIncomeCommand
- CreateExpenseCommand
- RecordOverheadCommand
```

#### Commit 4: Queries - Finanzas
```
feat(finance): agregar queries para consultas financieras

- GetFinancialReportByDateQuery
- GetIncomesByPeriodQuery
- GetExpensesByPeriodQuery
- GetProfitLossStatementQuery
```

#### Commit 5: Handlers y Validadores
```
feat(finance): implementar handlers y validadores

- Handlers para commands y queries
- Validadores para ingresos y gastos
- Lógica de cálculo de reportes
```

#### Commit 6: DTOs, Mappings y Controllers
```
feat(finance): agregar DTOs, mappings y controllers

- FinancialReportDto, IncomeDto, ExpenseDto
- AutoMapper profiles
- FinancialReportsController
- IncomesController
- ExpensesController
```

---

## 🎯 Sprint 6: Configuración Final y Testing

**Duración Estimada:** 2-3 días

### Tareas

#### Commit 1: Configuración Global
```
feat(config): configurar Program.cs y appsettings.json

- Configurar DbContext con connection string
- Registrar todos los servicios en DI
- Configurar MediatR
- Configurar AutoMapper
- Configurar FluentValidation
```

#### Commit 2: Autenticación y Autorización
```
feat(auth): configurar JWT y políticas de autorización

- Configurar JWT Authentication
- Crear políticas de autorización por módulo
- Configurar CORS
- Agregar middleware de autenticación
```

#### Commit 3: Middlewares y Manejo de Errores
```
feat(middleware): agregar middlewares globales

- Middleware de manejo de excepciones
- Middleware de logging
- Middleware de validación
- Response wrapping
```

#### Commit 4: Swagger y Documentación
```
feat(docs): configurar Swagger con autenticación JWT

- Configurar Swagger UI
- Agregar JWT bearer en Swagger
- Documentar endpoints
- Agregar ejemplos de requests
```

#### Commit 5: Behaviors de MediatR
```
feat(behaviors): agregar pipeline behaviors

- ValidationBehavior
- LoggingBehavior
- PerformanceBehavior
- TransactionBehavior
```

#### Commit 6: Testing y Ajustes Finales
```
test(all): agregar pruebas básicas y ajustes finales

- Probar todos los endpoints
- Ajustar validaciones
- Verificar relaciones EF Core
- Documentación final
```

---

## 📊 Resumen de Entidades por Sprint

| Sprint | Módulo | Entidades | Commits |
|--------|--------|-----------|---------|
| 1 | Usuarios y Roles | 6 | 7 |
| 2 | Inventario | 12 | 7 |
| 3 | Ventas | 7 | 7 |
| 4 | Producción | 3 | 5 |
| 5 | Finanzas | 5 | 6 |
| 6 | Configuración | - | 6 |
| **Total** | **6 módulos** | **33 entidades** | **38 commits** |

---

## 🔧 Tecnologías y Patrones

- **Arquitectura:** Clean Architecture / Hexagonal
- **Patrón CQRS:** MediatR
- **Validación:** FluentValidation
- **Mapping:** AutoMapper
- **ORM:** Entity Framework Core
- **Base de Datos:** PostgreSQL
- **Autenticación:** JWT Bearer
- **Documentación:** Swagger/OpenAPI
- **Patrón Repositorio:** Generic Repository + Unit of Work

---

## 📝 Convención de Commits

```
<tipo>(<alcance>): <descripción corta>

<cuerpo opcional>

<pie opcional>
```

**Tipos:**
- `feat`: Nueva funcionalidad
- `fix`: Corrección de bugs
- `refactor`: Refactorización de código
- `test`: Agregar o modificar tests
- `docs`: Documentación
- `chore`: Tareas de mantenimiento

**Alcances:**
- `users`, `roles`, `auth`
- `inventory`, `products`, `warehouse`
- `sales`, `customers`, `cash`
- `production`, `recipes`
- `finance`, `reports`
- `config`, `middleware`

---

## 🚀 Orden de Implementación

1. **Sprint 1** es crítico - Usuarios y autenticación son base para todo
2. **Sprint 2** es fundamental - Inventario es core del negocio
3. **Sprint 3** depende de Inventario - Ventas consume stock
4. **Sprint 4** depende de Inventario - Producción genera productos
5. **Sprint 5** puede ir en paralelo - Finanzas es independiente
6. **Sprint 6** al final - Configuración y pulido

---

## ✅ Checklist por Sprint

Cada sprint debe completar:

- [ ] Interfaces de repositorio en Domain
- [ ] Configuraciones EF Core
- [ ] Implementaciones de repositorios
- [ ] Commands CQRS
- [ ] Queries CQRS
- [ ] Handlers de MediatR
- [ ] Validadores FluentValidation
- [ ] DTOs
- [ ] Mappings AutoMapper
- [ ] Controllers
- [ ] Pruebas en Swagger

---

**Proyecto:** ProjectofinalDAEA
**Fecha de Inicio:** 24 de noviembre de 2025
**Estimación Total:** 18-24 días
