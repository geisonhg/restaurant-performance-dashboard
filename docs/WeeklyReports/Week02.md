# Weekly Project Report – Week 02
**Date:** 2025-11-06  
**Status:** 🟢 Green  
**Branch:** update-20251106  

---

## Project Title and Description
Restaurant Performance Dashboard — a web app to centralize restaurant data (sales, tips, expenses, and basic table metrics) and generate weekly/monthly PDF reports. The goal is to reduce manual work, improve transparency, and provide one source of truth for owners and managers.

---

## Problem Statement (Draft)
Small/medium restaurants often keep data across Excel, POS exports (Toast/SumUp), Stripe payouts, and manual notes. This fragmentation causes duplicate work, inconsistent numbers, and slow decision making (e.g., “How did we do vs last week?” “Are tips split fairly?”). We need a single place to ingest data, compute KPIs consistently, and produce reliable PDFs that match the dashboard.

---

## Stakeholders (Draft)
| Stakeholder | Needs |
|---|---|
| Owner | Profit view, trends, fair tip distribution, clean weekly/monthly PDF |
| Manager | Fast ingestion, clear KPIs, filters by date/range, exports |
| Staff | Transparent tip breakdown by week/period |
| Accountant | Consistent numbers matching exports, audit trail |
| Admin (IT) | Roles/permissions, imports audit, health checks, CI visibility |

---

## Objectives (Draft)
- Centralize sales, tips, and expenses with consistent KPIs and filters.  
- Automate weekly/monthly PDF reports that match the dashboard exactly.  
- Provide transparent tip allocation (FOH/BOH rules, audit of adjustments).  
- Keep imports robust (CSV + mapping wizard + error file) to ensure adoption.  
- Maintain reliability: CI pipeline, health endpoint, seed data for demos.

---

## Initial Architecture (Outline)
- **Frontend:** Blazor (or React TBD), responsive, global date filter, KPI cards + charts.
- **API (.NET 8):** REST endpoints (`/sales`, `/tips`, `/expenses`, `/imports`, `/reports`, `/health`).
- **Domain Layer:** business rules (tip split, validations, period comparisons).
- **Data Layer (EF Core):** SQL Server or PostgreSQL; indexed by date/category/staff.
- **Reporting:** QuestPDF/iText7; use the SAME queries as the dashboard for parity.
- **DevOps:** GitHub Actions (build/tests later), protected `main`, PR-based workflow.

ASCII sketch:

[ Blazor UI ] → [ .NET API ] → [ Domain ] → [ EF Core / SQL ]
│
└─> [ Reporting (PDF) ]


---

## Base Entities (Draft)
**Sale**
- `Id (guid)`, `Date (date)`, `Section (string?)`, `Covers (int)`, `NetAmount (decimal)`, `Tax (decimal)`, `ServiceType (enum? dine-in/delivery)`

**Tip**
- `Id`, `Date`, `StaffId (fk)`, `Amount (decimal)`, `Source (string: cash/card)`

**Expense**
- `Id`, `Date`, `CategoryId (fk)`, `Amount`, `Notes (string?)`, `Recurring (bool)`

**Staff**
- `Id`, `Name`, `Role (enum: Owner/Manager/FOH/BOH/Accountant)`, `Active (bool)`

**TipRule**
- `Id`, `ValidFrom (date)`, `ValidTo (date?)`, `FOHPercent (decimal)`, `BOHPercent (decimal)`

**Import**
- `Id`, `Type (enum: Sales/Tips/Expenses)`, `When (datetime)`, `FileName`, `TotalRows`, `ValidRows`, `InvalidRows`, `ErrorFilePath (string?)`

**(Optional) Stock**
- `ItemId`, `Name`, `UoM`, `CurrentQty`, `LowStockThreshold`, `Movements[] (date, delta, reason)`

---

## User Stories Completed This Week
- Drafted **problem statement**, **stakeholders**, and **objectives**.  
- Outlined **initial architecture** (layers, responsibilities, parity rules for PDFs).  
- Defined **base entities** for the first migration (Sales, Tips, Expenses, Staff, TipRules, Imports; Stock optional).

---

## User Stories Planned for Next Week
- Create .NET solution and API project skeleton.  
- Configure EF Core, first migration with **Sale/Tip/Expense/Staff/TipRule/Import**.  
- Implement `/health` endpoint.  
- Seed data for realistic Fri/Sat peaks.

---

## What Went Well
- Clear separation of concerns and simple domain model.  
- Architecture keeps PDF and dashboard in sync (same queries).  
- Weekly workflow on GitHub (update branches + PR to main) is working.

---

## What Didn’t Go Well / Issues Encountered
- Choosing SQL Server vs PostgreSQL: will decide based on hosting/college constraints.  
- Need to validate CSV edge cases early (dates, decimals, empty staff).

---

## GitHub Submission
- Weekly branch: `update-20251106`  
- Commit message: “Week 02: problem/stakeholders/objectives, architecture outline, base entities”
