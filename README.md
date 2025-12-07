<img width="1910" height="912" alt="1" src="https://github.com/user-attachments/assets/db0d2e85-5236-4795-9729-eed47ae1119b" /># Employee Management System

## Overview

The Employee Management System is a full-stack web application designed to streamline HR and team management tasks. It provides tools for handling employee records, assigning and tracking tasks, sending internal communications via mail and chats, monitoring attendance, and viewing notifications and dashboards. Built with a modern frontend for a responsive user interface and a robust backend for data persistence and real-time features, this system is ideal for small to medium-sized organizations.

From the provided screenshots, the app includes:
- **Dashboard**: Centralized overview of key metrics.
- **Employees**: CRUD operations for employee profiles, departments, assignments, and contacts.
- **Tasks**: Task creation, assignment, prioritization, status tracking, and Gantt views.
- **Mail**: Composing and sending emails to individuals or departments.
- **Chats**: Real-time messaging (likely powered by SignalR).
- **Attendance**: Time tracking and logging.
- **Profile**: User-specific settings and details.
- **Notifications**: Aggregated alerts (e.g., 35 unread).

This README focuses primarily on the frontend, as detailed backend information is sparse. The frontend is an Angular application, and the backend is a .NET Core API (inferred from repo structure and common pairings with SignalR).

**Live Demo**: [employee-managment-system-web-bo8t.vercel.app](https://employee-managment-system-web-bo8t.vercel.app) (hosted via Vercel).

## Features

- **Employee Management**: View, add, edit, and delete employee records; filter by department/status; assign to managers; export contacts.
- **Task Management**: Create tasks with titles, priorities (High/Low), due dates, and assignees; track progress (Open/In Progress); integrate with Gantt charts; add notes and messages.
- **Communication Tools**: Internal email composer with recipient selection (employees/departments); real-time chat for team collaboration.
- **Attendance Tracking**: Log and review employee attendance.
- **Notifications**: Centralized inbox for alerts, with unread counts.
- **Real-Time Updates**: Powered by SignalR for live chats and activity feeds.
- **Reporting & Exports**: Generate PDFs/Excel from employee data or tasks using jsPDF and ExcelJS.
- **User Authentication**: JWT-based login (evident from decoded tokens in screenshots).
- **Responsive UI**: Built with DevExtreme components for data grids, forms, and charts.

## Tech Stack

### Frontend (Angular App)
- **Framework**: Angular 16.x
- **UI Components**: DevExtreme Angular 24.1.3 (grids, forms, schedulers)
- **Real-Time**: @microsoft/signalr 8.0.7 for WebSocket connections
- **State Management**: Ngx-Toastr for notifications; Luxon for date handling
- **Utilities**: jsPDF + jsPDF-AutoTable for PDF exports; ExcelJS + FileSaver for Excel; JWT-Decode for token handling
- **Build Tools**: Angular CLI 16.x
- **Testing**: Jasmine/Karma
- **Other**: Zone.js, TSLib

See `package.json` for full dependencies.

### Backend (.NET Core)
- **Framework**: ASP.NET Core (inferred from repo name and SignalR integration)
- **Real-Time**: SignalR Hub for chats/notifications
- **Database**: Likely Entity Framework with SQL Server (common for such apps; confirm via repo files)
- **Authentication**: JWT Bearer tokens
- **API Endpoints**: RESTful APIs for employees, tasks, mail, etc. (e.g., `/api/employees`, `/api/tasks`)

**Note on Backend**: The backend repo lacks a README or detailed documentation. To flesh this out fully, I need:
- Project description/summary.
- Key features (e.g., specific API endpoints, database schema).
- Installation steps (e.g., .NET SDK version, connection strings, migrations).
- Tech stack details (e.g., Entity Framework version, middleware like CORS/Swagger).
- Any Docker/Kestrel config or deployment notes.
If you can provide the backend's `Program.cs`, `Startup.cs`, or a list of controllers/models, or update the repo with a README, that would help refine this.

## Quick Start

### Prerequisites
- Node.js 18+ and npm/yarn
- .NET SDK 6.0+ (for backend)
- Git
- Database (e.g., SQL Server; configure connection string)

### Frontend Setup
1. Clone the repo:
git clone https://github.com/awaisalii/EmployeeManagmentSystemWeb.git
cd EmployeeManagmentSystemWeb

2. Install dependencies:
npm install
3. Start the dev server (proxies to backend at `http://localhost:5000` by default; adjust `proxy.conf.json` if needed):
- App runs at `http://localhost:4200`.
4. Build for production: npm run build
- Outputs to `dist/`; serve via Nginx/Apache or deploy to Vercel/Netlify.

### Backend Setup
(Placeholder based on standard .NET Core practices; customize with details above.)
1. Clone the repo:
git clone https://github.com/awaisalii/EmployeeManagmentSystem.git
cd EmployeeManagmentSystem
2. Restore packages:
dotnet restore
3. Update `appsettings.json` with DB connection (e.g., `"ConnectionStrings": { "DefaultConnection": "Server=localhost;Database=EMS;Trusted_Connection=true;" }`).
4. Run migrations (if using EF):
dotnet ef migrations add InitialCreate
dotnet ef database update
5. Start the API:
- API runs at `https://localhost:5001` (or configured port); Swagger at `/swagger`.

### Full-Stack Run
- Start backend first.
- Start frontend; it will connect via API calls and SignalR.
- Login with credentials (default: check seed data or screenshots for "awais@awais").

## Usage

1. **Login**: Navigate to `/login` (or dashboard); use JWT for sessions.
2. **Manage Employees**: Go to Employees > Add/Edit/View. Use filters for departments like Management/Development.
3. **Assign Tasks**: Tasks > Add Task. Select assignee (e.g., Awaiss Aslam), set priority/due date.
4. **Communicate**: Mail for bulk emails; Chats for threaded discussions.
5. **Track Attendance**: Log via Attendance module.
6. **View Reports**: Export employee lists to Excel/PDF from actions menu.

Example API calls (from frontend network tab, inferred):
- GET `/api/employees` – List employees.
- POST `/api/tasks` – Create task.
- SignalR: Connect to `/chathub` for real-time.

## Development

- **Code Generation**: `ng generate component my-component`
- **Linting**: `npm run lint`
- **Testing**: `npm test` (unit); `npm run e2e` (if configured).
- **Hot Reload**: Enabled in dev mode.

## Deployment

- **Frontend**: Build and deploy to Vercel (as in demo), Netlify, or static host. Set base href for subpaths.
- **Backend**: Publish to Azure/IIS/Docker. Use `dotnet publish`. Configure HTTPS and CORS for frontend origin.
- **Database**: Migrate to cloud (Azure SQL) for prod.

## Contributing

1. Fork the repo.
2. Create a feature branch (`git checkout -b feature/amazing-feature`).
3. Commit changes (`git commit -m 'Add some amazing feature'`).
4. Push to branch (`git push origin feature/amazing-feature`).
5. Open a Pull Request.

No formal guidelines exist yet—propose improvements via issues. Focus on code quality, tests, and docs.

## License

MIT License (assumed; add to repos if not specified). See LICENSE file.

## Issues & Support

- Report bugs/feature requests on GitHub Issues.
- For questions, open a discussion.



🌐 Live Deployment
The frontend application is live but currently not connected to the backend API, which is not deployed.

Live Frontend: https://employee-managment-system-web-bo8t.vercel.app




<img width="1910" height="912" alt="1" src="https://github.com/user-attachments/assets/85c2df15-ef1d-44ad-9c31-4269a2f48725" />

<img width="1917" height="905" alt="2" src="https://github.com/user-attachments/assets/29897c9d-7daa-4e80-807c-e4f25278aeef" />

<img width="1915" height="904" alt="3" src="https://github.com/user-attachments/assets/06a42560-d281-4d25-b351-371a065e61c7" />
<img width="1911" height="904" alt="4" src="https://github.com/user-attachments/assets/c466caeb-53a6-4a1d-a59a-a531761b4a27" />

<img width="1916" height="909" alt="5" src="https://github.com/user-attachments/assets/e908211b-d1cf-4d55-9c27-c975c07f4fa8" />
<img width="1914" height="901" alt="6" src="https://github.com/user-attachments/assets/36606e08-ffaf-4eff-975f-080a9f177a57" />
<img width="1917" height="909" alt="7" src="https://github.com/user-attachments/assets/a8221434-0c7b-41b4-9ab9-fb6e6fc060b8" />
<img width="1916" height="906" alt="8" src="https://github.com/user-attachments/assets/13b3f746-11c3-49e5-98ef-d2859fec6462" />
<img width="1916" height="910" alt="9" src="https://github.com/user-attachments/assets/de6bbef8-ac55-4e7e-b1c6-4e204a89b26f" />
<img width="1913" height="902" alt="10" src="https://github.com/user-attachments/assets/7e4742cd-1283-4d1c-b3e3-955107faa3df" />
