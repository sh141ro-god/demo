@echo off
@echo This cmd file creates a Data API Builder configuration based on the chosen database objects.
@echo To run the cmd, create an .env file with the following contents:
@echo dab-connection-string=your connection string
@echo ** Make sure to exclude the .env file from source control **
@echo **
dotnet tool install -g Microsoft.DataApiBuilder --prerelease
dab init -c dab-config.json --database-type mssql --connection-string "@env('dab-connection-string')" --host-mode Development
@echo Adding tables
dab add "Category" --source "[dbo].[Category]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "Manufacturer" --source "[dbo].[Manufacturer]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "Order" --source "[dbo].[Order]" --fields.include "Id,Article,IdStatus,PickupAddress,OrderDate,DeliveryDate" --permissions "anonymous:*" 
dab add "OrderItem" --source "[dbo].[OrderItem]" --fields.include "Id,IdOrder,IdProduct,Quantity" --permissions "anonymous:*" 
dab add "OrderStatus" --source "[dbo].[OrderStatus]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "Product" --source "[dbo].[Product]" --fields.include "Id,Name,Description,IdCategory,IdManufacturer,IdSupplier,IdUnit,Price,Quantity,Discount,PhotoPath" --permissions "anonymous:*" 
dab add "Role" --source "[dbo].[Role]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "Supplier" --source "[dbo].[Supplier]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "Unit" --source "[dbo].[Unit]" --fields.include "Id,Name" --permissions "anonymous:*" 
dab add "User" --source "[dbo].[User]" --fields.include "Id,FullName,Login,Password,IdRole" --permissions "anonymous:*" 
@echo Adding views and tables without primary key
@echo Adding column descriptions
@echo Adding relationships
dab update Order --relationship OrderStatus --target.entity OrderStatus --cardinality one
dab update OrderStatus --relationship Order --target.entity Order --cardinality many
dab update OrderItem --relationship Order --target.entity Order --cardinality one
dab update Order --relationship OrderItem --target.entity OrderItem --cardinality many
dab update OrderItem --relationship Product --target.entity Product --cardinality one
dab update Product --relationship OrderItem --target.entity OrderItem --cardinality many
dab update Product --relationship Category --target.entity Category --cardinality one
dab update Category --relationship Product --target.entity Product --cardinality many
dab update Product --relationship Manufacturer --target.entity Manufacturer --cardinality one
dab update Manufacturer --relationship Product --target.entity Product --cardinality many
dab update Product --relationship Supplier --target.entity Supplier --cardinality one
dab update Supplier --relationship Product --target.entity Product --cardinality many
dab update Product --relationship Unit --target.entity Unit --cardinality one
dab update Unit --relationship Product --target.entity Product --cardinality many
dab update User --relationship Role --target.entity Role --cardinality one
dab update Role --relationship User --target.entity User --cardinality many
@echo Adding stored procedures
@echo **
@echo ** run 'dab validate' to validate your configuration **
@echo ** run 'dab start' to start the development API host **
