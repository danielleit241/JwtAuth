[![CI/CD - JwtAuth](https://github.com/danielleit241/JwtAuth/actions/workflows/dotnet.yml/badge.svg)](https://github.com/danielleit241/JwtAuth/actions/workflows/dotnet.yml)

# JwtAuth

JwtAuth is a mini-project designed to demonstrate a typical workflow for creating and managing JWT (JSON Web Token) authentication using ASP.NET 8 and MSSQL. The project includes robust unit testing with xUnit and is CI/CD-ready for seamless integration and deployment.

## 🚀 Project Overview

- **Tech Stack:**  
  - ASP.NET 8  
  - MSSQL  
  - xUnit (Testing)  
  - CI/CD pipeline (GitHub Actions)

- **Key Features:**  
  - Secure JWT token generation and validation  
  - Clean and modular project structure  
  - Easy-to-follow workflow design  
  - Ready-to-use testing suite  
  - Automated build and deployment

## 🛠️ Workflow & Features

1. **User Registration & Login**  
   - Users can register and securely log in using their credentials.
2. **JWT Token Creation**  
   - Upon successful login, a JWT token is generated using ASP.NET 8's built-in libraries.
3. **Token Validation**  
   - Protected endpoints validate JWT tokens for secure access.
4. **Database Integration**  
   - User data and authentication details are stored in MSSQL.
5. **Testing**  
   - All core logic is covered by xUnit test cases.
6. **CI/CD**  
   - Automated builds and tests are triggered via GitHub Actions on every push or pull request.

## ⚡ Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/danielleit241/JwtAuth.git
   cd JwtAuth
   ```

2. **Set up the database**
   - Update your connection string in `appsettings.json` for MSSQL.

3. **Run the project**
   ```bash
   dotnet build
   dotnet run
   ```

4. **Run tests**
   ```bash
   dotnet test
   ```

## 🔗 CI/CD

- The project uses GitHub Actions for continuous integration and deployment.
- All pull requests and pushes to main will trigger builds and test runs automatically.

## 📁 Project Structure

```
JwtAuth/
├── Controllers/
├── Models/
├── Services/
├── Data/
├── Tests/
├── .github/workflows/
├── appsettings.json
└── ...
```

## 🧪 Testing

- All business logic is covered by xUnit test cases.
- To run tests:  
  ```bash
  dotnet test
  ```

## 🤝 Contributing

Contributions, issues, and feature requests are welcome. Feel free to check [issues](https://github.com/danielleit241/JwtAuth/issues) page or submit a pull request!

## 📬 Contact

- GitHub: [danielleit241](https://github.com/danielleit241)

---
