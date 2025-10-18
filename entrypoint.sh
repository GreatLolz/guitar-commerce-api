#!/bin/bash
set -e

# Run migrations
dotnet ef database update

# Start the app
dotnet GuitarCommerceAPI.dll