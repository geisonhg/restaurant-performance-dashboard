# 🍽️ Restaurant Performance Dashboard  
**Authors:** Geison Herrera & Daniel Vega  
**Course:** BSc in Computing  
**College:** Dorset College Dublin  
**Date:** October 2025  

---

## 🧠 Project Overview  

Our project is called **Restaurant Performance Dashboard**, and it aims to build a digital system that helps restaurant owners and managers truly understand how their business is performing.  

Right now, many restaurants (including the one where we both work or have experience in) rely on several disconnected tools — Excel sheets, Stripe, Toast, SumUp, and even handwritten notes. This makes it hard to get a clear picture of sales, expenses, or tips without wasting a lot of time combining data manually.  

With this project, we want to create a **centralized dashboard** that brings everything together. It will automatically calculate KPIs, generate graphs, and produce weekly or monthly PDF reports that are easy to read and share.  

---

## 🎯 Project Goals  

Our main goals are:
- Reduce the time spent collecting and merging data from different sources.
- Give restaurant owners clear insights about sales, tips, expenses, and staff performance.
- Generate automatic weekly and monthly reports in PDF format.
- Design a modern, intuitive dashboard that’s easy to use.
- Apply what we’ve learned in software development to a real, useful problem.

---

## 💡 Problem Statement  

The restaurant industry runs fast, and managers rarely have time to manually review all financial data.  
We’ve seen that even when data exists, it’s stored in multiple formats or systems: one spreadsheet for tips, another for food cost, another for daily sales.  

This makes reporting slow and sometimes inaccurate.  

Our project will **automate and centralize this process** so managers and owners can view their data in one place — visual, clean, and fast.

---

## 👥 Stakeholders  

| Role | Description |
|------|--------------|
| **Restaurant Owner** | Needs to see profit, cost, and tip distribution easily. |
| **Manager** | Monitors sales, expenses, and daily operations. |
| **Staff Members** | Can check their individual performance and tip amounts. |
| **Accountant** | Reviews reports for tax and payroll purposes. |

---

## ⚙️ Technical Approach  

This is a **full-stack web application** that combines backend and frontend technologies.

### Backend  
- Developed with **.NET 8 (C#)** and **Entity Framework Core**.  
- Exposes RESTful API endpoints like `/sales`, `/tips`, `/expenses`, `/reports`.  
- Uses a relational database (SQL Server or PostgreSQL).  
- Includes seed scripts to simulate realistic data.

### Frontend  
- Built with **Blazor** or **React**, showing dashboards and key metrics.  
- Responsive and clean design focused on usability.  
- Includes user roles (Owner, Manager, Staff, Accountant).

### Reporting  
- Automatic **PDF report generation** with QuestPDF or iText7.  
- Summaries per week and month, including KPIs like food cost %, busiest days, and staff tip share.

### CI/CD  
- Uses **GitHub Actions** to build, test, and validate every commit.  
- Branching model includes `main`, `dev`, `feature/*`, `report/*`, and weekly `update/*` branches.

---

## 🧩 Architecture Overview  

The system follows a layered architecture pattern:

Frontend (Blazor)
↓
API Gateway (.NET)
↓
Domain Layer (Business Logic)
↓
Data Layer (Database Access with EF Core)


---

## 📊 Main Entities  

| Entity | Description |
|--------|--------------|
| **Sale** | Stores daily sales per section or waiter. |
| **Tip** | Records total tips and distribution by role. |
| **Expense** | Includes daily costs such as supplies or ingredients. |
| **Staff** | Contains data about each employee, hours, and role. |
| **Report** | Holds weekly and monthly summaries for download. |

---

## 🧱 User Stories (Summary)

Some key user stories extracted from our main plan:

### A. Sales and Tips
- As a manager, I can upload daily sales from CSV files.  
- As an owner, I can compare daily or weekly sales and tips through charts.  
- As staff, I can view my total weekly tips.  

### B. Expenses and Profit
- As an accountant, I can record and analyze weekly expenses.  
- As a manager, I can view the food cost percentage automatically.  

### C. Dashboard and Reports
- As an owner, I can see a live dashboard with sales and expenses KPIs.  
- As a manager, I can export or print a PDF summary each week.  

### D. Data Management
- As an admin, I can import or export data safely.  
- As a manager, I can view alerts for high expenses or missing uploads.  

---

## 🗓️ Delivery Plan (Sprints Overview)

We’ll follow an agile workflow with **10 sprints (2 weeks each)**.  

| Sprint | Focus | Key Deliverables |
|--------|--------|------------------|
| **1** | Project setup | Repo, CI scaffold, DB setup, health endpoint, seed data |
| **2** | CSV ingestion | Upload endpoints, validation, mapping, error feedback |
| **3** | Dashboard | UI, charts, data visualization |
| **4** | Tips logic | FOH/BOH tip split, validation, reports |
| **5** | Expenses | Expense upload, categories, analysis |
| **6** | Reports | PDF generation, export functionality |
| **7** | Roles | Authentication and role-based access |
| **8** | Testing | Unit tests and data validation |
| **9** | CI/CD | Full pipeline setup and badges |
| **10** | Final polish | Documentation and presentation demo |

---

## 📈 Expected Benefits  

- Save time and effort on daily and weekly reporting.  
- Improve data accuracy and reliability.  
- Provide a clear picture of financial performance.  
- Help staff and management make faster, smarter decisions.  
- Demonstrate our skills as developers working on a real, practical system.  

---

## 🧰 Tools and Technologies  

| Category | Tools |
|-----------|-------|
| Backend | .NET 8, C#, EF Core |
| Frontend | Blazor / React |
| Database | SQL Server / PostgreSQL |
| Reporting | QuestPDF / iText7 |
| Version Control | Git + GitHub |
| CI/CD | GitHub Actions |
| Data Seeding | Bogus / Faker |
| UI Design | Canva / Figma |

---

## 🔍 Risks and Challenges  

- Balancing work, study, and project deadlines.  
- Learning advanced topics like PDF generation and data visualization.  
- Keeping the project running smoothly on GitHub Actions.  
- Managing weekly deliverables and reports consistently.  

---

## 💬 Personal Motivation  

We both work or have worked in restaurants, so we’ve seen this problem first-hand — managers spending hours trying to collect data from multiple sources.  
That experience inspired us to build something that actually solves a real issue we understand personally.  

This project combines our programming studies with real-world experience, and it’s a chance to apply everything we’ve learned in C#, data management, and DevOps.  

It’s not just for college; it’s a system we could genuinely use in the future.  

---

## 🏁 Conclusion  

The **Restaurant Performance Dashboard** is designed to simplify how restaurant data is managed, making operations clearer, faster, and smarter.  

It’s a project developed by **Geison Herrera and Daniel Vega** to merge our passion for technology and our real-world experience in hospitality, while showcasing our teamwork, coding ability, and problem-solving mindset.  

---
