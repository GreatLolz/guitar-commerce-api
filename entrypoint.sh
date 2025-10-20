#!/bin/bash
set -e

export ASPNETCORE_URLS="http://0.0.0.0:5180"

dotnet GuitarCommerceAPI.dll