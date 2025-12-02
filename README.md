# EventTask

## About this solution

This is an Event Registration Application built using the ABP Framework and follows Domain Driven Design (DDD) practices.  
This README has been updated to reflect the project requirements provided for the Event Management and Registration system.

---

## Event Registration Application

### **Event Entity**
Each event contains the following fields:

- **Name** (Arabic/English)
- **Capacity** (number) *(required only if the event is not online)*
- **IsOnline** (boolean)
- **StartDate** (with time)
- **EndDate** (with time)
- **Organizer** (User)
- **Link** *(required if the event is online)*
- **Location** *(required if the event is not online)*
- **IsActive** (boolean)

---

## Use Cases

- **Only Admin users can create events.**
- **Admins can manage only their own events**  
  (An admin cannot update or delete another admin's events).
- **Any user can:**
  - View all *active* events.
  - View *their own* events (active or inactive).
  - Cannot view other users’ inactive events.
- **Users can register for active events** (only if capacity is available).
- **Users can cancel their registration** up to **1 hour before the event start time**.

---

## Tasks to Implement

- CRUD operations for events (Admin only).
- User Registration & Cancellation for events.
- Admins can list registrations for their own events.

---

## Notes

- Built using the **ABP Framework**.
- **Localization (Arabic/English)** must be supported.
- Follow **DDD best practices**.
- Frontend must be developed using **Angular**.

---

## Pre‑requirements

- [.NET 10.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
- [Node v18 or v20](https://nodejs.org/en)

---

## Configuration

Before running the application, verify:

- Update `ConnectionStrings` in:
  - `EventTask.HttpApi.Host/appsettings.json`
  - `EventTask.DbMigrator/appsettings.json`

---

## Before Running the Application

1. Run:
   ```bash
   abp install-libs
   ```
   This installs all client-side dependencies.

2. Run:
   ```bash
   EventTask.DbMigrator
   ```
   This creates the database and seeds initial data.

---

## Generating a Signing Certificate

For production, generate an OpenIddict certificate:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p 4182956d-d347-469f-84cd-bc129b017e9d
```

More info:
- OpenIddict Certificates  
- ABP Deployment Documentation

---

## Solution Structure

This is a layered monolith application with the following projects:

- **EventTask.DbMigrator**  
  Console app for migrations and seeding.

- **EventTask.HttpApi.Host**  
  ASP.NET Core API hosting all endpoints.

- **angular**  
  Angular frontend application.

---

## Deploying the Application

Deployment follows standard ASP.NET Core deployment practices.  
For detailed instructions, review:

- ABP Deployment Docs

---

## Additional Resources

### Internal
- [Angular App README](./angular/README.md)

### External
- ABP Web App Tutorial  
- ABP Application Startup Template

### Credentials for login app
- enter admin as username and 1q2w3E* as password to login to the application