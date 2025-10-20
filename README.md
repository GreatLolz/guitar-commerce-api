# GuitarCommerceAPI
API for the [e-commerce app](https://github.com/greatlolz/guitar-commerce) written using ASP.NET C#

# Requirements

### Development:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- MSSQL Server 2022 instance

# Setup

### Production:

This app is intended to be run in a docker compose stack, see [here](https://github.com/greatlolz/guitar-commerce) for more information.

### Development:

1. Stripe payments setup:
   Make sure you have created a stripe account and have access to the public and secret test keys (**pk_test_...** and **sk_test_...**)
   Now using the Stripe CLI tool:

   - Log in to Stripe:
      ```bash
      stripe login
      ```

   - Run listener:
      ```bash
      stripe listen --forward-to http://localhost:5180/api/v1/orders/webhook
      ```

2. Clone the repository:
   ```bash
   git clone https://github.com/greatlolz/guitar-commerce-api
   ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Add configuration to **appsettings.json**:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=<db_host>;Database=<db_name>;Trusted_Connection=False;TrustServerCertificate=true;User=<db_user>;Password=<db_pass>"
   },
   "JwtSettings": {
     "Issuer": "GuitarCommerceAPI",
     "Audience": "GuitarCommerce",
     "Key": "examplekey"
   },
   "StripeConfig": {
     "WebhookSecret": "whsec_example",
     "SecretKey": "sk_test_examplekey"
   }
   ```
   
5. Set environment variables:
   ```bash
   ASPNETCORE_ENVIRONMENT="Development"
   ```

6. Run the app
   ```bash
   cd GuitarCommerceAPI
   dotnet run
   ```

# Usage & Documentation

After running the application, you can access OpenAPI (Swagger) documentation at `http://localhost:5180/swagger/index.html`

# License

This repository is licensed under the MIT license.
   
