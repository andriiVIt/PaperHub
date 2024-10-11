# Paper Hub

## Project Description
Paper Hub is a business-to-business (B2B) e-commerce platform developed for Dunder Mifflin Infinity, a fictional paper company. The platform allows businesses to order different types of paper and manage their orders, as well as manage inventory from the admin side.

## Technology Stack
- **Frontend**: React, Vite, Jotai (state management)
- **Backend**: .NET Web API, C#, Entity Framework
- **Database**: PostgreSQL
- **Testing**: xUnit

## Features
### Customer Features
1. Place orders for paper products, including multiple order entries.
2. View own order history.
3. Browse product catalog with filtering, sorting, and search options.

### Admin Features
1. Create new products, discontinue products, and restock inventory.
2. View order history for all customers.
3. Create custom properties for paper products (e.g., water-resistant, sturdy).
4. Update order status.

## How to Run the Project
### Prerequisites
- Docker
- .NET 8.0 SDK
- Node.js and npm

### Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone https://github.com/andriiVIt/PaperHub.git
   cd PaperHub
   ```

2. **Database Setup using Docker**
   Ensure Docker is running, then execute the following command to start the PostgreSQL database:
   ```bash
   docker-compose up -d
   ```
   This will set up a PostgreSQL container and a volume to persist the data.

3. **Seed Data**
   To automatically populate the database with initial data, a `seed_data.sql` file is provided in the `Database` directory.
   ```bash
   psql -h localhost -p 5432 -U user -d paperdb -f Database/seed_data.sql
   ```
   Use the credentials defined in `docker-compose.yml` for `user` and `password`.

4. **Run Backend**
   Navigate to the API directory and run the backend:
   ```bash
   cd Api
   dotnet run
   ```

5. **Run Frontend**
   Navigate to the frontend directory and run the frontend:
   ```bash
   cd ../paper-hub-client
   npm install
   npm run dev
   ```
   The frontend should now be accessible at `http://localhost:5173`.

## Project Structure
- **Api**: Contains backend code for handling requests, business logic, and database interactions.
- **DataAccess**: Contains Entity Framework models and database context (`MyDbContext`).
- **paper-hub-client**: Contains the frontend code for customer interaction with the platform.
- **PaperHub.Tests**: Contains unit tests for backend services.

## Environment Variables
The backend requires some environment variables, which are stored in `appsettings.json`:
- `DbConnectionString`: Connection string to the PostgreSQL database. This needs to be updated if running on a different environment.

## Testing
- The backend is tested using `xUnit`. Tests are located in the `PaperHub.Tests` folder.
- Automated tests run on every commit using GitHub Actions.

 

## Contributors
- Andrii Savchenko - https://github.com/andriiVIt/PaperHub

## User Stories

### Completed User Stories
1. **Role Selection**: As a user, I can choose my role as either a customer or a business admin.
2. **Customer Selection**: As a customer, I can select an existing customer from a list or create a new customer and then proceed into the system.
3. **Product Browsing and Ordering**: As a customer, I can view products and place orders.
4. **Admin Role Features**: As a business admin, I can log in to the system, view the list of products, create new products, and assign properties to them. I can also create new product properties.
5. **Stock Update**: As a business admin, I can update the stock directly on the product card.
6. **Order History Management**: As a business admin, I have access to order history and can change the status of orders.
